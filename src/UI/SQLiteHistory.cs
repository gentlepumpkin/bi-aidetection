using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
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

        public SQLiteHistory(string Filename, bool ReadOnly)
        {
            if (string.IsNullOrEmpty(Filename))
                throw new System.ArgumentException("Parameter cannot be empty", "Filename");

            this.Filename = Filename;
            this.ReadOnly = ReadOnly;


        }

        private bool IsSQLiteDBConnected()
        {
            try
            {
                return (sqlite_conn != null && !string.IsNullOrEmpty(sqlite_conn.DatabasePath));
            }
            catch (Exception ex)
            {
                Global.Log("Error: " + Global.ExMsg(ex));

                return false;
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

            try
            {
                Stopwatch sw = Stopwatch.StartNew();


                this.HistoryDic.Clear();
                this.HasChanged.WriteFullFence(false);
                this.DeletedCount.WriteFullFence(0);
                this.AddedCount.WriteFullFence(0);

                //FullMutex for safe multithreading
                SQLiteOpenFlags flags = SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex;
                if (this.ReadOnly)
                {
                    flags = flags | SQLiteOpenFlags.ReadOnly;
                }
                else
                {
                    flags = flags | SQLiteOpenFlags.ReadWrite;
                }

                if (this.IsSQLiteDBConnected())
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

                Global.Log($"Created connection to SQLite db v{sqlite_conn.LibVersionNumber} in {sw.ElapsedMilliseconds}ms - TableCreate={ctr.ToString()}: {this.Filename}");

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

            try
            {
                Stopwatch sw = Stopwatch.StartNew();

                if (!this.IsSQLiteDBConnected())
                    await this.CreateConnectionAsync();

                ret = this.HistoryDic.TryAdd(hist.Filename.ToLower(), hist);

                //if (!ret)
                //{
                //	Global.Log($"Info: File already existed in dictionary: {hist.Filename}");
                //}

                int iret = await this.sqlite_conn.InsertAsync(hist);

                if (iret != 1)
                {
                    Global.Log($"Error: When trying to insert database entry, RowsAdded count was {ret} but we expected 1. StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}");
                }

                if (sw.ElapsedMilliseconds > 1000)
                    Global.Log($"Warning: It took a long time to add a history item @ {sw.ElapsedMilliseconds}ms, StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}: {Filename}");
                
            }
            catch (Exception ex)
            {

                if (ex.Message.ToLower().Contains("UNIQUE constraint failed"))
                {
                    //Global.Log($"Info: File was already in the database: {hist.Filename}");
                }
                else
                {
                    Global.Log($"Error: StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}: {hist.Filename}: " + Global.ExMsg(ex));
                }
            }

            if (ret)
            {
                this.AddedCount.AtomicIncrementAndGet();
                this.LastUpdateTime = DateTime.Now;
                this.HasChanged.WriteFullFence(true);

            }

            return ret;

        }
        public async Task<bool> DeleteHistoryItem(string Filename)
        {
            bool ret = false;

            try
            {
                Stopwatch sw = Stopwatch.StartNew();

                if (!this.IsSQLiteDBConnected())
                    await this.CreateConnectionAsync();

                History hist;
                ret = this.HistoryDic.TryRemove(Filename.ToLower(), out hist);

                //if (!ret)
                //{
                //	Global.Log($"Info: File was not in dictionary '{Filename}'");
                //}

                //Error: Cannot delete String: it has no PK [NotSupportedException] Mod: <DeleteHistoryItem>d__18 Line:150:5

                int dret = await this.sqlite_conn.DeleteAsync(Filename.ToLower());

                if (dret != 1)
                {
                    Global.Log($"Error: When trying to delete database entry '{Filename}', RowsDeleted count was {ret} but we expected 1. StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}");
                }

                if (sw.ElapsedMilliseconds > 1000)
                    Global.Log($"Warning: It took a long time to delete a history item @ {sw.ElapsedMilliseconds}ms, StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}: {Filename}");



            }
            catch (Exception ex)
            {

                if (ex.Message.ToLower().Contains("it has no pk"))
                {
                    //Global.Log($"Info: File was not in database '{Filename}' - " + ex.Message);
                }
                else
                {
                    Global.Log($"Error: StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}: '{Filename}' - " + Global.ExMsg(ex));
                }
            }

            if (ret)
            {
                this.DeletedCount.AtomicIncrementAndGet();
                this.LastUpdateTime = DateTime.Now;
                this.HasChanged.WriteFullFence(true);
            }

            return ret;

        }

        public async Task<bool> MigrateHistoryCSV(string Filename)
        {

            bool ret = false;

            try
            {

                if (!this.IsSQLiteDBConnected())
                    await this.CreateConnectionAsync();

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
                            foreach (var val in result)
                            {

                                History hist = new History().CreateFromCSV(val);
                                if (File.Exists(hist.Filename))
                                {
                                    if (await this.InsertHistoryItem(hist))
                                    {
                                        added++;
                                        this.AddedCount.AtomicIncrementAndGet();
                                    }
                                }
                            }

                            ret = (added > 0);

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
            if (!this.ReadOnly)
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
            
            return false;

        }

        public async Task<bool> UpdateHistoryList(bool Clean)
        {

            bool ret = false;

            try
            {
                Stopwatch sw = Stopwatch.StartNew();

                if (!this.IsSQLiteDBConnected())
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
                        foreach (History hist in CurList)
                    {
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

                    if (Clean)
                        await this.CleanHistoryList();


                });

                ret = (added > 0 || removed > 0);

                this.HasChanged.WriteFullFence(false);

                Global.Log($"Update History Database: Added={added}, removed={removed}, total={this.HistoryDic.Count}, StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count} in {sw.ElapsedMilliseconds}ms");





            }
            catch (Exception ex)
            {

                Global.Log("Error: " + Global.ExMsg(ex));
            }

            return ret;

        }

        public async Task<bool> CleanHistoryList()
        {

            bool ret = false;

            try
            {
                Stopwatch sw = Stopwatch.StartNew();

                Global.Log("Removing missing files from database...");

                if (!this.IsSQLiteDBConnected())
                    await this.CreateConnectionAsync();


                ConcurrentBag<string> removed = new ConcurrentBag<string>();   //this may only need to be a list, but just being safe in threading


                //run the file exists check in another thread so we dont freeze the UI, but WAIT for it
                await Task.Run(async () =>
                {
                    //we are allowed to do this with a ConcurrentDictionary...
                    foreach (History hist in this.HistoryDic.Values)
                    {
                        if (!File.Exists(hist.Filename))
                        {
                            removed.Add(hist.Filename.ToLower());
                            History rhist;
                            this.HistoryDic.TryRemove(hist.Filename, out rhist);
                        }

                    }

                    if (removed.Count > 0)
                    {
                        //start another thread to finish cleaning the database so that the app gets the list faster...
                        //the db should be thread safe due to opening with fullmutex flag
                        //DONT WAIT TO FINISH
                        Task.Run(async () =>
                        {
                            Stopwatch swr = Stopwatch.StartNew();
                            int rcnt = 0;
                            foreach (string fil in removed)
                            {
                                //the db should be thread safe due to opening with fullmutex flag
                                int rowsdeleted = await this.sqlite_conn.DeleteAsync(fil);
                                rcnt = rcnt + rowsdeleted;
                                if (rowsdeleted != 1)
                                {
                                    Global.Log($"...Error: When trying to delete database entry, RowsDeleted count was {rowsdeleted} but we expected 1.");
                                }
                            }
                            Global.Log($"...Cleaned {removed.Count} history DATABASE items because file did not exist in {swr.ElapsedMilliseconds}ms");

                        });
                    }


                });


                Global.Log($"...Cleaned {removed.Count} in-memory history items because files did not exist in {sw.ElapsedMilliseconds}ms");


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
                    if (this.IsSQLiteDBConnected())
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

