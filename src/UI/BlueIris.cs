using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using static AITool.AITOOL;

namespace AITool


{
    public enum BlueIrisResult
    {
        Valid,
        NeedsRemoteRegistryService,
        NeedsPermission,
        NeedsAdminSharesEnabled,   //https://www.repairwin.com/enable-admin-shares-windows-10-8-7/
        InvalidHostOrIP,
        HasFirewall,
        CouldNotOpenRemoteRegistry,
        Unknown
    }
    public class BlueIris
    {
        public List<String> ClipPaths = new List<String>();
        public List<String> Cameras = new List<String>();
        public string AppPath = "";
        public string URL = "";
        public string ServerName = "127.0.0.1";
        public string ServerPort = "81";
        public BlueIrisResult Result = BlueIrisResult.Unknown;
        public bool IsHTTPS = false;
        public bool IsLocalhost = false;

        public BlueIris()
        {

        }

        public async Task<string> UpdateRemotePathAsync(string InPath)
        {
            if (this.IsLocalhost)
                return InPath;

            //         d:\blueiris\clips\pathname
            //\\server\d$\blueiris\clips\pathname
            //return $"\\\\{this.ServerName}\\{InPath.Replace(":", "$")}";
            //https://www.repairwin.com/enable-admin-shares-windows-10-8-7/

            return await Global.GetBestRemotePathAsync(InPath, this.ServerName);

        }



        public async Task<BlueIrisResult> RefreshBIInfoAsync(string ServernameOrIP)
        {
            using var Trace = new Trace();  //This c# 8.0 using feature will auto dispose when the function is done.

            this.Result = BlueIrisResult.Unknown;
            this.URL = "";
            this.ServerName = ServernameOrIP;

            RegistryKey RemoteKey = null;

            try
            {
                Log($"Debug: Reading BlueIris settings from registry from '{ServernameOrIP}'...");

                Global.UpdateProgressBar($"Reading BlueIris Registry info on '{ServernameOrIP}'...", 1, 1, 1);

                this.IsLocalhost = string.IsNullOrEmpty(ServernameOrIP) ||
                                    ServernameOrIP == "." ||
                                    string.Equals(ServernameOrIP, "localhost", StringComparison.OrdinalIgnoreCase) ||
                                    ServernameOrIP == "127.0.0.1" ||
                                    ServernameOrIP == "0.0.0.0" ||
                                    string.Equals(ServernameOrIP, Dns.GetHostName(), StringComparison.OrdinalIgnoreCase);

                if (this.IsLocalhost)
                {
                    RemoteKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
                }
                else
                {
                    //quick ping to validate first
                    Global.ClsPingOut cpo = await Global.IsConnected(ServernameOrIP, 500, 1);
                    if (!cpo.Success)
                    {
                        this.Result = BlueIrisResult.InvalidHostOrIP;
                        Log($"Error: Could not connect to BlueIris server '{ServernameOrIP}': {cpo.PingError}");
                        return this.Result;
                    }
                    Log($"Debug: Opening remote registry on '{ServernameOrIP}'...");
                    Stopwatch sw = Stopwatch.StartNew();

                    RemoteKey = await Task.Run(() => RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ServernameOrIP));

                    if (RemoteKey != null)
                        Log($"Debug: Successfully connected to remote registry on {ServernameOrIP} in {sw.ElapsedMilliseconds}ms.");
                    else
                    {
                        this.Result = BlueIrisResult.CouldNotOpenRemoteRegistry;
                        Log($"Error: Could not open remote registry on {ServernameOrIP} ({sw.ElapsedMilliseconds}ms.)");
                        return this.Result;
                    }
                }

                using (RegistryKey key = RemoteKey.OpenSubKey("Software\\Perspective Software\\Blue Iris\\Install", false))
                {
                    if (key != null)
                    {
                        string ap = Convert.ToString(key.GetValue("AppPath4"));
                        if (!string.IsNullOrWhiteSpace(ap))
                        {
                            this.AppPath = await this.UpdateRemotePathAsync(ap);

                            if (this.IsLocalhost)
                                Log($"Debug: BlueIris app path found: {ap}");
                            else
                                Log($"Debug: BlueIris app path found: {ap} (Remote={this.AppPath})");

                            //Test the remote connection...
                            if (!this.IsLocalhost && !Directory.Exists(this.AppPath))
                            {
                                Log($"Error: Cannot access remote BlueIris install path '{this.AppPath}' - Make sure you have permissions to remotely access and your 'Administrative shares' have been enabled (c$, d$, etc): https://www.repairwin.com/enable-admin-shares-windows-10-8-7/");
                                this.Result = BlueIrisResult.NeedsAdminSharesEnabled;
                                return this.Result;
                            }
                            else if (this.IsLocalhost && !Directory.Exists(this.AppPath))
                            {
                                Log($"Error: Cannot access BlueIris install path '{this.AppPath}'.");
                            }

                        }
                    }
                    else
                    {
                        Log("Debug: Could not find BlueIris INSTALL info in the registry.");
                    }


                }

                using (RegistryKey key = RemoteKey.OpenSubKey("Software\\Perspective Software\\Blue Iris\\server", false))
                {
                    if (key != null)
                    {
                        this.IsHTTPS = (Convert.ToInt32(key.GetValue("httpslan")) == 1);
                        this.ServerName = Convert.ToString(key.GetValue("lanip")).Trim();
                        this.ServerPort = Convert.ToString(key.GetValue("port")).Trim();
                        if (!string.IsNullOrWhiteSpace(this.ServerName))
                        {
                            if (this.IsHTTPS)  //maybe need to check secureonly setting also??
                            {
                                this.URL = "https://" + this.ServerName + ":" + this.ServerPort;
                            }
                            else
                            {
                                this.URL = "http://" + this.ServerName + ":" + this.ServerPort;
                            }
                            Log("Debug: BlueIris URL found: " + this.URL);

                        }
                        else
                        {
                            Log("Error: BlueIris LANIP registry key not found?");
                        }
                    }
                    else
                    {
                        Log("Debug: Could not find BlueIris SERVER info in the registry.");
                    }

                }


                using (RegistryKey key = RemoteKey.OpenSubKey("Software\\Perspective Software\\Blue Iris\\clips\\folders", false))
                {
                    if (key != null)
                    {
                        foreach (var sk in key.GetSubKeyNames())
                        {

                            using (RegistryKey curkey = key.OpenSubKey(sk))
                            {
                                if (curkey != null)
                                {
                                    string path = Convert.ToString(curkey.GetValue("path"));
                                    if (!string.IsNullOrWhiteSpace(path))
                                    {
                                        string pth = await this.UpdateRemotePathAsync(path.Trim());
                                        this.ClipPaths.Add(pth);
                                        if (this.IsLocalhost)
                                            Log($"Debug: BlueIris clip path found: {pth}");
                                        else
                                            Log($"Debug: BlueIris app path found: {path} (Remote={pth})");

                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        Log("Debug: Could not find BlueIris CLIPS info in the registry.");
                    }


                }

                using (RegistryKey key = RemoteKey.OpenSubKey("Software\\Perspective Software\\Blue Iris\\Cameras", false))
                {
                    if (key != null)
                    {
                        foreach (var sk in key.GetSubKeyNames())
                        {

                            using (RegistryKey curkey = key.OpenSubKey(sk))
                            {
                                if (curkey != null)
                                {
                                    bool enabled = (Convert.ToInt32(curkey.GetValue("enabled")) == 1);
                                    if (enabled)
                                    {
                                        string shortname = Convert.ToString(curkey.GetValue("shortname"));
                                        if (!string.IsNullOrWhiteSpace(shortname))
                                        {
                                            this.Cameras.Add(shortname);
                                            Log("Debug: BlueIris camera found: " + shortname);

                                        }
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        Log("Debug: Could not find BlueIris CAMERAS info in the registry.");
                    }

                }



                bool IsValid = (this.ClipPaths.Count > 0 && !String.IsNullOrWhiteSpace(this.AppPath) && !string.IsNullOrWhiteSpace(this.URL) && Directory.Exists(this.AppPath));

                if (IsValid)
                    this.Result = BlueIrisResult.Valid;

            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("The network path was not found", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Log($"Error: The remote machine needs the 'Remote Registry' service enabled (automatic) + started on '{ServernameOrIP}': " + Global.ExMsg(ex));
                    this.Result = BlueIrisResult.NeedsRemoteRegistryService;
                }
                //System.UnauthorizedAccessException: 'Attempted to perform an unauthorized operation.'
                else if (ex.Message.IndexOf("unauthorized operation", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    string userinfo = Environment.GetEnvironmentVariable("USERDOMAIN") + @"\" + Environment.GetEnvironmentVariable("USERNAME");
                    Log($"Error: Give the current user ({userinfo}) access to '{ServernameOrIP}': " + Global.ExMsg(ex));
                    this.Result = BlueIrisResult.NeedsPermission;
                }
                else
                {
                    Log("Error: Got error while reading BlueIris registry: " + Global.ExMsg(ex));
                }
                this.Result = BlueIrisResult.Unknown;
            }
            finally
            {
                if (RemoteKey != null)
                    RemoteKey.Dispose();
            }

            return this.Result;
        }
    }
}
