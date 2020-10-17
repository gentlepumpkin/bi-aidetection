using NLog;
using NLog.Common;
using NLog.Layouts;
using NLog.Targets;
using NLog.Targets.Wrappers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;

namespace AITool
{

    //this is for UI logging only, not file logging directly
    public class ClsLogManager:IDisposable
    {
        public List<ClsLogItm> Values { get; set; } = new List<ClsLogItm>();
        public ConcurrentBag<ClsLogItm> RecentlyAdded { get; set; } = new ConcurrentBag<ClsLogItm>();
        public ConcurrentBag<ClsLogItm> RecentlyDeleted { get; set; } = new ConcurrentBag<ClsLogItm>();
        public ThreadSafe.Integer ErrorCount { get; set; } = new ThreadSafe.Integer(0);
        public ClsLogItm LastLogItm = new ClsLogItm();
        public int MaxGUILogItems { get; set; } = 10000;
        private int _LastIDX = 0;
        private bool _Store;
        private ThreadSafe.Integer _CurDepth = new ThreadSafe.Integer(0);
        private object _LockObj = new object();

        private LogLevel MinLevel = LogLevel.Debug;
        private string _Filename = "";
        private long _MaxSize = 0;
        private int _MaxAgeDays = 0;
        private string _LastSource = "";
        private string _LastCamera = "";
        private string _LastAIServer = "";

        private NLog.Logger NLogFileWriter = null;
        AsyncTargetWrapper NLogAsyncWrapper = null;


        public ClsLogManager(bool Store, string DefaultSource, LogLevel MinLevel, string Filename, int MaxSize, int MaxAgeDays, int MaxGUILogItems)
        {
            this._Store = Store;  //we wont store log entries when running as a service, its only for the GUI
            this._LastSource = DefaultSource;
            this.UpdateNLog(MinLevel, Filename, MaxSize, MaxAgeDays, MaxGUILogItems);
        }

        public async void UpdateNLog(LogLevel MinLevel, string Filename, long MaxSize, int MaxAgeDays, int MaxGUILogItems)
        {

            this.MaxGUILogItems = MaxGUILogItems;

            bool needsupdating = this.NLogFileWriter == null || this.NLogAsyncWrapper == null || MinLevel != this.MinLevel || Filename != this._Filename || MaxSize != this._MaxSize || MaxAgeDays != this._MaxAgeDays;

            if (!needsupdating)
                return;

            if (this.NLogAsyncWrapper == null)
                this.NLogAsyncWrapper = new AsyncTargetWrapper();

            //if we change the logging level only, I dont want to re-initialize everything else...

            bool onlylevel = this.NLogFileWriter != null && this.NLogAsyncWrapper != null && MinLevel != this.MinLevel && Filename == this._Filename && MaxSize == this._MaxSize && MaxAgeDays == this._MaxAgeDays;

            if (onlylevel)
            {
                this.MinLevel = MinLevel;
                foreach (var rule in LogManager.Configuration.LoggingRules)
                {
                    rule.EnableLoggingForLevel(this.MinLevel);
                }

                //Call to update existing Loggers created with GetLogger() or 
                //GetCurrentClassLogger()
                LogManager.ReconfigExistingLoggers();
                return;
            }

            this.MinLevel = MinLevel;
            this._Filename = Filename;
            this._MaxAgeDays = MaxAgeDays;
            this._MaxSize = MaxSize;

            // Targets where to log to: File and Console
            var FileTarget = new NLog.Targets.FileTarget("logfile"); // { FileName = AppSettings.Settings.LogFileName };

            FileTarget.FileName = Filename;
            FileTarget.ArchiveAboveSize = MaxSize;
            FileTarget.ArchiveEvery = NLog.Targets.FileArchivePeriod.Day;
            FileTarget.MaxArchiveDays = MaxAgeDays;
            FileTarget.ArchiveNumbering = NLog.Targets.ArchiveNumberingMode.Date;
            FileTarget.ArchiveOldFileOnStartup = true;
            FileTarget.KeepFileOpen = false;
            FileTarget.CreateDirs = true;
            FileTarget.Header = "Date|Level|Source|Func|AIServer|Camera|Detail|Idx|Depth|Color|ThreadID";
            FileTarget.EnableArchiveFileCompression = true;
            FileTarget.Layout = "${message}";  //nothing fancy we are doing it ourselves

            this.NLogAsyncWrapper.WrappedTarget = FileTarget;
            this.NLogAsyncWrapper.QueueLimit = 100;
            this.NLogAsyncWrapper.OverflowAction = AsyncTargetWrapperOverflowAction.Discard;
            this.NLogAsyncWrapper.Name = "NLogAsyncWrapper";
            
            // Rules for mapping loggers to targets            
            NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(this.NLogAsyncWrapper, this.MinLevel);
            NLog.LogManager.AutoShutdown = true;

            this.NLogFileWriter = NLog.LogManager.GetCurrentClassLogger();

            //load the current log file if it exists...
            //if (this.Values.Count == 0)
            //    await this.LoadLogFile(Filename, true);

        }

        public List<ClsLogItm> GetRecentlyDeleted()
        {
            List<ClsLogItm> ret = new List<ClsLogItm>();
            while (!this.RecentlyDeleted.IsEmpty)
            {
                if (this.RecentlyDeleted.TryTake(out ClsLogItm CLI))
                    ret.Add(CLI);
            }
            return ret;
        }
        public List<ClsLogItm> GetRecentlyAdded()
        {
            List<ClsLogItm> ret = new List<ClsLogItm>();
            while (!this.RecentlyAdded.IsEmpty)
            {
                if (this.RecentlyAdded.TryTake(out ClsLogItm CLI))
                    ret.Add(CLI);
            }
            return ret;
        }
        public void Clear()
        {
            lock (this._LockObj)
            {
                this.Values.Clear();
                this.LastLogItm = new ClsLogItm();
                this._LastIDX = 0;
                this.ErrorCount.WriteFullFence(0);
            }
        }
               

        public void Add(ClsLogItm CDI)
        {
            lock (this._LockObj)
            {
                this._LastIDX += 1;
                this.LastLogItm = CDI;
                CDI.Idx = this._LastIDX;
                if (this._Store)
                    this.Values.Add(this.LastLogItm);
            }
        }

        public void AddRange(List<ClsLogItm> CDIList)
        {
            lock (this._LockObj)
            {
                foreach (ClsLogItm CDI in CDIList)
                {
                    if (!this.Values.Contains(CDI))
                    {
                        this._LastIDX += 1;
                        this.LastLogItm = CDI;
                        CDI.Idx = this._LastIDX;
                        if (this._Store)
                            this.Values.Add(this.LastLogItm);
                    }
                }
            }
        }

        public void Enter([CallerMemberName()] string memberName = null)
        {
            this._CurDepth.AtomicIncrementAndGet();
            
            if (this._CurDepth.ReadFullFence() > 10)
                this._CurDepth.WriteFullFence(10);  //just in case something weird is going on

            if (this.MinLevel == LogLevel.Trace)
                this.Log($"---->ENTER {memberName}, Depth={this._CurDepth.ReadFullFence()}", "Trace-Enter", "Trace-Enter", "", 0, LogLevel.Trace, DateTime.Now, memberName);

        }
        public void Exit([CallerMemberName()] string memberName = null, long timems = 0)
        {
            this._CurDepth.AtomicDecrementAndGet();
            if (this._CurDepth.ReadFullFence() < 0)
                this._CurDepth.WriteFullFence(0);

            if (this.MinLevel == LogLevel.Trace)
                this.Log($"----<EXIT {memberName}, Time={timems}ms, Depth={this._CurDepth.ReadFullFence()}", "Trace-Exit", "Trace-Exit", "", 0, LogLevel.Trace, DateTime.Now, memberName);
        }
        public ClsLogItm Log(string Detail, string AIServer = "", string Camera = "", string Source = "", int Depth = 0, LogLevel Level = null, Nullable<DateTime> Time = default(DateTime?), [CallerMemberName()] string memberName = null)
        {

            lock (this._LockObj)
            {
                this._LastIDX += 1;
                this.LastLogItm = new ClsLogItm();
                this.LastLogItm.Detail = Detail;
                
                this.LastLogItm.ThreadID = Thread.CurrentThread.ManagedThreadId;

                if (Source == null || string.IsNullOrWhiteSpace(Source))
                    this.LastLogItm.Source = this._LastSource;
                else
                    this.LastLogItm.Source = Source;

                if (Camera == null || string.IsNullOrWhiteSpace(Camera))
                    this.LastLogItm.Camera = this._LastCamera;
                else
                {
                    this.LastLogItm.Camera = Camera;
                    if (!Camera.StartsWith("Trace-"))
                        this._LastCamera = Camera;
                }

                if (AIServer == null || string.IsNullOrWhiteSpace(AIServer))
                    this.LastLogItm.AIServer = this._LastAIServer;
                else
                {
                    this.LastLogItm.AIServer = AIServer;
                    if (!AIServer.StartsWith("Trace-"))
                        this._LastAIServer = AIServer;
                }

                if (Time.HasValue)
                    this.LastLogItm.Date = Time.Value;
                else
                    this.LastLogItm.Date = DateTime.Now;

                if (memberName == ".ctor")
                    memberName = "Constructor";

                this.LastLogItm.Func = memberName.Replace("AITool.","");
                
                if (Level == null)
                {
                    if (this.LastLogItm.Detail.IndexOf("fatal:", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        Level = LogLevel.Fatal;
                    }
                    else if (this.LastLogItm.Detail.IndexOf("error:", StringComparison.OrdinalIgnoreCase) >= 0)
                        Level = LogLevel.Error;
                    else if (this.LastLogItm.Detail.IndexOf("warning:", StringComparison.OrdinalIgnoreCase) >= 0 || this.LastLogItm.Detail.IndexOf("warn:", StringComparison.OrdinalIgnoreCase) >= 0)
                        Level = LogLevel.Warn;
                    else if (this.LastLogItm.Detail.IndexOf("info:", StringComparison.OrdinalIgnoreCase) >= 0)
                        Level = LogLevel.Info;
                    else if (this.LastLogItm.Detail.IndexOf("debug:", StringComparison.OrdinalIgnoreCase) >= 0)
                        Level = LogLevel.Debug;
                    else if (this.LastLogItm.Detail.IndexOf("trace:", StringComparison.OrdinalIgnoreCase) >= 0)
                        Level = LogLevel.Trace;
                    else
                        Level = LogLevel.Info;
                }

                bool HasError = false;
                
                    //remove tags
                if (Level == LogLevel.Error)
                {
                    this.ErrorCount.AtomicIncrementAndGet();
                    HasError = true;
                    this.LastLogItm.Detail = Global.ReplaceCaseInsensitive(this.LastLogItm.Detail, "error:", "");
                }
                else if (Level == LogLevel.Fatal)
                {
                    this.ErrorCount.AtomicIncrementAndGet();
                    HasError = true;
                    this.LastLogItm.Detail = Global.ReplaceCaseInsensitive(this.LastLogItm.Detail, "fatal:", "");
                }
                else if (Level == LogLevel.Warn)
                {
                    this.ErrorCount.AtomicIncrementAndGet();
                    HasError = true;
                    this.LastLogItm.Detail = Global.ReplaceCaseInsensitive(this.LastLogItm.Detail, "warn:", "");
                    this.LastLogItm.Detail = Global.ReplaceCaseInsensitive(this.LastLogItm.Detail, "warning:", "");
                }
                else if (Level == LogLevel.Info)
                {
                    this.LastLogItm.Detail = Global.ReplaceCaseInsensitive(this.LastLogItm.Detail, "info:", "");
                }
                else if (Level == LogLevel.Trace)
                {
                    this.LastLogItm.Detail = Global.ReplaceCaseInsensitive(this.LastLogItm.Detail, "trace:", "");
                }
                else if (Level == LogLevel.Debug)
                {
                    this.LastLogItm.Detail = Global.ReplaceCaseInsensitive(this.LastLogItm.Detail, "debug:", "");
                }

                if (this.LastLogItm.Detail.TrimStart().StartsWith("{"))
                {
                    this.LastLogItm.Color = Global.GetWordBetween(this.LastLogItm.Detail, "{", "}");
                    //strip out the color definition
                    this.LastLogItm.Detail = Global.ReplaceCaseInsensitive(this.LastLogItm.Detail,"{"+ this.LastLogItm.Color + "}","");
                }

                //clean out any whitespace
                //this.LastLogItm.Detail = this.LastLogItm.Detail.TrimStart();

                if (this._CurDepth.ReadFullFence() + Depth > 0)
                    this.LastLogItm.Detail = new string(' ', (this._CurDepth.ReadFullFence() + Depth * 2)) + this.LastLogItm.Detail;

                this.LastLogItm.Depth = this._CurDepth.ReadFullFence() + Depth;
                this.LastLogItm.Level = Level;
                this.LastLogItm.Idx = _LastIDX;

                if (this._Store)
                {
                    if (Level >= this.MinLevel)
                    {
                        this.Values.Add(this.LastLogItm);
                        this.RecentlyAdded.Add(this.LastLogItm);
                        //keep the log list size down
                        if (Values.Count > this.MaxGUILogItems)
                        {
                            this.RecentlyDeleted.Add(Values[0]);
                            Values.RemoveAt(0);
                        }
                    }
                }

                this.NLogFileWriter.Log(Level, this.LastLogItm.ToString());
                
                if (Debugger.IsAttached)
                    Console.WriteLine(this.LastLogItm.ToDetailString());
                
                string itm = this.LastLogItm.ToString();
                if (AppSettings.Settings.send_errors == true && (HasError) && AppSettings.Settings.telegram_chatids.Count > 0 && AITOOL.TriggerActionQueue != null && !(itm.IndexOf("telegram", StringComparison.OrdinalIgnoreCase) >= 0) && !(itm.IndexOf("addtriggeraction", StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    //await TelegramText($"[{time}]: {text}"); //upload text to Telegram
                    AITOOL.TriggerActionQueue.AddTriggerActionAsync(TriggerType.TelegramText, null, null, null, true, false, null, this.LastLogItm.ToDetailString());

                }

                if (HasError)
                    NLog.LogManager.Flush();
                
            }

            return this.LastLogItm;
        }
        public void Sort()
        {
            lock (this._LockObj)
                this.Values = this.Values.OrderBy(c => c.Date).ThenBy(c => c.Idx).ToList();
        }

        public async Task<List<ClsLogItm>> LoadLogFile(string Filename, bool Import)
        {
            List<ClsLogItm> ret = new List<ClsLogItm>();
            if (File.Exists(Filename))
            {
                bool success = await Global.WaitForFileAccessAsync(Filename, FileSystemRights.Read, FileShare.Read, 30000, 20);
                if (success)
                {
                    int Invalid = 0;
                    bool OldFile = false;
                    using (StreamReader sr = new StreamReader(Filename))
                    {
                        int cnt = 0;
                        while (!sr.EndOfStream)
                        {
                            cnt++;
                            if (cnt > 1)
                            {
                                string line = await sr.ReadLineAsync();

                                if (!OldFile && line.TrimStart().StartsWith("["))  //old log format, ignore
                                {
                                    OldFile = true;
                                    this._LastIDX = 0;
                                    break;
                                }

                                if (!Import)
                                {
                                    ClsLogItm CLI = new ClsLogItm(line);
                                    if (CLI.Level != LogLevel.Off)  //off indicates invalid - for example the old log format
                                    {
                                        CLI.FromFile = true;
                                        ret.Add(CLI);
                                    }
                                    else
                                    {
                                        Invalid++;
                                        if (Invalid > 10)
                                        {
                                            ret.Clear();
                                            this._LastIDX = 0;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    //load into current log manager
                                    if (this._Store)
                                    {
                                        ClsLogItm CLI = new ClsLogItm(line);
                                        if (CLI.Level != LogLevel.Off)  //off indicates invalid - for example the old log format
                                        {
                                            this.LastLogItm = CLI;
                                            if (this.LastLogItm.Level >= this.MinLevel)
                                            {
                                                CLI.FromFile = true;
                                                this.Values.Add(this.LastLogItm);
                                                this.RecentlyAdded.Add(this.LastLogItm);
                                                //keep the log list size down
                                                if (Values.Count > this.MaxGUILogItems)
                                                {
                                                    this.RecentlyDeleted.Add(Values[0]);
                                                    Values.RemoveAt(0);
                                                }
                                            }

                                        }
                                        else
                                        {
                                            Invalid++;
                                            if (Invalid > 10)
                                            {
                                                ret.Clear();
                                                this._LastIDX = 0;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                    
                    if (OldFile)
                    {
                        //rename it to keep it out of our way next time
                        try
                        {
                            File.Move(Filename,Filename+".OLDLOGFORMAT");
                        }
                        catch { }
                    }
                }
            }

            return ret;
        }

        public async void Dispose()
        {
            NLog.LogManager.Flush();
            ((IDisposable)NLogAsyncWrapper).Dispose();
        }
    }

}
