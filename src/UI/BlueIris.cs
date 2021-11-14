using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using static AITool.AITOOL;

namespace AITool


{
    public enum BlueIrisResult
    {
        Valid,
        NeedsRemoteRegistryServiceEnabled,
        NeedsPermission,
        NeedsAdminSharesEnabled,   //https://www.repairwin.com/enable-admin-shares-windows-10-8-7/
        InvalidHostOrIP,
        HasFirewall,
        CouldNotOpenRemoteRegistry,
        NotInstalled,
        Unknown
    }

    public class BICamera
    {

    }

    public class BIUser
    {
        public string Name = "";
        public string Password = "";
        public bool IsAdmin = false;
        public bool IsEnabled = false;
    }
    public class BlueIris
    {
        public List<String> ClipPaths = new List<String>();
        public List<String> Cameras = new List<String>();
        public List<BIUser> Users = new List<BIUser>();
        public string AppPath = "";
        public string URL = "";
        public string ServerName = "127.0.0.1";
        public string ServerPort = "81";
        public BlueIrisResult Result = BlueIrisResult.Unknown;
        public bool IsHTTPS = false;
        public bool IsLocalhost = false;
        public string VersionStr = "";
        public double Latitude = 39.809734;
        public double Longitude = -98.555620;

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
            this.ClipPaths.Clear();

            RegistryKey RemoteKey = null;

            try
            {
                if (ServernameOrIP.IsEmpty())
                {
                    this.Result = BlueIrisResult.NotInstalled;
                    Log($"Debug: No BlueIris server specified.");
                    return this.Result;
                }

                Log($"Debug: Reading BlueIris settings from registry from '{ServernameOrIP}'...");


                this.IsLocalhost = await Global.IsLocalHostAsync(ServernameOrIP);

                if (this.IsLocalhost)
                {
                    RemoteKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default);
                }
                else
                {
                    Global.UpdateProgressBar($"Reading BlueIris Registry info on '{ServernameOrIP}'...", 1, 1, 1);
                    //quick ping to validate first
                    Global.ClsPingOut cpo = await Global.IsConnected(ServernameOrIP, 500, 1);
                    if (!cpo.Success)
                    {
                        this.Result = BlueIrisResult.InvalidHostOrIP;
                        Log($"Error: Could not PING BlueIris server '{ServernameOrIP}': {cpo.PingError}");
                        return this.Result;
                    }

                    //string fixip = ServernameOrIP;

                    //we have to get the hostname if it is an ipv6 ip address:
                    //fixip = await Global.GetHostNameAsync(fixip);

                    Log($"Debug: Ping response={cpo.TotalTimeMS}ms, opening remote registry on '{ServernameOrIP}'...");

                    Stopwatch sw = Stopwatch.StartNew();

                    //When the remote registry service is not running we get System.IO.IOException: 'The network path was not found.
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

                            var versionInfo = FileVersionInfo.GetVersionInfo(Path.Combine(this.AppPath, "BlueIris.exe"));
                            this.VersionStr = versionInfo.FileVersion;

                            Log($"Debug: BlueIris found. Version '{this.VersionStr}', app path '{this.AppPath}'");

                            //Test the remote connection...
                            if (!this.IsLocalhost && !Directory.Exists(this.AppPath))
                            {
                                Log($"Error: Cannot access remote BlueIris install path '{this.AppPath}' - Make sure you have permissions to remotely access and MAP A DRIVE to your remote blue iris CLIPS folder.  (Or make sure 'Administrative shares' have been enabled (c$, d$, etc)): https://www.repairwin.com/enable-admin-shares-windows-10-8-7/");
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
                        this.Result = BlueIrisResult.NotInstalled;
                        Log("Debug: Could not find BlueIris INSTALL info in the registry.");
                        return this.Result;

                    }


                }

                if (!string.IsNullOrEmpty(this.AppPath))
                {

                    using (RegistryKey key = RemoteKey.OpenSubKey("Software\\Perspective Software\\Blue Iris\\server\\users", false))
                    {
                        if (key != null)
                        {
                            foreach (string sk in key.GetSubKeyNames())
                            {

                                using (RegistryKey curkey = key.OpenSubKey(sk))
                                {
                                    if (curkey != null)
                                    {
                                        bool enabled = curkey.GetValue("enabled", 0).ToString().ToInt() == 1;
                                        bool admin = curkey.GetValue("admin", 0).ToString().ToInt() == 1;

                                        if (!enabled || !admin) // || string.Equals(sk, "local_console", StringComparison.OrdinalIgnoreCase))
                                            continue;

                                        BIUser user = new BIUser();
                                        user.Name = sk;

                                        //The password can now be encrypted rather than just base64 encoded
                                        string pass = Convert.ToString(curkey.GetValue("password", ""));
                                        if (!pass.IsEmpty())
                                            try { user.Password = Encoding.UTF8.GetString(Convert.FromBase64String(pass)); } catch (Exception) { };

                                        this.Users.Add(user);
                                        Log($"Debug: Found user '{user.Name}', password '{pass.ReplaceChars('*')}'");

                                    }

                                }
                            }
                        }

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
                                this.URL = "https://" + Global.IP2Str(this.ServerName, Global.IPType.URL) + ":" + this.ServerPort;
                            }
                            else
                            {
                                this.URL = "http://" + Global.IP2Str(this.ServerName, Global.IPType.URL) + ":" + this.ServerPort;
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

                using (RegistryKey key = RemoteKey.OpenSubKey("Software\\Perspective Software\\Blue Iris\\Options", false))
                {
                    if (key != null)
                    {
                        this.Latitude = Global.GetNumberDbl(key.GetValue("latitude", "39.809734").ToString());
                        this.Longitude = Global.GetNumberDbl(key.GetValue("longitude", "-98.555620").ToString());
                    }
                    else
                    {
                        Log("Debug: Could not find BlueIris OPTIONS info in the registry.");
                    }

                }

                bool IsValid = (this.ClipPaths.Count > 0 && !String.IsNullOrWhiteSpace(this.AppPath) && !string.IsNullOrWhiteSpace(this.ServerName) && Directory.Exists(this.AppPath));

                if (IsValid)
                    this.Result = BlueIrisResult.Valid;

            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("The network path was not found", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Log($"Error: The remote machine needs the 'Remote Registry' service enabled (automatic) + started on '{ServernameOrIP}': " + ex.Msg());
                    this.Result = BlueIrisResult.NeedsRemoteRegistryServiceEnabled;
                }
                //System.UnauthorizedAccessException: 'Attempted to perform an unauthorized operation.'
                else if (ex.Message.IndexOf("unauthorized operation", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    string userinfo = Environment.GetEnvironmentVariable("USERDOMAIN") + @"\" + Environment.GetEnvironmentVariable("USERNAME");
                    Log($"Error: Give the current user ({userinfo}) access to '{ServernameOrIP}': " + ex.Msg());
                    this.Result = BlueIrisResult.NeedsPermission;
                }
                else
                {
                    Log("Error: Got error while reading BlueIris registry: " + ex.Msg());
                    this.Result = BlueIrisResult.Unknown;
                }
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
