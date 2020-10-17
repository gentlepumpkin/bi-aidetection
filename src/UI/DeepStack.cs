using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using static AITool.AITOOL;


namespace AITool
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
        public bool VisionDetectionRunning = false;
        public bool NeedsSaving = false;
        public Global.ClsProcess DeepStackProc;
        public Global.ClsProcess ServerProc;
        public Global.ClsProcess PythonProc;
        public Global.ClsProcess RedisProc;
        public List<double> ResponseTimeList = new List<double>();  //From this you can get min/max/avg

        
        public DeepStack(string AdminKey, string APIKey, string Mode, bool SceneAPIEnabled, bool FaceAPIEnabled, bool DetectionAPIEnabled, string Port)
        {

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
        public bool GetDeepStackRun()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            bool Ret = false;

            //Note - deepstack.exe does NOT need to be running
            if (!Global.ProcessValid(this.DeepStackProc))
                this.DeepStackProc = Global.GetaProcessByPath(this.DeepStackEXE);
            if (!Global.ProcessValid(this.ServerProc))
                this.ServerProc = Global.GetaProcessByPath(this.ServerEXE);
            if (!Global.ProcessValid(this.PythonProc))
                this.PythonProc = Global.GetaProcessByPath(this.PythonEXE);
            if (!Global.ProcessValid(this.RedisProc))
                this.RedisProc = Global.GetaProcessByPath(this.RedisEXE);

            if (Global.ProcessValid(this.ServerProc) && Global.ProcessValid(this.PythonProc) && Global.ProcessValid(this.RedisProc))
            {
                this.IsInstalled = true;
                    this.HasError = false;
                    Log("Debug: DeepStack Desktop IS running from " + ServerProc.FileName);

                    this.IsStarted = true;
                    //C:\DeepStack\server\server.exe
                    //check to see if it is a different path than default
                    if (!this.ServerProc.FileName.ToLower().StartsWith(this.DeepStackFolder.ToLower()))
                    {
                        string dspath = this.ServerProc.FileName.ToLower().Replace(@"server\server.exe", "");
                        Log("Debug: Deepstack running from non-default path: " + dspath);
                        this.DeepStackFolder = dspath;
                        this.DeepStackEXE = Path.Combine(this.DeepStackFolder, @"DeepStack.exe");
                        this.PythonEXE = Path.Combine(this.DeepStackFolder, @"interpreter\python.exe");
                        this.RedisEXE = Path.Combine(this.DeepStackFolder, @"redis\redis-server.exe");
                        this.ServerEXE = Path.Combine(this.DeepStackFolder, @"server\server.exe");
                        this.NeedsSaving = true;
                    }

                    //Try to get command line params to fill in correct running port, etc
                    //"C:\DeepStack\server\server.exe" -VISION-FACE=False -VISION-SCENE=True -VISION-DETECTION=True -ADMIN-KEY= -API-KEY= -PORT=84

                    string face = Global.GetWordBetween(this.ServerProc.CommandLine, "-VISION-FACE=", " |-");
                    if (!string.IsNullOrEmpty(face))
                        if (this.FaceAPIEnabled != Convert.ToBoolean(face))
                        {
                            Log($"Debug: ...Face API detection setting found in running server.exe process changed from '{this.FaceAPIEnabled}' to '{Convert.ToBoolean(face)}'");
                            this.FaceAPIEnabled = Convert.ToBoolean(face);
                            this.NeedsSaving = true;
                        }

                    string scene = Global.GetWordBetween(this.ServerProc.CommandLine, "-VISION-SCENE=", " |-");
                    if (!string.IsNullOrEmpty(scene))
                        if (Convert.ToBoolean(scene) != this.SceneAPIEnabled) {
                            Log($"Debug: ...Scene API detection setting found in running server.exe process changed from '{this.SceneAPIEnabled}' to '{Convert.ToBoolean(scene)}'");
                            this.SceneAPIEnabled = Convert.ToBoolean(scene);
                            this.NeedsSaving = true;
                        };
                    
                    string detect = Global.GetWordBetween(this.ServerProc.CommandLine, "-VISION-DETECTION=", " |-");
                    if (!string.IsNullOrEmpty(detect))
                        if (this.DetectionAPIEnabled != Convert.ToBoolean(detect))
                        {
                            Log($"Debug: ...Detection API detection setting found in running server.exe process changed from '{this.DetectionAPIEnabled}' to '{Convert.ToBoolean(detect)}'");
                            this.DetectionAPIEnabled = Convert.ToBoolean(detect);
                            this.NeedsSaving = true;
                        }

                    string admin = Global.GetWordBetween(this.ServerProc.CommandLine, "-ADMIN-KEY=", " |-");
                    if (!string.IsNullOrEmpty(admin))
                        if (this.AdminKey != admin)
                        {
                            Log($"Debug: ...Admin key setting found in running server.exe process changed from '{this.AdminKey}' to '{admin}'");
                            this.AdminKey = admin;
                            this.NeedsSaving = true;
                        }

                    string api = Global.GetWordBetween(this.ServerProc.CommandLine, "-API-KEY=", " |-");
                    if (!string.IsNullOrEmpty(api))
                        if (this.APIKey != api)
                        {
                            Log($"Debug: ...API key setting found in running server.exe process changed from '{this.APIKey}' to '{api}'");
                            this.APIKey = api;
                            this.NeedsSaving = true;
                        }

                    string port = Global.GetWordBetween(this.ServerProc.CommandLine, "-PORT=", " |-");
                    if (!string.IsNullOrEmpty(port))
                        if (this.Port != port)
                        {
                            Log($"Debug: ...Port setting found in running server.exe process changed from '{this.Port}' to '{port}'");
                            this.Port = port;
                            this.NeedsSaving = true;
                        }

                    //Get mode:
                    //"C:\DeepStack\interpreter\python.exe" ../intelligence.py -MODE=Medium -VFACE=False -VSCENE=True -VDETECTION=True

                    string mode = Global.GetWordBetween(this.PythonProc.CommandLine, "-MODE=", " |-");
                    if (!string.IsNullOrEmpty(port))
                        if (this.Mode != mode)
                        {
                            Log($"Debug: ...Mode setting found in running python.exe process changed from '{this.Mode}' to '{mode}'");
                            this.Mode = mode;
                            this.NeedsSaving = true;
                        }


                    //"C:\DeepStack\interpreter\python.exe" "-c" "from multiprocessing.spawn import spawn_main; spawn_main(parent_pid=17744, pipe_handle=328)" "--multiprocessing-fork"

            }
            else if (Global.ProcessValid(this.ServerProc) || Global.ProcessValid(this.PythonProc) || Global.ProcessValid(this.RedisProc))
            {
                Log("Error: Deepstack partially running.  You many need to manually kill server.exe, python.exe, redis-server.exe");
                this.HasError = true;
                this.IsStarted = true;
            }
            else
            {
                Log("Debug: DeepStack Desktop NOT running.");
                this.IsStarted = false;
                this.HasError = false;
            }

            return Ret;
        }
        public bool RefreshInfo()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

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
                        Log("Debug: Deepstack Desktop install path found in Uninstall registry: " + dspath);

                        string exepth = Path.Combine(dspath, "DeepStack.exe");
                        if (File.Exists(exepth))
                        {
                            this.IsInstalled = true;
                            this.DeepStackFolder = dspath;
                            this.DeepStackEXE = exepth;
                            if (dspath.ToLower() != this.DeepStackFolder.ToLower())
                            {
                                Log("Debug: Deepstack running from non-default path: " + dspath);
                                this.PythonEXE = Path.Combine(dspath, @"interpreter\python.exe");
                                this.RedisEXE = Path.Combine(dspath, @"redis\redis-server.exe");
                                this.ServerEXE = Path.Combine(dspath, @"server\server.exe");
                                this.NeedsSaving = true;
                            }
                        }
                        else
                        {
                            Log("debug: DeepStack File not found " + exepth);
                        }
                    }

                }


                if (!this.IsInstalled)
                {
                    //Check default install path (cus deepstack.exe decompiled shows HARDCODED exe paths!!!!!!  WTF?)
                    if (File.Exists(this.DeepStackEXE))
                    {
                        this.IsInstalled = true;
                        Log("Debug: DeepStack is installed: " + this.DeepStackEXE);
                    }
                    else
                    {
                        this.IsInstalled = false;
                        Log("Debug: DeepStack NOT installed");
                    }
                }
                else
                {
                    //LogProgress("DeepStack is installed: " + this.DeepStackEXE);
                }


                //Try to get running processes in any case
                bool success = GetDeepStackRun();

                Ret = true;

            }
            catch (Exception ex)
            {

                Log("Error: While detecting DeepStack, got: " + Global.ExMsg(ex));
            }

            if (key != null)
                key.Dispose();

            return Ret;

        }
        public async Task<bool> StartAsync()
        {
            return await Task.Run(() => this.Start());
        }
        private async Task<bool> Start()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            bool Ret = false;
            
            try
            {

                if (!this.IsInstalled)
                {
                    Log("Error: Cannot start because not installed.");
                    this.IsStarted = false;
                    return Ret;
                }
                else
                {
                    Log("Starting DeepStack...");
                }

                Stopwatch SW = Stopwatch.StartNew();

                //First initialize with the py script

                Process InitProc = new Process();
                InitProc.StartInfo.FileName = this.PythonEXE;
                InitProc.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.PythonEXE);
                InitProc.StartInfo.Arguments = "../init.py";
                InitProc.StartInfo.UseShellExecute = false;
                InitProc.StartInfo.CreateNoWindow = true;
                InitProc.StartInfo.RedirectStandardOutput = true;
                InitProc.StartInfo.RedirectStandardError = true;
                InitProc.EnableRaisingEvents = true;
                InitProc.OutputDataReceived += this.handleinitprocmsg;
                InitProc.ErrorDataReceived += this.handleinitprocerror;
                InitProc.Exited += (sender, e) => myProcess_Exited(sender, e, "Init:Python.exe"); //new EventHandler(myProcess_Exited);
                Log($"Starting {InitProc.StartInfo.FileName} {InitProc.StartInfo.Arguments}...");
                InitProc.Start();
                InitProc.PriorityClass = ProcessPriorityClass.High;  //always run this as high priority since it will initialize faster
                InitProc.BeginOutputReadLine();
                InitProc.BeginErrorReadLine();

                //next start the redis server...
                this.RedisProc = new Global.ClsProcess();
                this.RedisProc.process.StartInfo.FileName = this.RedisEXE;
                this.RedisProc.process.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.RedisEXE);
                this.RedisProc.process.StartInfo.UseShellExecute = false;
                this.RedisProc.process.StartInfo.CreateNoWindow = true;
                this.RedisProc.process.StartInfo.RedirectStandardOutput = true;
                this.RedisProc.process.StartInfo.RedirectStandardError = true;
                this.RedisProc.process.EnableRaisingEvents = true;
                this.RedisProc.process.OutputDataReceived += this.handleredisprocmsg;
                this.RedisProc.process.ErrorDataReceived += this.handleredisprocerror;
                this.RedisProc.process.Exited += (sender, e) => myProcess_Exited(sender, e, "Redis.exe"); //new EventHandler(myProcess_Exited);
                this.RedisProc.FileName = this.RedisEXE;
                this.RedisProc.CommandLine = this.RedisEXE;
                Log($"Starting {this.RedisEXE}...");
                this.RedisProc.process.Start();
                if (AppSettings.Settings.deepstack_highpriority)
                {
                    this.RedisProc.process.PriorityClass = ProcessPriorityClass.High;
                }
                this.RedisProc.process.BeginOutputReadLine();
                this.RedisProc.process.BeginErrorReadLine();

                //next, start the server

                this.ServerProc = new Global.ClsProcess();
                this.ServerProc.process.StartInfo.FileName = this.ServerEXE;
                this.ServerProc.process.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.ServerEXE);
                this.ServerProc.process.StartInfo.Arguments = $"-VISION-FACE={this.FaceAPIEnabled} -VISION-SCENE={this.SceneAPIEnabled} -VISION-DETECTION={this.DetectionAPIEnabled} -ADMIN-KEY={this.AdminKey} -API-KEY={this.APIKey} -PORT={this.Port}";
                this.ServerProc.process.StartInfo.CreateNoWindow = true;
                this.ServerProc.process.StartInfo.UseShellExecute = false;
                this.ServerProc.process.StartInfo.RedirectStandardOutput = true;
                this.ServerProc.process.StartInfo.RedirectStandardError = true;
                this.ServerProc.process.EnableRaisingEvents = true;
                this.ServerProc.process.OutputDataReceived += this.handleserverprocmsg;
                this.ServerProc.process.ErrorDataReceived += this.handleserverprocerror;
                this.ServerProc.process.Exited += (sender, e) => myProcess_Exited(sender, e, "Server.exe"); //new EventHandler(myProcess_Exited);
                this.ServerProc.FileName = this.ServerEXE;
                this.ServerProc.CommandLine = this.ServerProc.process.StartInfo.Arguments;
                Log($"Starting {this.ServerProc.process.StartInfo.FileName} {this.ServerProc.process.StartInfo.Arguments}...");
                this.ServerProc.process.Start();
                if (AppSettings.Settings.deepstack_highpriority)
                {
                    this.ServerProc.process.PriorityClass = ProcessPriorityClass.High;
                }

                this.ServerProc.process.BeginOutputReadLine();
                this.ServerProc.process.BeginErrorReadLine();

                //start the python intelligence.py script
                this.PythonProc = new Global.ClsProcess();
                this.PythonProc.process.StartInfo.FileName = this.PythonEXE;
                this.PythonProc.process.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.PythonEXE);
                this.PythonProc.process.StartInfo.Arguments = $"../intelligence.py -MODE={this.Mode} -VFACE={this.FaceAPIEnabled} -VSCENE={this.SceneAPIEnabled} -VDETECTION={this.DetectionAPIEnabled}";
                this.PythonProc.process.StartInfo.UseShellExecute = false;
                this.PythonProc.process.StartInfo.CreateNoWindow = true;
                this.PythonProc.process.EnableRaisingEvents = true;
                this.PythonProc.process.StartInfo.RedirectStandardOutput = true;
                this.PythonProc.process.StartInfo.RedirectStandardError = true;
                this.PythonProc.process.OutputDataReceived += this.handlepythonprocmsg;
                this.PythonProc.process.ErrorDataReceived += this.handlepythonprocerror;
                this.PythonProc.process.Exited += (sender, e) => myProcess_Exited(sender, e, "Main:Python.exe"); //new EventHandler(myProcess_Exited);
                this.PythonProc.FileName = this.PythonEXE;
                this.PythonProc.CommandLine = this.PythonProc.process.StartInfo.Arguments;
                Log($"Starting {this.PythonProc.process.StartInfo.FileName} {this.PythonProc.process.StartInfo.Arguments}...");
                this.PythonProc.process.Start();
                if (AppSettings.Settings.deepstack_highpriority)
                {
                    this.PythonProc.process.PriorityClass = ProcessPriorityClass.High;
                }


                this.PythonProc.process.BeginOutputReadLine();
                this.PythonProc.process.BeginErrorReadLine();


                this.IsStarted = true;
                this.HasError = false;
                Ret = true;

                //Lets wait for the rest of the python.exe processes to spawn and set their priority too (otherwise they are normal)

                int cnt = 0;
                do
                {

                    List<Global.ClsProcess> montys = Global.GetProcessesByPath(this.PythonEXE);
                    if (montys.Count >= 5)
                    {
                        //when deepstack is running normaly there will be 5 python.exe processes
                        //Set priority for each this way since we didnt start them in the first place...
                        cnt = montys.Count;
                        if (AppSettings.Settings.deepstack_highpriority)
                        {
                            foreach (Global.ClsProcess prc in montys)
                            {
                                if (Global.ProcessValid(prc))
                                {
                                    try
                                    {
                                        prc.process.PriorityClass = ProcessPriorityClass.High;
                                    }
                                    catch { }

                                }
                            }
                        }
                        break;
                    }
                    await Task.Delay(100);
                    
                } while (SW.ElapsedMilliseconds < 10000);  //wait 10 seconds max

                if (cnt == 5)
                {
                    Log("Started in " + SW.ElapsedMilliseconds + "ms");
                }
                else if (cnt > 5)
                {
                    this.HasError = true;
                    this.IsStarted = true;
                    Log("Error: More than 5 python.exe processes are running from the deepstack folder?  Manually stop/restart.   (" + SW.ElapsedMilliseconds + "ms)");
                }
                else if (cnt == 0)
                {
                    this.HasError = true;
                    this.IsStarted = true;
                    Log("Error: 5 python.exe processes did not fully start in " + SW.ElapsedMilliseconds + "ms");
                }

            }
            catch (Exception ex)
            {
                this.IsStarted = false;
                this.HasError = true;
                Log("Error: Cannot start: " + Global.ExMsg(ex));
            }

            return Ret;

        }


        private void myProcess_Exited(object sender, System.EventArgs e, string Name)
        {

            string output = "Debug: DeepStack>> Process exited: ";
            if (sender != null)
            {
                Process prc = (Process)sender;
                try
                {
                    //if (!prc.HasExited)
                    //{
                        output += $"Debug: Name='{Name}'";
                    //}
                    string err = prc.ExitCode.ToString();
                    if (prc.ExitCode == 0)
                    {
                        err += " (Normal exit)";
                    }
                    else
                    {
                        err += " (Process killed or exited with error)";
                    }
                    output += $", ExitCode='{err}', Runtime='{Math.Round((prc.ExitTime - prc.StartTime).TotalMilliseconds)}'ms";
                }
                catch {

                    output += " (error accessing process properties)";
                }
            }
            if (!Name.Contains("Init:"))
            {
                this.IsStarted = false;
                this.HasError = true;
            }
            Log(output, "");

        }



        private void handleredisprocerror(object sender, DataReceivedEventArgs line)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(line.Data) || Global.progress == null)
                {
                    return;
                }

                Log($"DeepStack>> ERROR: {line.Data}", "","", "REDIS-SERVER.EXE");

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private void handleredisprocmsg(object sender, DataReceivedEventArgs line)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(line.Data) || Global.progress == null)
                {
                    return;
                }

                Log($"Debug: DeepStack>> {line.Data}", "","", "REDIS-SERVER.EXE");

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void handleinitprocerror(object sender, DataReceivedEventArgs line)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(line.Data) || Global.progress == null)
                {
                    return;
                }

                Log($"Debug: DeepStack>> Init: ERROR: {line.Data}", "","PYTHON.EXE");

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private void handleinitprocmsg(object sender, DataReceivedEventArgs line)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(line.Data) || Global.progress == null)
                {
                    return;
                }

                Log($"Debug: DeepStack>> Init: {line.Data}", "","","PYTHON.EXE");

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void handlepythonprocerror(object sender, DataReceivedEventArgs line)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(line.Data) || Global.progress == null)
                {
                    return;
                }

                Log($"DeepStack>> ERROR: {line.Data}", "", "", "PYTHON.EXE");

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private void handlepythonprocmsg(object sender, DataReceivedEventArgs line)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(line.Data) || Global.progress == null)
                {
                    return;
                }

                Log($"Debug: DeepStack>> {line.Data}", "", "", "PYTHON.EXE");

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void handleserverprocerror(object sender, DataReceivedEventArgs line)
        {
            
            try
            {
                if (string.IsNullOrWhiteSpace(line.Data) || Global.progress == null)
                {
                    return;
                }

                Log($"DeepStack>> ERROR: {line.Data}", "", "", "SERVER.EXE");

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private void handleserverprocmsg(object sender, DataReceivedEventArgs line)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(line.Data) || Global.progress == null)
                {
                    return;
                }

                //Console.WriteLine(Message);
                if (line.Data.ToLower().Contains("visit localhost to activate deepstack"))
                {
                    this.IsActivated = false;
                }
                else if (line.Data.ToLower().Contains("deepstack is active"))
                {
                    this.IsActivated = true;
                }

                if (line.Data.ToLower().Contains("vision/detection"))
                {
                    this.VisionDetectionRunning = true;
                }


                Log($"Debug: DeepStack>> {line.Data}", "", "", "SERVER.EXE");

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<bool> StopAsync()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            bool Ret = false;
            bool err = false;

            Log("Stopping Deepstack...");
            Stopwatch sw = Stopwatch.StartNew();
            //Try to get running processes in any case
            bool success = GetDeepStackRun();
            //more than one python process we need to take care of...  Sometimes MANY 
            for (int i = 0; i < 20; i++)
            {
                if (Global.ProcessValid(PythonProc))
                {
                    try
                    {
                        await Task.Run(() => this.PythonProc.process.Kill());
                        await Task.Delay(100);
                        this.PythonProc = Global.GetaProcessByPath(this.PythonEXE);
                    }
                    catch (Exception ex)
                    {

                        Log("Error: Could not stop DeepStack python.exe process: " + Global.ExMsg(ex));
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
                if (Global.ProcessValid(this.RedisProc))
                    await Task.Run(() => this.RedisProc.process.Kill());
            }
            catch (Exception ex)
            {
                Log("Error: Could not stop DeepStack redis-server.exe process: " + Global.ExMsg(ex));
                err = true;
            }
            try
            {
                if (Global.ProcessValid(this.ServerProc))
                    await Task.Run(() => this.ServerProc.process.Kill());
            }
            catch (Exception ex)
            {
                Log("Error: Could not stop DeepStack server.exe process: " + Global.ExMsg(ex));
                err = true;
            }
            try
            {
                if (Global.ProcessValid(this.DeepStackProc))
                    await Task.Run(() => this.DeepStackProc.process.Kill());
            }
            catch (Exception ex)
            {
                Log("Error: Could not stop DeepStack.exe process: " + Global.ExMsg(ex));
                err = true;
            }

            //takes a while for other python.exe processes to fully stop
            await Task.Delay(250);

            if (!err)
            {
                this.PythonProc = null;
                this.RedisProc = null;
                this.ServerProc = null;
                this.DeepStackProc = null;
                this.IsStarted = false;
                Log("Debug: Stopped DeepStack in " + sw.ElapsedMilliseconds + "ms");
                Ret = true;
            }
            else
            {
                Log("Error: Could not stop - This can happen for a few reasons: 1) This tool did not originally START deepstack.  2) If this tool is 32 bit it cannot stop 64 bit Deepstack process.  Kill manually via task manager - Server.exe, python.exe, redis-server.exe.");
            }


            this.HasError = !Ret;
            return Ret;

        }

    }
}
