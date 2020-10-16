using SQLite;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static AITool.AITOOL;


namespace AITool
{
    //this is mainly so we only need one thread for adding and deleting
    public class DBQueueHistoryItem
    {
        public bool add = false;  //otherwise delete
        public History hist = new History();
        public DBQueueHistoryItem(History hist, bool add)
        {
            this.add = add;
            this.hist = hist;
        }
    }
    public class SQLiteHistory:IDisposable
    {
        private bool disposedValue;

        public string Filename { get; } = "";
        public bool ReadOnly { get; } = false;
        public ConcurrentDictionary<string, History> HistoryDic { get; } = new ConcurrentDictionary<string, History>();
        public DateTime InitializeTime { get; } = DateTime.Now;
        public DateTime LastUpdateTime { get; set; } = DateTime.MinValue;
        public ThreadSafe.Boolean HasInitialized { get; set; } = new ThreadSafe.Boolean(false);
        private SQLiteConnection sqlite_conn { get; set; } = null;
        public ConcurrentBag<History> RecentlyAdded { get; set; } = new ConcurrentBag<History>();
        public ConcurrentBag<History> RecentlyDeleted { get; set; } = new ConcurrentBag<History>();
        public ThreadSafe.Integer AddedCount { get; set; } = new ThreadSafe.Integer(0);
        public ThreadSafe.Integer DeletedCount { get; set; } = new ThreadSafe.Integer(0);
        //private ThreadSafe.Boolean IsUpdating { get; set; } = new ThreadSafe.Boolean(false);
        private BlockingCollection<DBQueueHistoryItem> DBQueueHistory = new BlockingCollection<DBQueueHistoryItem>();
        public MovingCalcs AddTimeCalc { get; set; } = new MovingCalcs(1000);
        public MovingCalcs DeleteTimeCalc { get; set; } = new MovingCalcs(1000);

        public static object DBLock = new object();

        public SQLiteHistory(string Filename, bool ReadOnly)
        {
            if (string.IsNullOrEmpty(Filename))
                throw new System.ArgumentException("Parameter cannot be empty", nameof(Filename));

            this.Filename = Filename;
            this.ReadOnly = ReadOnly;

            Task.Run(Initialize);

        }
        private void Initialize()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is left.

            try
            {
                UpdateHistoryList(true);

                if (this.HistoryDic.Count == 0)
                    this.MigrateHistoryCSV(AppSettings.Settings.HistoryFileName);

                Task.Run(HistoryJobQueueLoop);

            }
            catch (Exception ex)
            {
                Log("Error: " + Global.ExMsg(ex));
            }
            finally
            {
                this.HasInitialized.WriteFullFence(true);
                Global.SendMessage(MessageType.DatabaseInitialized);
            }

        }

        private async void HistoryJobQueueLoop()
        {
            //this runs forever and blocks if nothing is in the queue

            foreach (DBQueueHistoryItem hitm in DBQueueHistory.GetConsumingEnumerable())
            {
                string file = ""; 
                try
                {
                    if (hitm == null || hitm.hist == null || string.IsNullOrEmpty(hitm.hist.Filename))
                    {
                        Log("Error: hist should not be null?");
                    }
                    else
                    {
                        file = hitm.hist.Filename;
                        if (hitm.add)
                            this.InsertHistoryItem(hitm.hist);
                        else
                            this.DeleteHistoryItem(hitm.hist);  //Getting nullreferenceexception here and cant figure out why
                    }
                }
                catch (Exception ex)
                {

                    Log($"Error: ({file})" + ex.ToString());
                }

            }
            
            Log($"Error: Should not have left HistoryJobQueueLoop?");

        }

        private bool IsSQLiteDBConnected()
        {

            //make sure only one thread updating at a time
            //await Semaphore_Updating.WaitAsync();

            try
            {
                return (sqlite_conn != null && !string.IsNullOrEmpty(sqlite_conn.DatabasePath));
            }
            catch (Exception ex)
            {
                Log("Error: " + Global.ExMsg(ex));

                return false;
            }
            finally
            {
                //Semaphore_Updating.Release();
            }
        }

        private void DisposeConnection()
        {
            try
            {
                lock (DBLock)
                {
                    sqlite_conn.Close();
                    sqlite_conn = null;
                }

            }
            catch (Exception ex)
            {
                Log("Error: " + Global.ExMsg(ex));

            }
        }
        private bool CreateConnection()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is left.

            bool ret = false;

            try
            {
                Stopwatch sw = Stopwatch.StartNew();

                Global.UpdateProgressBar("Initializing history database...", 1, 1);

                this.HistoryDic.Clear();
                this.RecentlyDeleted = new ConcurrentBag<History>();
                this.RecentlyAdded = new ConcurrentBag<History>();

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

                if (this.IsSQLiteDBConnected())
                {
                    this.DisposeConnection();
                }

                //If the database file doesn't exist, the default behaviour is to create a new file
                sqlite_conn = new SQLiteConnection(this.Filename, flags, true);


                //make sure table exists:
                CreateTableResult ctr = sqlite_conn.CreateTable<History>();


                sqlite_conn.ExecuteScalar<int>(@"PRAGMA journal_mode = 'WAL';", new object[] { });
                sqlite_conn.ExecuteScalar<int>(@"PRAGMA busy_timeout = 30000;", new object[] { });


                sw.Stop();

                Log($"Created connection to SQLite db v{sqlite_conn.LibVersionNumber} in {sw.ElapsedMilliseconds}ms - TableCreate='{ctr.ToString()}', Flags='{sflags}': {this.Filename}", "None", "None");


            }
            catch (Exception ex)
            {

                Log("Error: " + Global.ExMsg(ex), "None", "None");
            }


            return ret;
        }

        public bool InsertHistoryQueue(History hist)
        {
            //simply add to the queue
            DBQueueHistoryItem ditm = new DBQueueHistoryItem(hist, true);
            return this.DBQueueHistory.TryAdd(ditm);
        }

        private bool InsertHistoryItem(History hist)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is left.

            bool ret = false;
            int iret = 0;

            //make sure only one thread updating at a time
            //await Semaphore_Updating.WaitAsync();

            //this.IsUpdating.WriteFullFence(true);
            lock (DBLock)
            {

                try
                {
                    ret = this.HistoryDic.TryAdd(hist.Filename.ToLower(), hist);

                    if (!ret)
                    {
                        Log($"debug: File already existed in dictionary: {hist.Filename}", hist.AIServer, hist.Camera);
                    }

                    Stopwatch sw = Stopwatch.StartNew();

                    iret = this.sqlite_conn.Insert(hist);

                    this.AddTimeCalc.AddToCalc(sw.ElapsedMilliseconds);

                    if (iret != 1)
                    {
                        Log($"Error: When trying to insert database entry, RowsAdded count was {ret} but we expected 1. StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}", hist.AIServer, hist.Camera);
                    }

                    if (sw.ElapsedMilliseconds > 3000)
                        Log($"Debug: It took a long time to add a history item @ {sw.ElapsedMilliseconds}ms, (Count={AddTimeCalc.Count}, Min={AddTimeCalc.Min}ms, Max={AddTimeCalc.Max}ms, Avg={AddTimeCalc.Average.ToString("#####")}ms), StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}: {Filename}", hist.AIServer, hist.Camera);

                }
                catch (Exception ex)
                {

                    if (ex.Message.ToLower().Contains("UNIQUE constraint failed".ToLower()))
                    {
                        Log($"Debug: File was already in the database: {hist.Filename}", hist.AIServer, hist.Camera);
                    }
                    else
                    {
                        Log($"Error: StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}: {hist.Filename}: " + Global.ExMsg(ex), hist.AIServer, hist.Camera);
                    }
                }

                if (ret || iret > 0)
                {
                    this.RecentlyAdded.Add(hist);
                    this.AddedCount.AtomicIncrementAndGet();
                    this.LastUpdateTime = DateTime.Now;
                }

            }

            //Semaphore_Updating.Release();

            //this.IsUpdating.WriteFullFence(false);

            return ret;

        }

        public bool DeleteHistoryQueue(History hist)
        {
            //simply add to the queue
            DBQueueHistoryItem ditm = new DBQueueHistoryItem(hist, false);
            return this.DBQueueHistory.TryAdd(ditm);
        }
        public bool DeleteHistoryQueue(string Filename)
        {
                //simply add to the queue
                History hist;
                this.HistoryDic.TryGetValue(Filename.ToLower(), out hist);

                if (hist == null)
                    hist = new History().Create(Filename, DateTime.Now, "unknown", "", "", false, "","");

                DBQueueHistoryItem ditm = new DBQueueHistoryItem(hist, false);

                return this.DBQueueHistory.TryAdd(ditm);

        }

        private bool DeleteHistoryItem(History hist)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is left.

            bool ret = false;
            int dret = 0;
            lock (DBLock)
            {
                try
                {

                    History thist = null;
                    ret = this.HistoryDic.TryRemove(hist.Filename.ToLower(), out thist);

                    Stopwatch sw = Stopwatch.StartNew();
                    //Error: Cannot delete String: it has no PK [NotSupportedException] Mod: <DeleteHistoryItem>d__18 Line:150:5
                    //trying to use object rather than primarykey to delete

                    if (thist != null)
                        hist = thist;  //probably dont have to do this in order to delete from db

                    dret = this.sqlite_conn.Delete(hist);

                    this.DeleteTimeCalc.AddToCalc(sw.ElapsedMilliseconds);

                    //if (dret != 1)
                    //{
                    //    Log($"Error: When trying to delete database entry '{Filename}', RowsDeleted count was {ret} but we expected 1. StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}");
                    //}
                    //else
                    //{
                    //    Log($"Removed {dret} database entry for '{Filename}'");
                    //}

                    if (sw.ElapsedMilliseconds > 2000)
                        Log($"Debug: It took a long time to delete a history item @ {sw.ElapsedMilliseconds}ms, (Count={DeleteTimeCalc.Count}, Min={DeleteTimeCalc.Min}ms, Max={DeleteTimeCalc.Max}ms, Avg={DeleteTimeCalc.Average.ToString("#####")}ms), StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}: {Filename}", hist.AIServer, hist.Camera);

                }
                catch (Exception ex)
                {

                    Log($"Error: File='{hist.Filename}' - " + Global.ExMsg(ex), hist.AIServer, hist.Camera);
                }

                if (ret || dret > 0)
                {
                    this.DeletedCount.AtomicIncrementAndGet();
                    this.LastUpdateTime = DateTime.Now;
                    this.RecentlyDeleted.Add(hist);
                }

                //Semaphore_Updating.Release();

                //this.IsUpdating.WriteFullFence(false);

            }

            return ret;

        }

        public bool MigrateHistoryCSV(string Filename)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is left.

            bool ret = false;
            lock (DBLock)
            {
                try
                {

                    //if (!await this.IsSQLiteDBConnectedAsync())
                    //    await this.CreateConnectionAsync();

                    //run in another thread so we dont block UI
                    //await Task.Run(async () =>
                    //{

                    if (System.IO.File.Exists(Filename))
                    {
                        Global.UpdateProgressBar("Migrating history.csv...", 1, 1);

                        Log($"Migrating history list from {Filename} ...");

                        Stopwatch SW = Stopwatch.StartNew();

                        //delete obsolete entries from history.csv
                        //CleanCSVList(); //removed to load the history list faster

                        List<string> result = new List<string>(); //List that later on will be containing all lines of the csv file

                        Task<bool> tsk = Global.WaitForFileAccess(Filename);

                        if (tsk.Result)
                        {
                            //load all lines except the first line into List (the first line is the table heading and not an alert entry)
                            foreach (string line in System.IO.File.ReadAllLines(Filename).Skip(1))
                            {
                                result.Add(line);
                            }

                            Log($"...Found {result.Count} lines.");

                            List<string> itemsToDelete = new List<string>(); //stores all filenames of history.csv entries that need to be removed

                            //load all List elements into the ListView for each row
                            int added = 0;
                            int removed = 0;
                            int cnt = 0;
                            foreach (var val in result)
                            {
                                cnt++;

                                if (cnt == 1 || cnt == result.Count || (cnt % (result.Count / 10) > 0))
                                {
                                    Global.UpdateProgressBar("Migrating history.csv...", cnt, result.Count);
                                }

                                History hist = new History().CreateFromCSV(val);
                                if (File.Exists(hist.Filename))
                                {
                                    if (this.InsertHistoryItem(hist))
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
                            //this.DeletedCount.AtomicAddAndGet(removed);

                            //try to get a better feel how much time this function consumes - Vorlon
                            Log($"...Added {added} out of {result.Count} history items in {{yellow}}{SW.ElapsedMilliseconds}ms{{white}}, {this.HistoryDic.Count()} lines.");

                        }
                        else
                        {
                            Log($"Error: Could not gain access to history file for {SW.ElapsedMilliseconds}ms - {AppSettings.Settings.HistoryFileName}");

                        }

                    }
                    else
                    {
                        Log($"Old history file does not exist, could not migrate: {Filename}");
                    }


                    //});


                }
                catch (Exception ex)
                {

                    Log("Error: " + Global.ExMsg(ex));
                }

            }

            Global.UpdateProgressBar("", 0, 1);


            return ret;


        }

        public List<History> GetRecentlyDeleted()
        {
            List<History> ret = new List<History>();
            while (!this.RecentlyDeleted.IsEmpty)
            {
                History hist;
                if (this.RecentlyDeleted.TryTake(out hist))
                    ret.Add(hist);
            }
            return ret;
        }
        public List<History> GetRecentlyAdded()
        {
            List<History> ret = new List<History>();
            while (!this.RecentlyAdded.IsEmpty)
            {
                History hist;
                if (this.RecentlyAdded.TryTake(out hist))
                    ret.Add(hist);
            }
            return ret;
        }

        public async Task<bool> HasUpdates()
        {

            try
            {

                if (!this.ReadOnly)  //assume if we had to wait the database was just updating
                {
                    //we are running in the same process as the database updater
                    //no need to re-check sqlite db every time

                    if (!this.RecentlyAdded.IsEmpty || !this.RecentlyDeleted.IsEmpty)
                    {
                        return true;
                    }
                }
                else
                {
                    //do a full update for now, later figure out best way to communicate updates from service to gui
                    return await UpdateHistoryListAsync(false);
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

        public async Task<bool> UpdateHistoryListAsync(bool Clean)
        {
            return await Task.Run(() => UpdateHistoryList(Clean));
        }

        public bool UpdateHistoryList(bool Clean)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is left.

            bool ret = false;

            lock (DBLock)
            {
                try
                {
                    Global.UpdateProgressBar("Reading database...", 1, 1);

                    Stopwatch sw = Stopwatch.StartNew();

                    if (!this.IsSQLiteDBConnected())
                        this.CreateConnection();

                    TableQuery<History> query = sqlite_conn.Table<History>();
                    List<History> CurList = query.ToList();

                    int added = 0;
                    int removed = 0;
                    bool isnew = (this.HistoryDic.Count == 0);

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
                        this.CleanHistoryList();

                    ret = (added > 0 || removed > 0);

                    if (ret)
                    {
                        //this.DeletedCount.AtomicAddAndGet(removed);
                        if (!isnew)
                            this.AddedCount.AtomicAddAndGet(added);

                        this.LastUpdateTime = DateTime.Now;
                    }

                    Log($"Update History Database: Added={added}, removed={removed}, total={this.HistoryDic.Count}, StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count} in {sw.ElapsedMilliseconds}ms");

                }
                catch (Exception ex)
                {

                    Log("Error: " + Global.ExMsg(ex));
                }

            }

            Global.UpdateProgressBar("", 0, 1);


            return ret;

        }

        private async Task<bool> CleanHistoryList()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is left.

            bool ret = false;

            try
            {
                Stopwatch sw = Stopwatch.StartNew();


                if (!this.IsSQLiteDBConnected())
                    this.CreateConnection();

                ConcurrentBag<History> removed = new ConcurrentBag<History>();   //this may only need to be a list, but just being safe in threading

                //run the file exists check in another thread so we dont freeze the UI, but WAIT for it
                await Task.Run(async () =>
                {
                    Log("Removing missing files from in-memory database...");

                    int missing = 0;
                    int tooold = 0;
                    int cnt = 0;
                    int HistCount = this.HistoryDic.Count;
                    int shownum = 0;
                    if (HistCount > 0)
                        shownum = HistCount / 10;
                    //we are allowed to do this with a ConcurrentDictionary...
                    foreach (History hist in this.HistoryDic.Values)
                    {
                        cnt++;

                        if (cnt == 1 || cnt == HistCount || (cnt % shownum) > 0)
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
                                Log($"Warning: Could not remove from in-memory database: {hist.Filename}");
                        }

                    }

                    if (removed.Count > 0)
                    {
                        Log($"Removing {missing} files and {tooold} database entries that are older than {AppSettings.Settings.MaxHistoryAgeDays} days (MaxHistoryAgeDays) from file database...");
                        //start another thread to finish cleaning the database so that the app gets the list faster...
                        //the db should be thread safe due to opening with fullmutex flag
                        Stopwatch swr = Stopwatch.StartNew();
                        int rcnt = 0;
                        int failedcnt = 0;
                        cnt = 0;
                        foreach (History hist in removed)
                        {
                            cnt++;
                            int rnum = 0;
                            if (removed.Count > 0)
                                rnum = (removed.Count / 10);

                            if (cnt == 1 || cnt == removed.Count || (cnt % rnum > 0))
                            {
                                Global.UpdateProgressBar("Cleaning database (2 of 2)...", cnt, removed.Count);
                            }

                            int rowsdeleted = 0;

                            try
                            {
                                Stopwatch dsw = Stopwatch.StartNew();

                                rowsdeleted = this.sqlite_conn.Delete(hist);

                                this.DeleteTimeCalc.AddToCalc(dsw.ElapsedMilliseconds);

                                if (rowsdeleted != 1)
                                {
                                    failedcnt++;
                                    Log($"...Error: When trying to delete database entry, RowsDeleted count was {rowsdeleted} but we expected 1.");
                                }
                            }
                            catch (Exception ex)
                            {
                                failedcnt++;
                                Log($"Error: StackDepth={new StackTrace().FrameCount}, TID={Thread.CurrentThread.ManagedThreadId}, TCNT={Process.GetCurrentProcess().Threads.Count}: '{Filename}' - " + Global.ExMsg(ex));
                            }
                            rcnt = rcnt + rowsdeleted;
                        }

                        //this.DeletedCount.AtomicAddAndGet(rcnt);

                        Log($"...Cleaned {rcnt} of {removed.Count} (Failed={failedcnt}) history file database items because file did not exist in {swr.ElapsedMilliseconds}ms (Count={DeleteTimeCalc.Count}, Min={DeleteTimeCalc.Min}ms, Max={DeleteTimeCalc.Max}ms, Avg={DeleteTimeCalc.Average.ToString("#####")}ms)");

                    }
                    else
                    {
                        Log("debug: No missing files to clean from database?");
                    }


                });


                Log($"...Cleaned {removed.Count} items in {sw.ElapsedMilliseconds}ms");


                ret = removed.Count > 0;


            }
            catch (Exception ex)
            {

                Log("Error: " + ex.ToString());
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

