﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Arch.CMessaging.Client.Core.Utils;
using SQLite;
//using Microsoft.Data.Sqlite;

namespace AITool
{

    public class SQLiteHistory:IDisposable
    {
        private bool disposedValue;

        public string Filename { get; } = "";
        public bool ReadOnly { get; } = false;
        public ConcurrentDictionary<string, History> HistoryDic { get; } = new ConcurrentDictionary<string, History>();
        public DateTime InitializeTime { get; } = DateTime.Now;
        public DateTime LastUpdateTime { get; set; } = DateTime.MinValue;
        private SQLiteAsyncConnection sqlite_conn { get; set; } = null;
        public ThreadSafe.Integer AddedCount { get; set; } = new ThreadSafe.Integer(0);
        public ThreadSafe.Integer DeletedCount { get; set; } = new ThreadSafe.Integer(0);
        private ThreadSafe.Boolean HasChanged { get; set; } = new ThreadSafe.Boolean(true);
        private ThreadSafe.Boolean IsUpdating { get; set; } = new ThreadSafe.Boolean(false);

        public MovingCalcs AddTimeCalc { get; set; } = new MovingCalcs(1000);
        public MovingCalcs DeleteTimeCalc { get; set; } = new MovingCalcs(1000);

        public static SemaphoreSlim Semaphore_Updating = new SemaphoreSlim(1, 1);

        public SQLiteHistory(string Filename, bool ReadOnly)
        {
            if (string.IsNullOrEmpty(Filename))
                throw new System.ArgumentException("Parameter cannot be empty", "Filename");

            this.Filename = Filename;
            this.ReadOnly = ReadOnly;


        }

        private async Task<bool> IsSQLiteDBConnectedAsync()
        {

            //make sure only one thread updating at a time
            //await Semaphore_Updating.WaitAsync();

            try
            {
                return (sqlite_conn != null && !string.IsNullOrEmpty(sqlite_conn.DatabasePath));
            }
            catch (Exception ex)
            {
                Global.Log("Error: " + Global.ExMsg(ex));

                return false;
            }
            finally
            {
                //Semaphore_Updating.Release();
            }
        }

        private async void DisposeConnection()
        {
            try
            {
                await sqlite_conn.CloseAsync();
                sqlite_conn = null;

            }
            catch (Exception ex)
            {
                Global.Log("Error: " + Global.ExMsg(ex));

            }
        }
        private async System.Threading.Tasks.Task<bool> CreateConnectionAsync()
        {
            bool ret = false;

            //make sure only one thread updating at a time
            //await Semaphore_Updating.WaitAsync();

            try
            {
                Stopwatch sw = Stopwatch.StartNew();

                Global.UpdateProgressBar("Initializing history database...", 1, 1);

                this.HistoryDic.Clear();
                this.HasChanged.WriteFullFence(false);
                this.DeletedCount.WriteFullFence(0);
                this.AddedCount.WriteFullFence(0);

                //https://www.sqlite.org/threadsafe.html
                SQLiteOpenFlags flags = SQLiteOpenFlags.SharedCache; // SQLiteOpenFlags.Create; // | SQLiteOpenFlags.NoMutex;  //| SQLiteOpenFlags.FullMutex;
                string sflags = "SharedCache";

                if (this.ReadOnly)
                {
                    flags = flags | SQLiteOpenFlags.ReadOnly;
                    sflags += "|ReadOnly";
                }
                else
                {
                    flags = flags | SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite;
                    sflags += "|Create|ReadWrite";
                }

                if (await this.IsSQLiteDBConnectedAsync())
                {
                    this.DisposeConnection();
                }

                //If the database file doesn't exist, the default behaviour is to create a new file
                sqlite_conn = new SQLiteAsyncConnection(this.Filename, flags, true);


                //make sure table exists:
                CreateTableResult ctr = await sqlite_conn.CreateTableAsync<History>();


                await sqlite_conn.ExecuteScalarAsync<int>(@"PRAGMA journal_mode = 'WAL';", new object[] { });
                await sqlite_conn.ExecuteScalarAsync<int>(@"PRAGMA busy_timeout = 30000;", new object[] { });


                sw.Stop();

                Global.Log($"Created connection to SQLite db v{sqlite_conn.LibVersionNumber} in {sw.ElapsedMilliseconds}ms - TableCreate='{ctr.ToString()}', Flags='{sflags}': {this.Filename}");

                HasChanged.WriteFullFence(true);

            }
            catch (Exception ex)
            {

                Global.Log("Error: " + Global.ExMsg(ex));
            }

            return ret;
        }

        public async Task<bool> InsertHistoryItem(History hist)
        {
            bool ret = false;
            int iret = 0;

            //make sure only one thread updating at a time
            //await Semaphore_Updating.WaitAsync();

            //this.IsUpdating.WriteFullFence(true);

            try
            {


                ret = this.HistoryDic.TryAdd(hist.Filename.ToLower(), hist);

                if (!ret)
                {
                    Global.Log($"Info: File already existed in dictionary: {hist.Filename}");
                }

                Stopwatch sw = Stopwatch.StartNew();

                iret = await this.sqlite_conn.InsertAsync(hist);

                this.AddTimeCalc.AddToCalc(sw.ElapsedMilliseconds);

                if (iret != 1)
                {
                    Global.Log($"Error: When trying to insert database entry, RowsAdded count was {ret} but we expected 1. StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}");
                }

                if (sw.ElapsedMilliseconds > 5000)
                    Global.Log($"Info: It took a long time to add a history item @ {sw.ElapsedMilliseconds}ms, (Count={AddTimeCalc.Count}, Min={AddTimeCalc.Min}ms, Max={AddTimeCalc.Max}ms, Avg={AddTimeCalc.Average.ToString("#####")}ms), StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}: {Filename}");

            }
            catch (Exception ex)
            {

                if (ex.Message.ToLower().Contains("UNIQUE constraint failed".ToLower()))
                {
                    Global.Log($"Info: File was already in the database: {hist.Filename}");
                }
                else
                {
                    Global.Log($"Error: StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}: {hist.Filename}: " + Global.ExMsg(ex));
                }
            }

            if (ret || iret > 0)
            {
                this.AddedCount.AtomicAddAndGet(1); // AtomicIncrementAndGet();
                this.LastUpdateTime = DateTime.Now;
                this.HasChanged.WriteFullFence(true);

            }

            //Semaphore_Updating.Release();

            //this.IsUpdating.WriteFullFence(false);

            return ret;

        }
        public async Task<bool> DeleteHistoryItem(string Filename)
        {
            bool ret = false;
            int dret = 0;

            //make sure only one thread updating at a time
            //await Semaphore_Updating.WaitAsync();

            //this.IsUpdating.WriteFullFence(true);

            try
            {


                History hist;
                ret = this.HistoryDic.TryRemove(Filename.ToLower(), out hist);

                //if (!ret)
                //{
                //    Global.Log($"Info: File was not in dictionary '{Filename}'");
                //}

                Stopwatch sw = Stopwatch.StartNew();
                //Error: Cannot delete String: it has no PK [NotSupportedException] Mod: <DeleteHistoryItem>d__18 Line:150:5
                //trying to use object rather than primarykey to delete
                if (hist == null)
                    hist = new History().Create(Filename,DateTime.Now,"unknown","","",false,"");

                dret = await this.sqlite_conn.DeleteAsync(hist);

                this.DeleteTimeCalc.AddToCalc(sw.ElapsedMilliseconds);

                //if (dret != 1)
                //{
                //    Global.Log($"Error: When trying to delete database entry '{Filename}', RowsDeleted count was {ret} but we expected 1. StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}");
                //}
                //else
                //{
                //    Global.Log($"Removed {dret} database entry for '{Filename}'");
                //}

                if (sw.ElapsedMilliseconds > 5000)
                    Global.Log($"Info: It took a long time to delete a history item @ {sw.ElapsedMilliseconds}ms, (Count={DeleteTimeCalc.Count}, Min={DeleteTimeCalc.Min}ms, Max={DeleteTimeCalc.Max}ms, Avg={DeleteTimeCalc.Average.ToString("#####")}ms), StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}: {Filename}");

            }
            catch (Exception ex)
            {

                Global.Log($"Error: StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}: '{Filename}' - " + Global.ExMsg(ex));
            }

            if (ret || dret > 0)
            {
                this.DeletedCount.AtomicAddAndGet(1); // AtomicIncrementAndGet();
                this.LastUpdateTime = DateTime.Now;
                this.HasChanged.WriteFullFence(true);
            }

            //Semaphore_Updating.Release();

            //this.IsUpdating.WriteFullFence(false);

            return ret;

        }

        public async Task<bool> MigrateHistoryCSV(string Filename)
        {

            bool ret = false;

            try
            {

                //if (!await this.IsSQLiteDBConnectedAsync())
                //    await this.CreateConnectionAsync();

                //run in another thread so we dont block UI
                await Task.Run(async () =>
                {

                    if (System.IO.File.Exists(Filename))
                    {
                        Global.Log("Migrating history list from cameras/history.csv ...");

                        Stopwatch SW = Stopwatch.StartNew();

                        //delete obsolete entries from history.csv
                        //CleanCSVList(); //removed to load the history list faster

                        List<string> result = new List<string>(); //List that later on will be containing all lines of the csv file

                        bool Success = await Global.WaitForFileAccessAsync(Filename);

                        if (Success)
                        {
                            //load all lines except the first line into List (the first line is the table heading and not an alert entry)
                            foreach (string line in System.IO.File.ReadAllLines(Filename).Skip(1))
                            {
                                result.Add(line);
                            }

                            Global.Log($"...Found {result.Count} lines.");

                            List<string> itemsToDelete = new List<string>(); //stores all filenames of history.csv entries that need to be removed

                            //load all List elements into the ListView for each row
                            int added = 0;
                            int removed = 0;
                            foreach (var val in result)
                            {

                                History hist = new History().CreateFromCSV(val);
                                if (File.Exists(hist.Filename))
                                {
                                    if (await this.InsertHistoryItem(hist))
                                    {
                                        added++;
                                        //this.AddedCount.AtomicIncrementAndGet();
                                    }
                                    else
                                    {
                                        removed++;
                                    }
                                }
                                else
                                {
                                    removed++;
                                }
                            }

                            ret = (added > 0);

                            //this.AddedCount.AtomicAddAndGet(added);
                            this.DeletedCount.AtomicAddAndGet(removed);

                            //try to get a better feel how much time this function consumes - Vorlon
                            Global.Log($"...Added {added} out of {result.Count} history items in {{yellow}}{SW.ElapsedMilliseconds}ms{{white}}, {this.HistoryDic.Count()} lines.");

                        }
                        else
                        {
                            Global.Log($"Error: Could not gain access to history file for {SW.ElapsedMilliseconds}ms - {AppSettings.Settings.HistoryFileName}");

                        }

                    }
                    else
                    {
                        Global.Log($"File does not exist, could not migrate: {Filename}");
                    }


                });


            }
            catch (Exception ex)
            {

                Global.Log("Error: " + Global.ExMsg(ex));
            }

            if (ret)
            {
                HasChanged.WriteFullFence(true);
            }

            return ret;


        }


        public async Task<bool> HasUpdates()
        {

            //make sure only one thread updating at a time
            //await Semaphore_Updating.WaitAsync();

            try
            {
                //lets loop in case the db is still initializing in the background
                Stopwatch sw = Stopwatch.StartNew();

                bool HadToWait = false;

                while (this.IsUpdating.ReadFullFence() || sw.ElapsedMilliseconds <= 10000)
                {
                    HadToWait = true;
                    await Task.Delay(50);
                }

                if (sw.ElapsedMilliseconds > 1000)
                    Global.Log($"Info: Had to wait {sw.ElapsedMilliseconds}ms for database to become free");

                if (!this.ReadOnly || HadToWait)  //assume if we had to wait the database was just updating
                {
                    //we are running in the same process as the database updater
                    //no need to re-check sqlite db every time

                    if (this.HasChanged.ReadFullFence())
                    {
                        this.HasChanged.WriteFullFence(false);
                        return true;
                    }
                }
                else
                {
                    return await UpdateHistoryList(false);
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //Semaphore_Updating.Release();
            }

            return false;

        }

        public async Task<bool> UpdateHistoryList(bool Clean)
        {

            bool ret = false;

            this.IsUpdating.WriteFullFence(true);

            try
            {
                Global.UpdateProgressBar("Reading database...", 1, 1);

                Stopwatch sw = Stopwatch.StartNew();

                if (!await this.IsSQLiteDBConnectedAsync())
                    await this.CreateConnectionAsync();

                AsyncTableQuery<History> query = sqlite_conn.Table<History>();
                List<History> CurList = await query.ToListAsync();

                int added = 0;
                int removed = 0;
                bool isnew = (this.HistoryDic.Count == 0);

                //Dont block UI, but wait for the thread to finish
                await Task.Run(async () =>
                {

                    Dictionary<string, String> tmpdic = new Dictionary<string, String>();

                    //lets try to keep the original objects in memory and not create them new
                    int cnt = 0;
                    foreach (History hist in CurList)
                    {
                        cnt++;

                        if (cnt == 1 || cnt == CurList.Count || (cnt % (CurList.Count / 10) > 0))
                        {
                            Global.UpdateProgressBar("Reading database...", cnt, CurList.Count);
                        }

                        if (!isnew)
                        {
                            //add missing
                            if (this.HistoryDic.TryAdd(hist.Filename.ToLower(), hist))
                            {
                                added++;
                            }
                            tmpdic.Add(hist.Filename.ToLower(), hist.Filename);
                        }
                        else
                        {
                            this.HistoryDic.TryAdd(hist.Filename.ToLower(), hist);
                            added++;
                        }
                    }

                    if (!isnew)
                    {
                        //subtract
                        foreach (History hist in this.HistoryDic.Values)
                        {
                            if (!tmpdic.ContainsKey(hist.Filename.ToLower()))
                            {
                                History th;
                                this.HistoryDic.TryRemove(hist.Filename.ToLower(), out th);
                                removed++;
                            }

                        }

                    }

                    if (Clean && !this.ReadOnly)
                        await this.CleanHistoryList();


                });

                ret = (added > 0 || removed > 0);

                if (ret)
                {
                    this.DeletedCount.AtomicAddAndGet(removed);
                    if (!isnew)
                        this.AddedCount.AtomicAddAndGet(added);

                    this.LastUpdateTime = DateTime.Now;
                    this.HasChanged.WriteFullFence(true);
                }

                Global.Log($"Update History Database: Added={added}, removed={removed}, total={this.HistoryDic.Count}, StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count} in {sw.ElapsedMilliseconds}ms");

            }
            catch (Exception ex)
            {

                Global.Log("Error: " + Global.ExMsg(ex));
            }


            this.IsUpdating.WriteFullFence(false);

            Global.UpdateProgressBar("", 0, 1);


            return ret;

        }

        public async Task<bool> CleanHistoryList()
        {

            bool ret = false;

            try
            {
                Stopwatch sw = Stopwatch.StartNew();


                if (!await this.IsSQLiteDBConnectedAsync())
                    await this.CreateConnectionAsync();


                ConcurrentBag<History> removed = new ConcurrentBag<History>();   //this may only need to be a list, but just being safe in threading


                //run the file exists check in another thread so we dont freeze the UI, but WAIT for it
                await Task.Run(async () =>
                {
                    Global.Log("Removing missing files from in-memory database...");

                    int missing = 0;
                    int tooold = 0;
                    int cnt = 0;
                    int HistCount = this.HistoryDic.Count;
                    //we are allowed to do this with a ConcurrentDictionary...
                    foreach (History hist in this.HistoryDic.Values)
                    {
                        cnt++;

                        if (cnt == 1 || cnt == HistCount || (HistCount > 0 && cnt % (HistCount / 10) > 0))
                        {
                            Global.UpdateProgressBar("Cleaning database (1 of 2)...", cnt, HistCount);
                        }

                        bool IsTooOld = (DateTime.Now - hist.Date).TotalDays >= AppSettings.Settings.MaxHistoryAgeDays;

                        if (IsTooOld || !File.Exists(hist.Filename))
                        {
                            if (IsTooOld)
                                tooold++;
                            else
                                missing++;

                            removed.Add(hist);

                            History rhist;

                            if (!this.HistoryDic.TryRemove(hist.Filename.ToLower(), out rhist))
                                Global.Log($"Warning: Could not remove from in-memory database: {hist.Filename}");
                        }

                    }

                    if (removed.Count > 0)
                    {
                        Global.Log($"Removing {missing} files and {tooold} database entries that are older than {AppSettings.Settings.MaxHistoryAgeDays} days (MaxHistoryAgeDays) from file database...");
                        //start another thread to finish cleaning the database so that the app gets the list faster...
                        //the db should be thread safe due to opening with fullmutex flag
                        Stopwatch swr = Stopwatch.StartNew();
                        int rcnt = 0;
                        int failedcnt = 0;
                        cnt = 0;
                        foreach (History hist in removed)
                        {
                            cnt++;

                            if (cnt == 1 || cnt == removed.Count || (removed.Count > 0 && cnt % (removed.Count / 10) > 0))
                            {
                                Global.UpdateProgressBar("Cleaning database (2 of 2)...", cnt, removed.Count);
                            }

                            int rowsdeleted = 0;

                            try
                            {
                                Stopwatch dsw = Stopwatch.StartNew();

                                rowsdeleted = await this.sqlite_conn.DeleteAsync(hist);

                                this.DeleteTimeCalc.AddToCalc(dsw.ElapsedMilliseconds);

                                if (rowsdeleted != 1)
                                {
                                    failedcnt++;
                                    Global.Log($"...Error: When trying to delete database entry, RowsDeleted count was {rowsdeleted} but we expected 1.");
                                }
                            }
                            catch (Exception ex)
                            {
                                failedcnt++;
                                Global.Log($"Error: StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}: '{Filename}' - " + Global.ExMsg(ex));
                            }
                            rcnt = rcnt + rowsdeleted;
                        }

                        this.DeletedCount.AtomicAddAndGet(rcnt);

                        Global.Log($"...Cleaned {rcnt} of {removed.Count} (Failed={failedcnt}) history file database items because file did not exist in {swr.ElapsedMilliseconds}ms (Count={DeleteTimeCalc.Count}, Min={DeleteTimeCalc.Min}ms, Max={DeleteTimeCalc.Max}ms, Avg={DeleteTimeCalc.Average.ToString("#####")}ms)");

                    }
                    else
                    {
                        Global.Log("Info: No missing files to clean from database?");
                    }


                });


                Global.Log($"...Cleaned {removed.Count} items in {sw.ElapsedMilliseconds}ms");


                ret = removed.Count > 0;


            }
            catch (Exception ex)
            {

                Global.Log("Error: " + Global.ExMsg(ex));
            }

            return ret;

        }


        protected virtual async void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (await this.IsSQLiteDBConnectedAsync())
                    {
                        this.DisposeConnection();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }


        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

