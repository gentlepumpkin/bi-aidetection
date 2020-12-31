using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static AITool.AITOOL;


namespace AITool
{
    public enum DeepStackTypeEnum
    {
        CPU,
        GPU,
        Unknown
    }
    public class DeepStack
    {

        public string DisplayName = "Unknown";
        public string DisplayVersion = "Unknown";
        public DeepStackTypeEnum Type = DeepStackTypeEnum.Unknown;
        public bool IsNewVersion = false;
        public string AdminKey = "";
        public string APIKey = "";
        public string Port = "81";
        public string Mode = "Medium";
        public int Count = 1;
        public string DeepStackFolder = @"C:\DeepStack";
        public string DeepStackEXE = @"C:\DeepStack\DeepStack.exe";
        public string ServerEXE = @"C:\DeepStack\server\server.exe";
        public string PythonEXE = @"C:\DeepStack\interpreter\python.exe";
        public string RedisEXE = @"C:\DeepStack\redis\redis-server.exe";
        public bool SceneAPIEnabled = false;
        public bool FaceAPIEnabled = false;
        public bool DetectionAPIEnabled = true;
        public bool CustomModelEnabled = false;
        public string CustomModelPath = "";
        public bool IsStarted = false;
        public bool HasError = false;
        public bool IsInstalled = false;
        public bool IsActivated = false;
        public bool VisionDetectionRunning = false;
        public bool NeedsSaving = false;
        public string CommandLine = "";
        public List<Global.ClsProcess> DeepStackProc = new List<Global.ClsProcess>();
        public List<Global.ClsProcess> ServerProc = new List<Global.ClsProcess>();
        public List<Global.ClsProcess> PythonProc = new List<Global.ClsProcess>();
        public List<Global.ClsProcess> RedisProc = new List<Global.ClsProcess>();
        public List<double> ResponseTimeList = new List<double>();  //From this you can get min/max/avg
        public string URLS = "";

        public ThreadSafe.Boolean Starting = new ThreadSafe.Boolean(false);
        public ThreadSafe.Boolean Stopping = new ThreadSafe.Boolean(false);

        public DeepStack(string AdminKey, string APIKey, string Mode, bool SceneAPIEnabled, bool FaceAPIEnabled, bool DetectionAPIEnabled, string Port, string CustomModelPath)
        {

            this.Update(AdminKey, APIKey, Mode, SceneAPIEnabled, FaceAPIEnabled, DetectionAPIEnabled, Port, CustomModelPath);

        }

        public void Update(string AdminKey, string APIKey, string Mode, bool SceneAPIEnabled, bool FaceAPIEnabled, bool DetectionAPIEnabled, string Port, string CustomModelPath)
        {
            this.AdminKey = AdminKey.Trim();
            this.APIKey = APIKey.Trim();
            this.SceneAPIEnabled = SceneAPIEnabled;
            this.FaceAPIEnabled = FaceAPIEnabled;
            this.DetectionAPIEnabled = DetectionAPIEnabled;
            this.CustomModelPath = CustomModelPath.Trim();
            this.CustomModelEnabled = !string.IsNullOrEmpty(this.CustomModelPath) && Directory.Exists(this.CustomModelPath);
            this.Port = Port;
            this.Mode = Mode;
            this.Count = Global.Split(this.Port, ",|").Count;

            bool found = this.RefreshDeepstackInfo();

        }
        public bool GetDeepStackRun()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            bool Ret = false;

            if (this.IsNewVersion)
            {
                this.Count = Global.Split(this.Port, ",|").Count;


                if (!Global.ProcessValid(this.ServerProc))
                    this.ServerProc = Global.GetProcessesByPath(this.ServerEXE);
                if (!Global.ProcessValid(this.PythonProc))
                    this.PythonProc = Global.GetProcessesByPath(this.PythonEXE);
                if (!Global.ProcessValid(this.RedisProc))
                    this.RedisProc = Global.GetProcessesByPath(this.RedisEXE);

                List<Global.ClsProcess> montys = Global.GetProcessesByPath(this.PythonEXE);

                bool srvvalid = Global.ProcessValid(this.ServerProc);
                bool redvalid = Global.ProcessValid(this.RedisProc);
                bool pytvalid = srvvalid && montys.Count == this.Count * 2;

                bool allvalid = srvvalid && redvalid && pytvalid;

                bool partvalid = (srvvalid || redvalid || pytvalid || Global.ProcessValid(this.PythonProc));

                if (allvalid)
                {
                    this.HasError = false;
                    this.IsStarted = true;
                    Log("Debug: DeepStack Desktop IS running from " + this.ServerEXE);
                }
                else if (partvalid)
                {
                    this.HasError = true;
                    this.IsStarted = true;
                    Log("Deepstack partially running.");

                }
                else
                {
                    this.IsStarted = false;
                    this.HasError = false;
                    Log("Debug: DeepStack Desktop NOT running.");
                }
            }
            else
            {
                Log("Error: Deepstack v3.4 not supported.  Install version 2020. https://docs.deepstack.cc/windows/index.html");
                ////Note - deepstack.exe does NOT need to be running
                //if (!Global.ProcessValid(this.DeepStackProc))
                //    this.DeepStackProc = Global.GetaProcessByPath(this.DeepStackEXE);
                //if (!Global.ProcessValid(this.ServerProc))
                //    this.ServerProc = Global.GetaProcessByPath(this.ServerEXE);
                //if (!Global.ProcessValid(this.PythonProc))
                //    this.PythonProc = Global.GetaProcessByPath(this.PythonEXE);
                //if (!Global.ProcessValid(this.RedisProc))
                //    this.RedisProc = Global.GetaProcessByPath(this.RedisEXE);

                //if (Global.ProcessValid(this.ServerProc) && Global.ProcessValid(this.PythonProc) && Global.ProcessValid(this.RedisProc))
                //{
                //    this.IsInstalled = true;
                //    this.HasError = false;
                //    Log("Debug: DeepStack Desktop IS running from " + this.ServerProc.FileName);

                //    this.IsStarted = true;
                //    //C:\DeepStack\server\server.exe
                //    //check to see if it is a different path than default
                //    if (!this.ServerProc.FileName.StartsWith(this.DeepStackFolder, StringComparison.OrdinalIgnoreCase))
                //    {
                //        string dspath = this.ServerProc.FileName.ToLower().Replace(@"server\server.exe", "");
                //        Log("Debug: Deepstack running from non-default path: " + dspath);
                //        this.DeepStackFolder = dspath;
                //        this.DeepStackEXE = Path.Combine(this.DeepStackFolder, @"DeepStack.exe");
                //        this.PythonEXE = Path.Combine(this.DeepStackFolder, @"interpreter\python.exe");
                //        this.RedisEXE = Path.Combine(this.DeepStackFolder, @"redis\redis-server.exe");
                //        this.ServerEXE = Path.Combine(this.DeepStackFolder, @"server\server.exe");
                //        this.NeedsSaving = true;
                //    }

                //    //Try to get command line params to fill in correct running port, etc
                //    //"C:\DeepStack\server\server.exe" -VISION-FACE=False -VISION-SCENE=True -VISION-DETECTION=True -ADMIN-KEY= -API-KEY= -PORT=84

                //    string face = Global.GetWordBetween(this.ServerProc.CommandLine, "-VISION-FACE=", " |-");
                //    if (!string.IsNullOrEmpty(face))
                //        if (this.FaceAPIEnabled != Convert.ToBoolean(face))
                //        {
                //            Log($"Debug: ...Face API detection setting found in running server.exe process changed from '{this.FaceAPIEnabled}' to '{Convert.ToBoolean(face)}'");
                //            this.FaceAPIEnabled = Convert.ToBoolean(face);
                //            this.NeedsSaving = true;
                //        }

                //    string scene = Global.GetWordBetween(this.ServerProc.CommandLine, "-VISION-SCENE=", " |-");
                //    if (!string.IsNullOrEmpty(scene))
                //        if (Convert.ToBoolean(scene) != this.SceneAPIEnabled)
                //        {
                //            Log($"Debug: ...Scene API detection setting found in running server.exe process changed from '{this.SceneAPIEnabled}' to '{Convert.ToBoolean(scene)}'");
                //            this.SceneAPIEnabled = Convert.ToBoolean(scene);
                //            this.NeedsSaving = true;
                //        };

                //    string detect = Global.GetWordBetween(this.ServerProc.CommandLine, "-VISION-DETECTION=", " |-");
                //    if (!string.IsNullOrEmpty(detect))
                //        if (this.DetectionAPIEnabled != Convert.ToBoolean(detect))
                //        {
                //            Log($"Debug: ...Detection API detection setting found in running server.exe process changed from '{this.DetectionAPIEnabled}' to '{Convert.ToBoolean(detect)}'");
                //            this.DetectionAPIEnabled = Convert.ToBoolean(detect);
                //            this.NeedsSaving = true;
                //        }

                //    string admin = Global.GetWordBetween(this.ServerProc.CommandLine, "-ADMIN-KEY=", " |-");
                //    if (!string.IsNullOrEmpty(admin))
                //        if (this.AdminKey != admin)
                //        {
                //            Log($"Debug: ...Admin key setting found in running server.exe process changed from '{this.AdminKey}' to '{admin}'");
                //            this.AdminKey = admin;
                //            this.NeedsSaving = true;
                //        }

                //    string api = Global.GetWordBetween(this.ServerProc.CommandLine, "-API-KEY=", " |-");
                //    if (!string.IsNullOrEmpty(api))
                //        if (this.APIKey != api)
                //        {
                //            Log($"Debug: ...API key setting found in running server.exe process changed from '{this.APIKey}' to '{api}'");
                //            this.APIKey = api;
                //            this.NeedsSaving = true;
                //        }

                //    string port = Global.GetWordBetween(this.ServerProc.CommandLine, "-PORT=", " |-");
                //    if (!string.IsNullOrEmpty(port))
                //        if (this.Port != port)
                //        {
                //            Log($"Debug: ...Port setting found in running server.exe process changed from '{this.Port}' to '{port}'");
                //            this.Port = port;
                //            this.NeedsSaving = true;
                //        }

                //    //Get mode:
                //    //"C:\DeepStack\interpreter\python.exe" ../intelligence.py -MODE=Medium -VFACE=False -VSCENE=True -VDETECTION=True

                //    string mode = Global.GetWordBetween(this.PythonProc.CommandLine, "-MODE=", " |-");
                //    if (!string.IsNullOrEmpty(port))
                //        if (this.Mode != mode)
                //        {
                //            Log($"Debug: ...Mode setting found in running python.exe process changed from '{this.Mode}' to '{mode}'");
                //            this.Mode = mode;
                //            this.NeedsSaving = true;
                //        }


                //    //"C:\DeepStack\interpreter\python.exe" "-c" "from multiprocessing.spawn import spawn_main; spawn_main(parent_pid=17744, pipe_handle=328)" "--multiprocessing-fork"

                //}
                //else if (Global.ProcessValid(this.ServerProc) || Global.ProcessValid(this.PythonProc) || Global.ProcessValid(this.RedisProc))
                //{
                //    Log("Error: Deepstack partially running.  You many need to manually kill server.exe, python.exe, redis-server.exe");
                //    this.HasError = true;
                //    this.IsStarted = true;
                //}
                //else
                //{
                //    Log("Debug: DeepStack Desktop NOT running.");
                //    this.IsStarted = false;
                //    this.HasError = false;
                //}

            }

            return Ret;
        }
        public bool RefreshDeepstackInfo()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            bool Ret = false;
            this.IsInstalled = false;
            RegistryKey key = null;
            List<string> reglocs = new List<string>();
            //                                                                            {0E2C3125-3440-4622-A82A-3B1E07310EF2}_is1
            reglocs.Add(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\{0E2C3125-3440-4622-A82A-3B1E07310EF2}_is1");  //new 2020 beta 
            reglocs.Add(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{0E2C3125-3440-4622-A82A-3B1E07310EF2}_is1");              //check for 64 bit install but I dont think it exists 
            reglocs.Add(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\{B976B0A1-C83C-4735-AC7F-196922A2748B}_is1");  //old 32 bit version
            reglocs.Add(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{B976B0A1-C83C-4735-AC7F-196922A2748B}_is1");              //check for 64 bit install but I dont think it exists 


            try
            {

                foreach (string keystr in reglocs)
                {
                    key = Registry.LocalMachine.OpenSubKey(keystr);
                    if (key != null)
                        break;
                }
                

                if (key != null)
                {
                    this.DisplayName = (string)key.GetValue("DisplayName");
                    this.DisplayVersion = (string)key.GetValue("DisplayVersion");
                    this.IsNewVersion = this.DisplayName.Contains("202") || this.DisplayVersion.Contains("202");
                   

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

                            this.PythonEXE = Path.Combine(dspath, @"interpreter\python.exe");
                            this.RedisEXE = Path.Combine(dspath, @"redis\redis-server.exe");

                            if (this.IsNewVersion)
                            {
                                this.ServerEXE = Path.Combine(dspath, @"DeepStack.exe");
                            }
                            else
                            {
                                this.ServerEXE = Path.Combine(dspath, @"server\server.exe");
                            }


                            if (!string.Equals(dspath, this.DeepStackFolder, StringComparison.OrdinalIgnoreCase))
                            {
                                Log("Debug: Deepstack running from non-default path: " + dspath);
                                this.NeedsSaving = true;
                            }
                        }
                        else
                        {
                            Log("debug: DeepStack File not found " + exepth);
                        }
                    }
                    else
                    {
                        Log("Error: DeepStack registry App Path not found? 'Inno Setup: App Path'");
                    }

                }


                if (!this.IsInstalled)
                {
                    Log("Debug: DeepStack does not appear to be installed in add/remove programs.");

                    if (File.Exists(this.DeepStackEXE))
                    {
                        this.DeepStackFolder = Path.GetDirectoryName(this.DeepStackEXE);
                        this.IsInstalled = true;
                        
                    }
                    else
                    {
                        this.IsInstalled = false;
                        Log("Debug: DeepStack NOT installed");
                    }
                }

                if (this.IsInstalled)
                {
                    //get type and version

                    //this file exists with 2020 version:
                    string servergo = Path.Combine(this.DeepStackFolder, "server", "server.go");
                    //{
                    //    "PROFILE":"windows_native",
                    //    "GPU":false
                    //}
                    if (File.Exists(servergo))
                    {
                        this.IsNewVersion = true;
                        this.IsActivated = true;
                        string contents = File.ReadAllText(servergo);
                        if (contents.IndexOf("\"CUDA_MODE\", \"True\"", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            this.Type = DeepStackTypeEnum.GPU;
                        }
                        else if (contents.IndexOf("\"CUDA_MODE\", \"False\"", StringComparison.OrdinalIgnoreCase) >= 0 || contents.IndexOf("\"CUDA_MODE\"", StringComparison.OrdinalIgnoreCase) == -1)
                        {
                            this.Type = DeepStackTypeEnum.CPU;
                        }
                        else
                        {
                            this.Type = DeepStackTypeEnum.Unknown;
                            Log($"Error: Could not determine CPU/GPU type in {servergo}?");
                        }

                        //get the version
                        List<FileInfo> files = Global.GetFiles(this.DeepStackFolder, "*.iss", SearchOption.TopDirectoryOnly);
                        if (files.Count > 0)
                        {
                            contents = File.ReadAllText(files[0].FullName);
                            //#define MyAppVersion "2020.12.beta"
                            this.DisplayVersion = Global.GetWordBetween(contents, "MyAppVersion \"", "\"");
                        }
                        else
                        {
                            Log($"Error: Could not find .ISS file in Deepstack folder?");
                        }

                        Log($"Debug: DeepStack v'{this.DisplayVersion}' ({this.Type}) is installed: " + this.DeepStackEXE);
                        //Try to get running processes in any case
                        bool success = this.GetDeepStackRun();

                        Ret = true;



                    }
                    else
                    {
                        this.IsNewVersion = false;
                        this.Type = DeepStackTypeEnum.CPU;
                        this.DisplayVersion = "3.4";
                        Log("Error: Deepstack v3.4 not supported.  Install version 2020.");


                    }

                }

            }
            catch (Exception ex)
            {

                Log("Error: While detecting DeepStack, got: " + Global.ExMsg(ex));
            }

            if (key != null)
                key.Dispose();

            return Ret;

        }
        public async Task<bool> StartDeepstackAsync()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            return await Task.Run(() => this.StartDeepstack());
        }
        public bool StartDeepstack()
        {

            if (this.Starting.ReadFullFence())
            {
                Log("Already starting?");
                return false;
            }

            this.Starting.WriteFullFence(true);

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
                    if (this.IsStarted)
                    {
                        Log("Debug: Stopping already running DeepStack instance...");
                        this.StopDeepstack();
                        Thread.Sleep(250);
                    }

                    Log("Starting DeepStack...");
                }

                Stopwatch SW = Stopwatch.StartNew();
                this.URLS = "";

                if (this.IsNewVersion)
                {
                    List<string> ports = Global.Split(this.Port, ",|");
                    this.Count = ports.Count;

                    this.CommandLine = "";
                    int pcnt = 0;
                    foreach (string CurPort in ports)
                    {
                        pcnt++;

                        Global.ClsProcess prc = new Global.ClsProcess();
                        prc.process.StartInfo.FileName = this.DeepStackEXE;
                        prc.process.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.DeepStackEXE);
                        if (this.CustomModelEnabled)
                        {
                            prc.process.StartInfo.Arguments = $"--MODELSTORE-DETECTION \"{this.CustomModelPath}\" --PORT {CurPort}";
                        }
                        else
                        {
                            string face = "";
                            string scene = "";
                            string detect = "";
                            string admin = "";
                            string api = "";
                            //string mode = "";

                            if (this.FaceAPIEnabled)
                                face = $"--VISION-FACE {this.FaceAPIEnabled} ";
                            if (this.SceneAPIEnabled)
                                scene = $"--VISION-SCENE {this.SceneAPIEnabled} ";
                            if (this.DetectionAPIEnabled)
                                detect = $"--VISION-DETECTION {this.DetectionAPIEnabled} ";
                            if (!string.IsNullOrEmpty(this.AdminKey))
                                admin = $"--ADMIN-KEY {this.AdminKey} ";
                            if (!string.IsNullOrEmpty(this.APIKey))
                                api = $"--API-KEY {this.APIKey} ";

                            //if (!string.IsNullOrEmpty(this.Mode))
                            //    mode = $"--MODE {this.Mode} ";

                            prc.process.StartInfo.Arguments = $"{face}{scene}{detect}{admin}{api}--PORT {CurPort}";
                        }
                        if (!AppSettings.Settings.deepstack_debug)
                        {
                            prc.process.StartInfo.CreateNoWindow = true;
                            prc.process.StartInfo.UseShellExecute = false;
                            prc.process.StartInfo.RedirectStandardOutput = true;
                            prc.process.StartInfo.RedirectStandardError = true;
                            prc.process.EnableRaisingEvents = true;
                            prc.process.OutputDataReceived += this.DSHandleServerProcMSG;
                            prc.process.ErrorDataReceived += this.DSHandleServerProcERROR;
                        }
                        else
                        {
                            prc.process.StartInfo.UseShellExecute = false;
                        }

                        prc.process.StartInfo.EnvironmentVariables["MODE"] = this.Mode;
                        prc.process.Exited += (sender, e) => this.DSProcess_Exited(sender, e, "deepstack.exe"); //new EventHandler(myProcess_Exited);
                        prc.FileName = this.DeepStackEXE;
                        prc.CommandLine = prc.process.StartInfo.Arguments;
                        Log($"Starting {pcnt} of {ports.Count}: {prc.process.StartInfo.FileName} {prc.process.StartInfo.Arguments}...");
                        prc.process.Start();

                        Global.WaitForProcessToStart(prc.process, 3000);

                        if (AppSettings.Settings.deepstack_highpriority)
                        {
                            prc.process.PriorityClass = ProcessPriorityClass.High;
                        }

                        if (!AppSettings.Settings.deepstack_debug)
                        {
                            prc.process.BeginOutputReadLine();
                            prc.process.BeginErrorReadLine();
                        }

                        this.ServerProc.Add(prc);

                        ClsURLItem url = new ClsURLItem($"http://127.0.0.1:{CurPort}/v1/vision/detection", AppSettings.Settings.AIURLList.Count + 1, URLTypeEnum.DeepStack);

                        this.CommandLine += $"{prc.FileName} {prc.CommandLine}\r\n";

                        this.URLS += $"{url.ToString()}\r\n";


                        if (!AppSettings.Settings.AIURLList.Contains(url))
                        {
                            Log("Automatically adding local Windows Deepstack URL: " + url.ToString());
                            AppSettings.Settings.AIURLList.Add(url);
                        }

                        Thread.Sleep(250);
                    }

                    this.IsStarted = true;
                    this.HasError = false;
                    Ret = true;

                    //Lets wait for the rest of the python.exe processes to spawn and set their priority too (otherwise they are normal)

                    int cnt = 0;
                    int cc = 0;
                    do
                    {

                        List<Global.ClsProcess> montys = Global.GetProcessesByPath(this.PythonEXE);
                        cnt = montys.Count;
                        if (montys.Count >= this.Count * 2)
                        {
                            //when deepstack is running normaly there will be 2 python.exe processes running for each deepstack.exe
                            //Set priority for each this way since we didnt start them in the first place...
                            if (AppSettings.Settings.deepstack_highpriority)
                            {
                                foreach (Global.ClsProcess prc in montys)
                                {
                                    cc++;
                                    if (Global.ProcessValid(prc))
                                    {
                                        try
                                        {
                                            prc.process.PriorityClass = ProcessPriorityClass.High;
                                            prc.process.StartInfo.UseShellExecute = false;
                                            prc.process.StartInfo.RedirectStandardOutput = true;
                                            prc.process.StartInfo.RedirectStandardError = true;
                                            prc.process.EnableRaisingEvents = true;
                                            prc.process.OutputDataReceived += this.DSHandlePythonProcMSG;
                                            prc.process.ErrorDataReceived += this.DSHandlePythonProcERROR;
                                            prc.process.Exited += (sender, e) => this.DSProcess_Exited(sender, e, "python.exe"); //new EventHandler(myProcess_Exited);
                                        }
                                        catch { }

                                    }
                                }
                            }
                            
                            this.PythonProc = montys;

                            break;
                        }
                        else
                        {
                            Log($"Debug: ...Waiting for {this.Count * 2} copies of {this.PythonEXE} to start (now={montys.Count})...");
                            Thread.Sleep(250);
                        }

                    } while (SW.ElapsedMilliseconds < 30000);  //wait 10 seconds max

                    this.RedisProc = Global.GetProcessesByPath(this.RedisEXE);

                    if (Global.ProcessValid(this.RedisProc))
                    {
                        if (AppSettings.Settings.deepstack_highpriority)
                        {
                            
                            this.RedisProc[0].process.PriorityClass = ProcessPriorityClass.High;
                            
                        }
                    }
                    else
                    {
                        this.HasError = true;
                        Log($"Error: 1 redis-server.exe processes did not start. (" + SW.ElapsedMilliseconds + "ms)");
                    }

                    if (cnt >= this.Count * 2)
                    {
                        
                        Log("Started in " + SW.ElapsedMilliseconds + "ms");
                    }
                    else 
                    {
                        this.HasError = true;
                        Log($"Error: {this.Count * 2} python.exe processes did not start within " + SW.ElapsedMilliseconds + "ms");
                    }

                    if (!this.HasError)
                    {
                        this.IsActivated = true;
                        this.IsStarted = true;
                    }

                }
                //else
                //{
                //    //First initialize with the py script

                //    Process InitProc = new Process();
                //    InitProc.StartInfo.FileName = this.PythonEXE;
                //    InitProc.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.PythonEXE);
                //    InitProc.StartInfo.Arguments = "../init.py";
                //    InitProc.StartInfo.UseShellExecute = false;
                //    InitProc.StartInfo.CreateNoWindow = true;
                //    InitProc.StartInfo.RedirectStandardOutput = true;
                //    InitProc.StartInfo.RedirectStandardError = true;
                //    InitProc.EnableRaisingEvents = true;
                //    InitProc.OutputDataReceived += this.DSHandleInitProcMSG;
                //    InitProc.ErrorDataReceived += this.DSHandleInitProcERROR;
                //    InitProc.Exited += (sender, e) => this.myProcess_Exited(sender, e, "Init:Python.exe"); //new EventHandler(myProcess_Exited);
                //    Log($"Starting {InitProc.StartInfo.FileName} {InitProc.StartInfo.Arguments}...");
                //    InitProc.Start();
                //    InitProc.PriorityClass = ProcessPriorityClass.High;  //always run this as high priority since it will initialize faster
                //    InitProc.BeginOutputReadLine();
                //    InitProc.BeginErrorReadLine();

                //    //next start the redis server...
                //    this.RedisProc = new Global.ClsProcess();
                //    this.RedisProc.process.StartInfo.FileName = this.RedisEXE;
                //    this.RedisProc.process.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.RedisEXE);
                //    this.RedisProc.process.StartInfo.UseShellExecute = false;
                //    this.RedisProc.process.StartInfo.CreateNoWindow = true;
                //    this.RedisProc.process.StartInfo.RedirectStandardOutput = true;
                //    this.RedisProc.process.StartInfo.RedirectStandardError = true;
                //    this.RedisProc.process.EnableRaisingEvents = true;
                //    this.RedisProc.process.OutputDataReceived += this.DSHandleRedisProcMSG;
                //    this.RedisProc.process.ErrorDataReceived += this.DSHandleRedisProcERROR;
                //    this.RedisProc.process.Exited += (sender, e) => this.myProcess_Exited(sender, e, "Redis.exe"); //new EventHandler(myProcess_Exited);
                //    this.RedisProc.FileName = this.RedisEXE;
                //    this.RedisProc.CommandLine = this.RedisEXE;
                //    Log($"Starting {this.RedisEXE}...");
                //    this.RedisProc.process.Start();
                //    if (AppSettings.Settings.deepstack_highpriority)
                //    {
                //        this.RedisProc.process.PriorityClass = ProcessPriorityClass.High;
                //    }
                //    this.RedisProc.process.BeginOutputReadLine();
                //    this.RedisProc.process.BeginErrorReadLine();

                //    //next, start the server

                //    this.ServerProc = new Global.ClsProcess();
                //    this.ServerProc.process.StartInfo.FileName = this.ServerEXE;
                //    this.ServerProc.process.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.ServerEXE);
                //    this.ServerProc.process.StartInfo.Arguments = $"-VISION-FACE={this.FaceAPIEnabled} -VISION-SCENE={this.SceneAPIEnabled} -VISION-DETECTION={this.DetectionAPIEnabled} -ADMIN-KEY={this.AdminKey} -API-KEY={this.APIKey} -PORT={this.Port}";
                //    this.ServerProc.process.StartInfo.CreateNoWindow = true;
                //    this.ServerProc.process.StartInfo.UseShellExecute = false;
                //    this.ServerProc.process.StartInfo.RedirectStandardOutput = true;
                //    this.ServerProc.process.StartInfo.RedirectStandardError = true;
                //    this.ServerProc.process.EnableRaisingEvents = true;
                //    this.ServerProc.process.OutputDataReceived += this.DSHandleServerProcMSG;
                //    this.ServerProc.process.ErrorDataReceived += this.DSHandleServerProcERROR;
                //    this.ServerProc.process.Exited += (sender, e) => this.myProcess_Exited(sender, e, "Server.exe"); //new EventHandler(myProcess_Exited);
                //    this.ServerProc.FileName = this.ServerEXE;
                //    this.ServerProc.CommandLine = this.ServerProc.process.StartInfo.Arguments;
                //    Log($"Starting {this.ServerProc.process.StartInfo.FileName} {this.ServerProc.process.StartInfo.Arguments}...");
                //    this.ServerProc.process.Start();
                //    if (AppSettings.Settings.deepstack_highpriority)
                //    {
                //        this.ServerProc.process.PriorityClass = ProcessPriorityClass.High;
                //    }

                //    this.ServerProc.process.BeginOutputReadLine();
                //    this.ServerProc.process.BeginErrorReadLine();

                //    //start the python intelligence.py script
                //    this.PythonProc = new Global.ClsProcess();
                //    this.PythonProc.process.StartInfo.FileName = this.PythonEXE;
                //    this.PythonProc.process.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.PythonEXE);
                //    this.PythonProc.process.StartInfo.Arguments = $"../intelligence.py -MODE={this.Mode} -VFACE={this.FaceAPIEnabled} -VSCENE={this.SceneAPIEnabled} -VDETECTION={this.DetectionAPIEnabled}";
                //    this.PythonProc.process.StartInfo.UseShellExecute = false;
                //    this.PythonProc.process.StartInfo.CreateNoWindow = true;
                //    this.PythonProc.process.EnableRaisingEvents = true;
                //    this.PythonProc.process.StartInfo.RedirectStandardOutput = true;
                //    this.PythonProc.process.StartInfo.RedirectStandardError = true;
                //    this.PythonProc.process.OutputDataReceived += this.DSHandlePythonProcMSG;
                //    this.PythonProc.process.ErrorDataReceived += this.DSHandlePythonProcERROR;
                //    this.PythonProc.process.Exited += (sender, e) => this.myProcess_Exited(sender, e, "Main:Python.exe"); //new EventHandler(myProcess_Exited);
                //    this.PythonProc.FileName = this.PythonEXE;
                //    this.PythonProc.CommandLine = this.PythonProc.process.StartInfo.Arguments;
                //    Log($"Starting {this.PythonProc.process.StartInfo.FileName} {this.PythonProc.process.StartInfo.Arguments}...");
                //    this.PythonProc.process.Start();
                //    if (AppSettings.Settings.deepstack_highpriority)
                //    {
                //        this.PythonProc.process.PriorityClass = ProcessPriorityClass.High;
                //    }


                //    this.PythonProc.process.BeginOutputReadLine();
                //    this.PythonProc.process.BeginErrorReadLine();


                //    this.IsStarted = true;
                //    this.HasError = false;
                //    Ret = true;

                //    //Lets wait for the rest of the python.exe processes to spawn and set their priority too (otherwise they are normal)

                //    int cnt = 0;
                //    do
                //    {

                //        List<Global.ClsProcess> montys = Global.GetProcessesByPath(this.PythonEXE);
                //        if (montys.Count >= 5)
                //        {
                //            //when deepstack is running normaly there will be 5 python.exe processes
                //            //Set priority for each this way since we didnt start them in the first place...
                //            cnt = montys.Count;
                //            if (AppSettings.Settings.deepstack_highpriority)
                //            {
                //                foreach (Global.ClsProcess prc in montys)
                //                {
                //                    if (Global.ProcessValid(prc))
                //                    {
                //                        try
                //                        {
                //                            prc.process.PriorityClass = ProcessPriorityClass.High;
                //                        }
                //                        catch { }

                //                    }
                //                }
                //            }
                //            break;
                //        }
                //        Thread.Sleep(100);

                //    } while (SW.ElapsedMilliseconds < 30000);  //wait 10 seconds max

                //    if (cnt == 5)
                //    {
                //        Log("Started in " + SW.ElapsedMilliseconds + "ms");
                //    }
                //    else if (cnt > 5)
                //    {
                //        this.HasError = true;
                //        this.IsStarted = true;
                //        Log("Error: More than 5 python.exe processes are running from the deepstack folder?  Manually stop/restart.   (" + SW.ElapsedMilliseconds + "ms)");
                //    }
                //    else if (cnt == 0)
                //    {
                //        this.HasError = true;
                //        this.IsStarted = true;
                //        Log("Error: 5 python.exe processes did not fully start in " + SW.ElapsedMilliseconds + "ms");
                //    }

                //}

            }
            catch (Exception ex)
            {
                this.IsStarted = false;
                this.HasError = true;
                Log("Error: Cannot start: " + Global.ExMsg(ex));
            }
            finally
            {
                this.Starting.WriteFullFence(false);
            }

            Global.SendMessage(MessageType.UpdateDeepstackStatus, "Manual start");


            return Ret;

        }

        public async Task<bool> StopDeepstackAsync()
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.
            return await Task.Run(() => this.StopDeepstack());
        }


        public bool StopDeepstack()
        {

            if (this.Stopping.ReadFullFence())
            {
                Log("Already stopping?");
                return false;
            }

            this.Stopping.WriteFullFence(true);

            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            bool Ret = false;
            bool err = false;

            Log("Stopping Deepstack...");
            Stopwatch sw = Stopwatch.StartNew();

            //Try to get current running processes in any case
            bool success = this.GetDeepStackRun();

            //more than one python process we need to take care of...  Sometimes MANY 
            bool perr = Global.KillProcesses(this.PythonProc);
            bool rerr = Global.KillProcesses(this.RedisProc);
            bool serr = Global.KillProcesses(this.ServerProc);

            err = !perr || !rerr || !serr;

            if (!err)
            {
                this.DeepStackProc = new List<Global.ClsProcess>();
                this.ServerProc = new List<Global.ClsProcess>();
                this.PythonProc = new List<Global.ClsProcess>();
                this.RedisProc = new List<Global.ClsProcess>();
                //this.DeepStackProc = null;
                Log("Debug: Stopped DeepStack in " + sw.ElapsedMilliseconds + "ms");
                Ret = true;
            }
            else
            {
                Log("Could not fully stop - This can happen for a few reasons: 1) This tool did not originally START deepstack.  2) If this tool is 32 bit it cannot stop 64 bit Deepstack process.  Kill manually via task manager - Server.exe, python.exe, redis-server.exe.");
            }

            this.IsStarted = false;

            this.Stopping.WriteFullFence(false);

            this.HasError = !Ret;

            Global.SendMessage(MessageType.UpdateDeepstackStatus, "Manual stop");

            return Ret;

        }
        public void ResetDeepstack()
        {
            try
            {
                if (this.IsStarted)
                {
                    Log("Stopping already running DeepStack instance...");
                    this.StopDeepstack();
                }

                Thread.Sleep(250);

                Log("Resetting DeepStack...");

                String curdir = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), ".deepstack");
                if (Directory.Exists(curdir))
                {
                    Log($"Removing {curdir}...");
                    Directory.Delete(curdir, true);
                }
                curdir = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), "DeepStack");
                if (Directory.Exists(curdir))
                {
                    Log($"Removing {curdir}...");
                    Directory.Delete(curdir, true);
                }
                curdir = Path.Combine(Environment.GetEnvironmentVariable("LOCALAPPDATA"), "DeepStack");
                if (Directory.Exists(curdir))
                {
                    Log($"Removing {curdir}...");
                    Directory.Delete(curdir, true);
                }

                List<FileInfo> files = Global.GetFiles(this.DeepStackFolder, "*.pyc");
                if (files.Count > 0)
                {
                    Log($"Removing {files.Count} compiled Python files {this.DeepStackFolder}\\*.PYC...");
                    foreach (FileInfo fi in files)
                    {
                        fi.Delete();
                    }
                }

            }
            catch (Exception ex)
            {

                Log($"Error: {Global.ExMsg(ex)}");
            }
        }
        private void DSProcess_Exited(object sender, System.EventArgs e, string Name)
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
                catch
                {

                    output += " (error accessing process properties)";
                }
            }
            if (!Name.Contains("Init:"))
            {
                this.IsStarted = false;
                this.HasError = true;
            }
            Log(output, "");
            Global.SendMessage(MessageType.UpdateDeepstackStatus, "Process exited");


        }

        private void DSHandleRedisProcERROR(object sender, DataReceivedEventArgs line)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(line.Data) || Global.progress == null)
                {
                    return;
                }

                if (AppSettings.Settings.deepstack_debug)
                    Log($"Debug: DeepStack>> STDERR: {line.Data}", "", "", "REDIS-SERVER.EXE");

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void DSHandleRedisProcMSG(object sender, DataReceivedEventArgs line)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(line.Data) || Global.progress == null)
                {
                    return;
                }

                if (AppSettings.Settings.deepstack_debug)
                    Log($"Debug: DeepStack>> {line.Data}", "", "", "REDIS-SERVER.EXE");

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void DSHandleInitProcERROR(object sender, DataReceivedEventArgs line)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(line.Data) || Global.progress == null)
                {
                    return;
                }

                if (AppSettings.Settings.deepstack_debug)
                    Log($"Debug: DeepStack>> Init: STDERR: {line.Data}", "", "PYTHON.EXE");

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void DSHandleInitProcMSG(object sender, DataReceivedEventArgs line)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(line.Data) || Global.progress == null)
                {
                    return;
                }

                if (AppSettings.Settings.deepstack_debug)
                    Log($"Debug: DeepStack>> Init: {line.Data}", "", "", "PYTHON.EXE");

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void DSHandlePythonProcERROR(object sender, DataReceivedEventArgs line)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(line.Data) || Global.progress == null)
                {
                    return;
                }

                if (AppSettings.Settings.deepstack_debug)
                    Log($"Debug: DeepStack>> STDERR: {line.Data}", "", "", "PYTHON.EXE");

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void DSHandlePythonProcMSG(object sender, DataReceivedEventArgs line)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(line.Data) || Global.progress == null)
                {
                    return;
                }

                if (AppSettings.Settings.deepstack_debug)
                    Log($"Debug: DeepStack>> {line.Data}", "", "", "PYTHON.EXE");

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void DSHandleServerProcERROR(object sender, DataReceivedEventArgs line)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(line.Data) || Global.progress == null)
                {
                    return;
                }

                if (AppSettings.Settings.deepstack_debug)
                    Log($"Debug: DeepStack>> STDERR: {line.Data}", "", "", "SERVER.EXE");

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void DSHandleServerProcMSG(object sender, DataReceivedEventArgs line)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(line.Data) || Global.progress == null)
                {
                    return;
                }

                //--VISION-DETECTION True --PORT 84
                Process prc = (Process)sender;
                string dsinfo = $"DeepStack:{Global.GetWordBetween(prc.StartInfo.Arguments, "PORT ", " |")}>>";

                //Console.WriteLine(Message);
                if (line.Data.IndexOf("visit localhost to activate deepstack", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    this.IsActivated = false;
                }
                else if (line.Data.IndexOf("deepstack is active", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    this.IsActivated = true;
                }

                if (line.Data.IndexOf("vision/detection", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    this.VisionDetectionRunning = true;
                }

                Log($"Debug: {dsinfo} {line.Data}", "", "", "DEEPSTACK.EXE");

            }
            catch { }
        }
        

    }
}
