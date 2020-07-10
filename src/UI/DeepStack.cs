using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;


namespace WindowsFormsApp2
{
    public class DeepStack
    {

        public string AdminKey = "";
        public string APIKey = "";
        public string Port = "81";
        public string Mode = "Medium";
        public string DeepStackFolder = @"C:\DeepStack";
        public string DeepStackEXE = @"C:\DeepStack\DeepStack.exe";
        public string ServerEXE = @"C:\DeepStack\server\server.exe";
        public string PythonEXE = @"C:\DeepStack\interpreter\python.exe";
        public string RedisEXE = @"C:\DeepStack\redis\redis-server.exe";
        public bool SceneAPIEnabled = false;
        public bool FaceAPIEnabled = false;
        public bool DetectionAPIEnabled = true;
        public bool IsStarted = false;
        public bool HasError = false;
        public bool IsInstalled = false;
        public bool IsActivated = false;
        public bool NeedsSaving = false;
        public SharedFunctions.ClsProcess DeepStackProc;
        public SharedFunctions.ClsProcess ServerProc;
        public SharedFunctions.ClsProcess PythonProc;
        public SharedFunctions.ClsProcess RedisProc;
        public List<double> ResponseTimeList = new List<double>();  //From this you can get min/max/avg
        public IProgress<string> ProgressEventLogger = null;

        private void LogProgress(string Message, [CallerMemberName] string memberName = null)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(Message) || this.ProgressEventLogger == null)
                {
                    return;
                }

                //Console.WriteLine(Message);
                if (Message.ToLower().Contains("visit localhost to activate deepstack"))
                {
                    this.IsActivated = false;
                }
                else if (Message.ToLower().Contains("deepstack is active"))
                {
                    this.IsActivated = true;
                }

                //Keep a list of the times for each image to be processed - later can use List.Min/max/avg if needed
                string[] splt = Message.Split("|".ToCharArray());
                if (splt.Count() >= 4)
                {
                    double tim = 0;
                    if (splt[2].Contains("ms"))
                    {
                        tim = Convert.ToDouble(splt[2].Replace("ms", "").Trim());
                    }
                    else if (splt[2].Contains("ms"))
                    {
                        tim = Convert.ToInt32(splt[2].Replace("s", "").Trim());
                        tim = TimeSpan.FromSeconds(tim).TotalMilliseconds;
                    }
                    if (tim > 0)
                    {
                        this.ResponseTimeList.Add(tim);
                    }
                }

                this.ProgressEventLogger.Report($"{memberName}> {Message}");

            }
            catch (Exception ex)
            {

                throw;
            }


        }
        public DeepStack(string AdminKey, string APIKey, string Mode, bool SceneAPIEnabled, bool FaceAPIEnabled, bool DetectionAPIEnabled, string Port, IProgress<string> ProgressEventLogger)
        {

            this.ProgressEventLogger = ProgressEventLogger;
            this.Update(AdminKey, APIKey, Mode, SceneAPIEnabled, FaceAPIEnabled, DetectionAPIEnabled, Port);

        }

        public void Update(string AdminKey, string APIKey, string Mode, bool SceneAPIEnabled, bool FaceAPIEnabled, bool DetectionAPIEnabled, string Port)
        {
            this.AdminKey = AdminKey;
            this.APIKey = APIKey;
            this.SceneAPIEnabled = SceneAPIEnabled;
            this.FaceAPIEnabled = FaceAPIEnabled;
            this.DetectionAPIEnabled = DetectionAPIEnabled;
            this.Port = Port;
            this.Mode = Mode;

            bool found = RefreshInfo();

        }
        public bool GetBlueStackRunningProcesses()
        {
            bool Ret = false;

            //Note - deepstack.exe does NOT need to be running
            this.DeepStackProc = SharedFunctions.GetaProcessByFullPath(this.DeepStackEXE);
            this.ServerProc = SharedFunctions.GetaProcessByFullPath(this.ServerEXE);
            this.PythonProc = SharedFunctions.GetaProcessByFullPath(this.PythonEXE);
            this.RedisProc = SharedFunctions.GetaProcessByFullPath(this.RedisEXE);

            if (this.ServerProc.process != null && this.PythonProc.process != null && this.RedisProc.process != null)
            {
                //access denied may happen
                bool HasExited = false;
                try
                {
                    HasExited = this.ServerProc.process.HasExited;
                }
                catch (Exception ex)
                {

                    LogProgress("Error: While trying to access DeepStack server.exe process, got: " + SharedFunctions.ExMsg(ex));
                }
                this.IsInstalled = true;
                if (!HasExited)
                {
                    this.HasError = false;
                    LogProgress("DeepStack Desktop IS running from " + ServerProc.FileName);

                    this.IsStarted = true;
                    //C:\DeepStack\server\server.exe
                    //check to see if it is a different path than default
                    if (!this.ServerProc.FileName.ToLower().StartsWith(this.DeepStackFolder.ToLower()))
                    {
                        string dspath = this.ServerProc.FileName.ToLower().Replace(@"server\server.exe", "");
                        LogProgress("Deepstack running from non-default path: " + dspath);
                        this.DeepStackFolder = dspath;
                        this.DeepStackEXE = Path.Combine(this.DeepStackFolder, @"DeepStack.exe");
                        this.PythonEXE = Path.Combine(this.DeepStackFolder, @"interpreter\python.exe");
                        this.RedisEXE = Path.Combine(this.DeepStackFolder, @"redis\redis-server.exe");
                        this.ServerEXE = Path.Combine(this.DeepStackFolder, @"server\server.exe");
                        this.NeedsSaving = true;
                    }

                    //Try to get command line params to fill in correct running port, etc
                    //"C:\DeepStack\server\server.exe" -VISION-FACE=False -VISION-SCENE=True -VISION-DETECTION=True -ADMIN-KEY= -API-KEY= -PORT=84

                    string face = SharedFunctions.GetWordBetween(this.ServerProc.CommandLine, "-VISION-FACE=", " |-");
                    if (!string.IsNullOrEmpty(face))
                        if (this.FaceAPIEnabled != Convert.ToBoolean(face))
                        {
                            LogProgress($"...Face API detection setting found in running server.exe process changed from '{this.FaceAPIEnabled}' to '{Convert.ToBoolean(face)}'");
                            this.FaceAPIEnabled = Convert.ToBoolean(face);
                            this.NeedsSaving = true;
                        }

                    string scene = SharedFunctions.GetWordBetween(this.ServerProc.CommandLine, "-VISION-SCENE=", " |-");
                    if (!string.IsNullOrEmpty(scene))
                        if (Convert.ToBoolean(scene) != this.SceneAPIEnabled) {
                            LogProgress($"...Scene API detection setting found in running server.exe process changed from '{this.SceneAPIEnabled}' to '{Convert.ToBoolean(scene)}'");
                            this.SceneAPIEnabled = Convert.ToBoolean(scene);
                            this.NeedsSaving = true;
                        };
                    
                    string detect = SharedFunctions.GetWordBetween(this.ServerProc.CommandLine, "-VISION-DETECTION=", " |-");
                    if (!string.IsNullOrEmpty(detect))
                        if (this.DetectionAPIEnabled != Convert.ToBoolean(detect))
                        {
                            LogProgress($"...Detection API detection setting found in running server.exe process changed from '{this.DetectionAPIEnabled}' to '{Convert.ToBoolean(detect)}'");
                            this.DetectionAPIEnabled = Convert.ToBoolean(detect);
                            this.NeedsSaving = true;
                        }

                    string admin = SharedFunctions.GetWordBetween(this.ServerProc.CommandLine, "-ADMIN-KEY=", " |-");
                    if (!string.IsNullOrEmpty(admin))
                        if (this.AdminKey != admin)
                        {
                            LogProgress($"...Admin key setting found in running server.exe process changed from '{this.AdminKey}' to '{admin}'");
                            this.AdminKey = admin;
                            this.NeedsSaving = true;
                        }

                    string api = SharedFunctions.GetWordBetween(this.ServerProc.CommandLine, "-API-KEY=", " |-");
                    if (!string.IsNullOrEmpty(api))
                        if (this.APIKey != api)
                        {
                            LogProgress($"...API key setting found in running server.exe process changed from '{this.APIKey}' to '{api}'");
                            this.APIKey = api;
                            this.NeedsSaving = true;
                        }

                    string port = SharedFunctions.GetWordBetween(this.ServerProc.CommandLine, "-PORT=", " |-");
                    if (!string.IsNullOrEmpty(port))
                        if (this.Port != port)
                        {
                            LogProgress($"...Port setting found in running server.exe process changed from '{this.Port}' to '{port}'");
                            this.Port = port;
                            this.NeedsSaving = true;
                        }

                    //Get mode:
                    //"C:\DeepStack\interpreter\python.exe" ../intelligence.py -MODE=Medium -VFACE=False -VSCENE=True -VDETECTION=True

                    string mode = SharedFunctions.GetWordBetween(this.PythonProc.CommandLine, "-MODE=", " |-");
                    if (!string.IsNullOrEmpty(port))
                        if (this.Mode != mode)
                        {
                            LogProgress($"...Mode setting found in running python.exe process changed from '{this.Mode}' to '{mode}'");
                            this.Mode = mode;
                            this.NeedsSaving = true;
                        }


                    //"C:\DeepStack\interpreter\python.exe" "-c" "from multiprocessing.spawn import spawn_main; spawn_main(parent_pid=17744, pipe_handle=328)" "--multiprocessing-fork"

                }
                else
                {
                    LogProgress("DeepStack Desktop NOT running.");
                    this.IsStarted = false;
                    this.PythonProc.process = null;
                    this.RedisProc.process = null;
                    this.ServerProc.process = null;
                    this.DeepStackProc.process = null;
                }
            }
            else if (this.ServerProc.process != null || this.PythonProc.process != null || this.RedisProc.process != null)
            {
                LogProgress("Error: Deepstack partially running.  You many need to manually kill server.exe, python.exe, redis-server.exe");
                this.HasError = true;
            }
            else
            {
                LogProgress("DeepStack Desktop NOT running.");
                this.IsStarted = false;
                this.HasError = false;
            }

            return Ret;
        }
        public bool RefreshInfo()
        {
            bool Ret = false;
            this.IsInstalled = false;
            RegistryKey key = null;

            try
            {

                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\{B976B0A1-C83C-4735-AC7F-196922A2748B}_is1");

                if (key == null)
                    //Try the 64 bit version of the registry...  
                    key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{B976B0A1-C83C-4735-AC7F-196922A2748B}_is1");

                if (key != null)
                {
                    string dspath = (string)key.GetValue("Inno Setup: App Path");
                    if (!string.IsNullOrWhiteSpace(dspath))
                    {
                        LogProgress("Deepstack Desktop install path found in Uninstall registry: " + dspath);

                        string exepth = Path.Combine(dspath, "DeepStack.exe");
                        if (File.Exists(exepth))
                        {
                            this.IsInstalled = true;
                            this.DeepStackFolder = dspath;
                            this.DeepStackEXE = exepth;
                            if (dspath.ToLower() != this.DeepStackFolder.ToLower())
                            {
                                LogProgress("Deepstack running from non-default path: " + dspath);
                                this.PythonEXE = Path.Combine(dspath, @"interpreter\python.exe");
                                this.RedisEXE = Path.Combine(dspath, @"redis\redis-server.exe");
                                this.ServerEXE = Path.Combine(dspath, @"server\server.exe");
                                this.NeedsSaving = true;
                            }
                        }
                        else
                        {
                            LogProgress("Error: File not found " + exepth);
                        }
                    }

                }


                if (!this.IsInstalled)
                {
                    //Check default install path (cus deepstack.exe decompiled shows HARDCODED exe paths!!!!!!  WTF?)
                    if (File.Exists(this.DeepStackEXE))
                    {
                        this.IsInstalled = true;
                        LogProgress("DeepStack is installed: " + this.DeepStackEXE);
                    }
                    else
                    {
                        this.IsInstalled = false;
                        LogProgress("DeepStack NOT installed");
                    }
                }
                else
                {
                    //LogProgress("DeepStack is installed: " + this.DeepStackEXE);
                }


                //Try to get running processes in any case
                bool success = GetBlueStackRunningProcesses();

                Ret = true;

            }
            catch (Exception ex)
            {

                LogProgress("Error: While detecting DeepStack, got: " + SharedFunctions.ExMsg(ex));
            }

            if (key != null)
                key.Dispose();

            return Ret;

        }
        public bool Start()
        {
            bool Ret = false;

            try
            {

                if (!this.IsInstalled)
                {
                    LogProgress("Error: Cannot start because not installed.");
                    this.IsStarted = false;
                    return Ret;
                }
                else
                {
                    LogProgress("Starting DeepStack...");
                }

                Stopwatch SW = Stopwatch.StartNew();

                //First initialize with the py script

                Process InitProc = new Process();
                InitProc.StartInfo.FileName = this.PythonEXE;
                InitProc.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.PythonEXE);
                InitProc.StartInfo.Arguments = "../init.py";
                InitProc.StartInfo.UseShellExecute = false;
                InitProc.StartInfo.CreateNoWindow = true;
                LogProgress($"Starting {InitProc.StartInfo.FileName} {InitProc.StartInfo.Arguments}...");
                InitProc.Start();

                //next start the redis server...
                this.RedisProc.process = new Process();
                this.RedisProc.process.StartInfo.FileName = this.RedisEXE;
                this.RedisProc.process.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.RedisEXE);
                this.RedisProc.process.StartInfo.UseShellExecute = false;
                this.RedisProc.process.StartInfo.CreateNoWindow = true;
                this.RedisProc.FileName = this.RedisEXE;
                LogProgress($"Starting {this.RedisEXE}...");
                this.RedisProc.process.Start();

                //next, start the server

                this.ServerProc.process = new Process();
                this.ServerProc.process.StartInfo.FileName = this.ServerEXE;
                this.ServerProc.process.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.ServerEXE);
                this.ServerProc.process.StartInfo.Arguments = $"-VISION-FACE={this.FaceAPIEnabled} -VISION-SCENE={this.SceneAPIEnabled} -VISION-DETECTION={this.DetectionAPIEnabled} -ADMIN-KEY={this.AdminKey} -API-KEY={this.APIKey} -PORT={this.Port}";
                this.ServerProc.process.StartInfo.CreateNoWindow = true;
                this.ServerProc.process.StartInfo.UseShellExecute = false;
                this.ServerProc.process.StartInfo.RedirectStandardOutput = true;
                this.ServerProc.process.OutputDataReceived += this.handlelogs;
                this.ServerProc.FileName = this.ServerEXE;
                this.ServerProc.CommandLine = this.ServerProc.process.StartInfo.Arguments;
                LogProgress($"Starting {this.ServerProc.process.StartInfo.FileName} {this.ServerProc.process.StartInfo.Arguments}...");
                this.ServerProc.process.Start();
                this.ServerProc.process.BeginOutputReadLine();

                //start the python intelligence.py script
                this.PythonProc.process = new Process();
                this.PythonProc.process.StartInfo.FileName = this.PythonEXE;
                this.PythonProc.process.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.PythonEXE);
                this.PythonProc.process.StartInfo.Arguments = $"../intelligence.py -MODE={this.Mode} -VFACE={this.FaceAPIEnabled} -VSCENE={this.SceneAPIEnabled} -VDETECTION={this.DetectionAPIEnabled}";
                this.PythonProc.process.StartInfo.UseShellExecute = false;
                this.PythonProc.process.StartInfo.CreateNoWindow = true;
                this.PythonProc.FileName = this.PythonEXE;
                this.PythonProc.CommandLine = this.PythonProc.process.StartInfo.Arguments;
                LogProgress($"Starting {this.PythonProc.process.StartInfo.FileName} {this.PythonProc.process.StartInfo.Arguments}...");
                this.PythonProc.process.Start();


                this.IsStarted = true;
                this.HasError = false;
                Ret = true;

                LogProgress("Started in " + SW.ElapsedMilliseconds + "ms");


            }
            catch (Exception ex)
            {
                this.IsStarted = false;
                this.HasError = true;
                LogProgress("Error: Cannot start: " + SharedFunctions.ExMsg(ex));
            }

            return Ret;

        }

        private void handlelogs(object sender, DataReceivedEventArgs line)
        {
            LogProgress(line.Data);
        }
        public async Task<bool> StopAsync()
        {
            bool Ret = false;
            bool err = false;

            LogProgress("Stopping Deepstack...");
            Stopwatch sw = Stopwatch.StartNew();

            //more than one python process we need to take care of...
            for (int i = 0; i < 5; i++)
            {
                if (!(this.PythonProc.process == null))
                {
                    try
                    {
                        await Task.Run(() => this.PythonProc.process.Kill());
                        await Task.Delay(100);
                        this.PythonProc = SharedFunctions.GetaProcessByFullPath(this.PythonEXE);
                    }
                    catch (Exception ex)
                    {

                        LogProgress("Error: Could not stop DeepStack python.exe process: " + SharedFunctions.ExMsg(ex));
                        err = true;
                    }
                }
                else
                {
                    break;
                }

            }

            try
            {
                if (!(this.RedisProc.process == null))
                    await Task.Run(() => this.RedisProc.process.Kill());
            }
            catch (Exception ex)
            {
                LogProgress("Error: Could not stop DeepStack redis-server.exe process: " + SharedFunctions.ExMsg(ex));
                err = true;
            }
            try
            {
                if (!(this.ServerProc.process == null))
                    await Task.Run(() => this.ServerProc.process.Kill());
            }
            catch (Exception ex)
            {
                LogProgress("Could not stop DeepStack server.exe process: " + SharedFunctions.ExMsg(ex));
                err = true;
            }
            try
            {
                if (!(this.DeepStackProc.process == null))
                    await Task.Run(() => this.DeepStackProc.process.Kill());
            }
            catch (Exception ex)
            {
                LogProgress("Error: Could not stop DeepStack.exe process: " + SharedFunctions.ExMsg(ex));
                err = true;
            }

            //takes a while for other python.exe processes to fully stop
            await Task.Delay(250);

            if (!err)
            {
                this.PythonProc.process = null;
                this.RedisProc.process = null;
                this.ServerProc.process = null;
                this.DeepStackProc.process = null;
                this.IsStarted = false;
                LogProgress("Stopped DeepStack in " + sw.ElapsedMilliseconds + "ms");
                Ret = true;
            }
            else
            {
                LogProgress("Error: Could not stop - This can happen for a few reasons: 1) This tool did not originally START deepstack.  2) If this tool is 32 bit it cannot stop 64 bit Deepstack process.  Kill manually via task manager - Server.exe, python.exe, redis-server.exe.");
            }


            this.HasError = !Ret;
            return Ret;

        }

    }
}
