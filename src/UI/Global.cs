﻿using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Globalization;
using static AITool.AITOOL;


namespace AITool
{
    // =============================================================
    // ALL FUNCTIONS HERE ARE GENERIC/SHARED AND NOT UNIQUE TO AITOOL
    // NO direct UI interaction
    // =============================================================

    public static class Global
    {
        public static IProgress<ClsMessage> progress = null;

        //this may speed up json serialization
        public static DefaultContractResolver JSONContractResolver = null;

        /// <summary>
        ///     ''' Gets a value indicating whether the application is a windows service.
        ///     ''' </summary>
        ///     ''' <value>
        ///     ''' <c>true</c> if this instance is service; otherwise, <c>false</c>.

        ///     ''' </value>
        public static bool IsService
        {
            get
            {
                // Determining whether or not the host application is a service is
                // an expensive operation (it uses reflection), so we cache the
                // result of the first call to this method so that we don't have to
                // recalculate it every call.

                // If we have not already determined whether or not the application
                // is running as a service...
                if (!_isService.HasValue)
                {

                    // Get details of the host assembly.
                    System.Reflection.Assembly entryAssembly = System.Reflection.Assembly.GetEntryAssembly();

                    // Get the method that was called to enter the host assembly.
                    System.Reflection.MethodInfo entryPoint = entryAssembly.EntryPoint;

                    // If the base type of the host assembly inherits from the
                    // "ServiceBase" class, it must be a windows service. We store
                    // the result ready for the next caller of this method.
                    _isService = (entryPoint.ReflectedType.BaseType.FullName == "System.ServiceProcess.ServiceBase");
                }

                // Return the cached result.
                return System.Convert.ToBoolean(_isService);
            }
        }

        private static Nullable<bool> _isService = default(Boolean?);

        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto)]
        public static extern long StrFormatByteSize(long fileSize, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder buffer, int bufferSize);
       
        public static string FormatBytes(long filesize)
        {
            StringBuilder sb = new StringBuilder();
            StrFormatByteSize(filesize, sb, sb.Capacity);
            return sb.ToString();
        }

        public static bool IsTimeBetween(DateTime time, string span)
        {
            // convert datetime to a TimeSpan
            TimeSpan now = time.TimeOfDay;

            string[] splt = span.Split('-');
            TimeSpan BeginSpan = TimeSpan.Parse(splt[0]);
            TimeSpan EndSpan= TimeSpan.Parse(splt[1]);
            // see if start comes before end
            if (BeginSpan < EndSpan)
                return BeginSpan <= now && now <= EndSpan;
            // start is after end, so do the inverse comparison
            return !(EndSpan < now && now < BeginSpan);
        }

        public static async Task<bool> DirectoryExistsAsync(string filename, int TimeoutMS = 20000)
        {
            //run the function in another thread
            CancellationTokenSource cts = new CancellationTokenSource(TimeoutMS);
            try
            {
                return await Task.Run(() => Directory.Exists(filename), cts.Token);
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public static string MappedDriveToUNCPath(string path)
        {
            if (!path.StartsWith(@"\\"))
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("Network\\" + path[0]))
                {
                    if (key != null)
                    {
                        return key.GetValue("RemotePath").ToString() + path.Remove(0, 2).ToString();
                    }
                }
            }
            return path;
        }

        public class MappedDrive
        {
            public string DriveLetter = "";
            public string Path = "";
        }

        public static async Task<string> GetBestRemotePathAsync(string RemoteLocalPath, string RemoteMachineNameOrIP)
        {
            //We are taking a path like C:\BlueIris\clips\blah read from a remote computer's registry
            //and trying to convert it to an accessible path on the current computer.
            string ret = RemoteLocalPath;
            string lastremotepathpart = Global.Split(RemoteLocalPath, @"\").Last();
            string ip = "";
            string hostname = "";
            Stopwatch sw = Stopwatch.StartNew();

            if (!RemoteLocalPath.StartsWith(@"\\"))
            {
                //Make sure we have the IP address
                IPAddress ipa = await GetIPAddressFromHostnameAsync(RemoteMachineNameOrIP);
                ip = ipa.ToString();
                hostname = await GetHostNameAsync(RemoteMachineNameOrIP);

                //first look for a mapped drive letter:
                string letter = RemoteLocalPath.Substring(0, 1);
                List<MappedDrive> mapped = new List<MappedDrive>();
                using RegistryKey key = Registry.CurrentUser.OpenSubKey("Network");
                if (key != null)
                {
                    List<string> drives = key.GetSubKeyNames().ToList();
                    foreach (string drv in drives)
                    {
                        using RegistryKey drvkey = key.OpenSubKey(drv);
                        if (drvkey != null)
                        {
                            string remotepath = drvkey.GetValue("RemotePath").ToString();
                            if (!string.IsNullOrEmpty(remotepath) && remotepath.StartsWith(@"\\"))
                            {
                                string mappedserver = GetWordBetween(remotepath, @"\\", @"\");

                                if (string.Equals(mappedserver, ip, StringComparison.OrdinalIgnoreCase) || string.Equals(mappedserver, hostname, StringComparison.OrdinalIgnoreCase))
                                {
                                    MappedDrive md = new MappedDrive();
                                    md.DriveLetter = drv;
                                    md.Path = remotepath;
                                    mapped.Add(md);
                                }
                            }
                        }
                    }
                }

                //first pass, try to get any matches where some of the UNC path matches...
                //there would have to be a shared part of the path
                //            C:\clips\myclippath
                //\\server\share\clips\myclippath
                foreach (MappedDrive md in mapped)
                {
                    string sharedpath = GetSharedPath(md.Path, RemoteLocalPath, false).TrimEnd(@"\".ToCharArray());
                    if (!string.IsNullOrEmpty(sharedpath))
                    {
                        Log($"Debug: Found shared path in {sw.ElapsedMilliseconds}ms on '{RemoteMachineNameOrIP}' for path '{RemoteLocalPath}': {sharedpath}");
                        return sharedpath;
                    }
                }

                List<string> spltpth = Global.Split(RemoteLocalPath, "\\");

                //search for last two parts of the path UNDER each of the shares
                string lastpath = spltpth[spltpth.Count - 1];
                string nexttolast = "";

                if (spltpth.Count - 2 > 0)
                {
                    nexttolast = spltpth[spltpth.Count - 2];
                    string searchpath = $"{nexttolast}\\{lastpath}";
                    foreach (MappedDrive md in mapped)
                    {
                        string checkpath = Path.Combine(md.Path, searchpath);
                        if (await Global.DirectoryExistsAsync(checkpath))
                        {
                            Log($"Debug: Found remote path in {sw.ElapsedMilliseconds}ms on '{RemoteMachineNameOrIP}' for path '{RemoteLocalPath}': {checkpath}");
                            return checkpath;
                        }
                    }
                }


                //            C:\clips\myclippath
                //\\server\share\clips\myclippath

                //C:\BlueIrisStorage\Alerts
                //\\server\BlueIrisStorage

                foreach (MappedDrive md in mapped)
                {
                    string checkpath = Path.Combine(md.Path, lastpath);
                    if (await Global.DirectoryExistsAsync(checkpath))
                    {
                        Log($"Debug: Found remote path in {sw.ElapsedMilliseconds}ms on '{RemoteMachineNameOrIP}' for path '{RemoteLocalPath}': {checkpath}");
                        return checkpath;
                    }
                }


                ret = $"\\\\{RemoteMachineNameOrIP}\\{RemoteLocalPath.Replace(":", "$")}";
                //resort to using admin shares (have to be enabled through group policy in newer versions of windows)
                Log($"Debug: Found ADMIN share in {sw.ElapsedMilliseconds}ms '{RemoteMachineNameOrIP}' for path '{RemoteLocalPath}': {ret}");

            }
            return ret;
        }

        public static string GetSharedPath(string BasePath, string SrcPath, bool OnlyPartial)
        {
            // Dim NetRootPth As String = "\\server\admlibrary\Software\AutoDesk\Test_CAD_State_Kit"
            // Dim SrcPth As String     = "                              C:\Test\Test_CAD_State_Kit\C3D 2021\Utilities"
            // Output=                     \\server\admlibrary\Software\AutoDesk\Test_CAD_State_Kit\C3D 2021\Utilities
            string Ret = "";
            try
            {
                // Dim com As String = FindCommonPath(pths)
                string[] nps = BasePath.Trim().Split('\\');
                string SharedPth = "";
                bool Fnd = false;
                foreach (string pp in nps)
                {
                    if (!OnlyPartial)
                    {
                        if (pp == "")
                            SharedPth = SharedPth + @"\";
                        else
                            SharedPth = SharedPth + pp + @"\";
                    }

                    string[] rps = SrcPath.Trim().Split('\\');
                    foreach (string rp in rps)
                    {
                        if (Fnd)
                        {
                            if (rp == "")
                                SharedPth = SharedPth + @"\";
                            else
                                SharedPth = SharedPth + rp + @"\";
                        }
                        else if (pp != "" && string.Equals(pp, rp, StringComparison.OrdinalIgnoreCase))
                        {
                            Fnd = true;
                            if (OnlyPartial)
                                SharedPth = SharedPth + rp + @"\";
                        }
                    }
                    if (Fnd)
                        break;
                }
                if (Fnd)
                    Ret = SharedPth;
            }
            catch (Exception ex)
            {
                Ret = "";
                Log("Error: " + ex.Message);
            }
            return Ret;
        }

        public static bool IsRegexPatternValid(string pattern)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(pattern) && pattern.Length > 2)
                {
                    System.Text.RegularExpressions.Regex test = new System.Text.RegularExpressions.Regex(pattern);
                    return true;
                }
            }
            catch { }
            return false;
        }

        public static async Task<string> GetHostNameAsync(string IPAddressOrHostName)
        {
            try
            {
                if (!IsValidIPAddress(IPAddressOrHostName))
                    return IPAddressOrHostName;  //assume valid hostname if it doesnt look like an ip address

                IPHostEntry entry = await Dns.GetHostEntryAsync(IPAddressOrHostName);
                if (entry != null)
                {
                    return entry.HostName;
                }
            }
            catch (Exception ex)
            {
                Log("Error: " + ex.Message);
            }

            return IPAddressOrHostName;

        }
        public static async Task<IPAddress> GetIPAddressFromHostnameAsync(string HostNameOrIPAddres = "")
        {
            IPAddress ret = default(IPAddress);
            try
            {
                if (IsValidIPAddress(HostNameOrIPAddres))
                    return GetIPAddressFromIPString(HostNameOrIPAddres);

                IPHostEntry Host;
                if (string.IsNullOrWhiteSpace(HostNameOrIPAddres))
                    Host = await Dns.GetHostEntryAsync(Dns.GetHostName());
                else
                    Host = await Dns.GetHostEntryAsync(HostNameOrIPAddres);

                foreach (IPAddress IP in Host.AddressList)
                {
                    if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        // just return the first one
                        ret = IP;
                        break;
                    }
                }
                if (!IsValidIPAddress(ret))
                {
                    // fall back to ipv6
                    foreach (IPAddress IP in Host.AddressList)
                    {
                        if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                        {
                            // just return the first one
                            ret = IP;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Error: Hostname '{HostNameOrIPAddres}': {ExMsg(ex)}");
            }

            return ret;
        }
        public static IPAddress GetIPAddressFromIPString(string IP)
        {
            IPAddress ret = default(IPAddress);
            if (!string.IsNullOrEmpty(IP))
            {
                IPAddress IPTst;
                if (IPAddress.TryParse(IP, out IPTst) && !IPTst.Equals(IPAddress.None))
                    return IPTst;
            }
            return ret;
        }
        public static bool IsValidIPAddress(string IP)
        {
            if (!string.IsNullOrEmpty(IP))
            {
                IPAddress IPTst;
                if (IPAddress.TryParse(IP, out IPTst) && !IPTst.Equals(IPAddress.None))
                    return true;
            }
            return false;
        }
        public static bool IsValidIPAddress(IPAddress IP)
        {
            if (IP != null && !IP.Equals(IPAddress.None))
            {
                IPAddress IPTst;
                if (IPAddress.TryParse(IP.ToString(), out IPTst) && !IPTst.Equals(IPAddress.None))
                    return true;
            }
            return false;
        }

        public class ClsPingOut
        {
            public bool Success = false;
            public PingReply PingReply = null;
            public long DNSResolveMS = 0;
            public string PingError = "";
            public int Hops = 0;
            public int Retries = 0;
            public long AvgTimeMS = 0;
            public long MaxTimeMS = 0;
            public long MinTimeMS = 0;
            public long TotalTimeMS = 0;
            public List<long> Pings = new List<long>();
        }

        public static async Task<ClsPingOut> IsConnected(string HostOrIPToPing = "www.google.com", int TimeoutMS = 2000, int RetryCount = 3, int DelayMS = 25)
        {
            ClsPingOut ret = new ClsPingOut();
            Stopwatch SW = Stopwatch.StartNew();

            try
            {
                IPAddress IP = null;

                if (IsValidIPAddress(HostOrIPToPing))
                    IP = GetIPAddressFromIPString(HostOrIPToPing);
                else
                    IP = await GetIPAddressFromHostnameAsync(HostOrIPToPing);

                ret.DNSResolveMS = SW.ElapsedMilliseconds;

                if (!IsValidIPAddress(IP))
                    return ret;

                Log($"Debug: Pinging {HostOrIPToPing} ({IP.ToString()}) With timeout:{TimeoutMS}ms And Ping Retry Count:{RetryCount}...");
                for (int Tries = 1; Tries <= RetryCount; Tries++)
                {
                    ret.Retries = Tries;
                    byte[] buffer = new byte[32];
                    PingOptions pingOptions = new PingOptions(128, false);

                    int TTLBefore = pingOptions.Ttl;
                    try
                    {
                        using (Ping Myping = new Ping())
                        {
                            ret.PingReply = await Myping.SendPingAsync(IP, TimeoutMS, buffer, pingOptions);
                        }
                    }
                    catch (Exception ex)
                    {
                        ret.PingError = ex.GetBaseException().Message;
                    }
                    if (ret.PingReply != null)
                    {
                        ret.Success = (ret.PingReply.Status == IPStatus.Success);
                        ret.PingError = ret.PingReply.Status.ToString();
                        if (ret.PingReply.Options != null) //ipv6 returns null
                            ret.Hops = TTLBefore - ret.PingReply.Options.Ttl;
                        if (ret.Success)
                        {
                            ret.Pings.Add(ret.PingReply.RoundtripTime);
                            break;
                        }
                    }

                    // wait before next try
                    await Task.Delay(DelayMS);
                }
            }
            catch (Exception ex)
            {
                ret.PingError = $"{ExMsg(ex)} (Site={HostOrIPToPing} Timeout was {TimeoutMS}ms)";
                Log($"Error: {ret.PingError}");
            }
            finally
            {
            }

            if (ret.Pings.Count > 0)
            {
                ret.AvgTimeMS = System.Convert.ToInt64(ret.Pings.Average());
                ret.MaxTimeMS = System.Convert.ToInt64(ret.Pings.Max());
                ret.MinTimeMS = System.Convert.ToInt64(ret.Pings.Min());
            }

            SW.Stop();
            ret.TotalTimeMS = SW.ElapsedMilliseconds;

            Log($"Debug: ...Result={ret.Success}, {ret.TotalTimeMS}ms, {ret.PingError}");

            return ret;
        }

        static byte[] entropy = System.Text.Encoding.Unicode.GetBytes("sdsgtj;lrjwteojtkslkdjsl;dvlbmv.bmvlfu7r0tret-rereigjejgkgljg42");

        //this is not truly secure, but better than storing plain text in the JSON file
        public static string EncryptString(string input)
        {
            byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                System.Text.Encoding.Unicode.GetBytes(input),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        public static string DecryptString(string encryptedData)
        {
            if (String.IsNullOrEmpty(encryptedData))
                return "";

            try
            {
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    entropy,
                    System.Security.Cryptography.DataProtectionScope.CurrentUser);
                return System.Text.Encoding.Unicode.GetString(decryptedData);
            }
            catch
            {
                return "";
            }
        }

        public static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }

        public static string ToInsecureString(SecureString input)
        {
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }

        public static void MoveFiles(string FromFolder, string ToFolder, string FileSpec, bool OnlyIfNewer)
        {
            //Let us pass a filename so we can be lazy
            if (Path.HasExtension(FromFolder))
                FromFolder = Path.GetDirectoryName(FromFolder);

            if (Path.HasExtension(ToFolder))
                ToFolder = Path.GetDirectoryName(ToFolder);

            List<FileInfo> files = GetFiles(FromFolder, FileSpec, SearchOption.TopDirectoryOnly);

            int cnt = 0;

            if (files.Count > 0)
            {
                if (!Directory.Exists(ToFolder))
                    Directory.CreateDirectory(ToFolder);

            }

            foreach (FileInfo fi in files)
            {
                string newfile = Path.Combine(ToFolder, fi.Name);
                try
                {
                    bool move = true;
                    FileInfo nfi = new FileInfo(newfile);
                    if (nfi.Exists)
                    {
                        if (fi.LastWriteTime < nfi.LastWriteTime)
                        {
                            //just delete the older file rather than moving it
                            move = false;
                            fi.Delete();
                        }
                    }

                    if (move)
                        fi.MoveTo(newfile);

                    cnt++;
                }
                catch (Exception ex)
                {

                    Log($"Error: Could not move {fi.FullName} to {newfile}: {Global.ExMsg(ex)}");
                }
            }

            Log($"Debug: Moved {cnt} '{FileSpec}' files from {FromFolder} to {ToFolder}.");

        }

        public static Version GetFrameworkVersion()
        {
            using (RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full"))
            {
                if (ndpKey != null)
                {
                    int value = (int)(ndpKey.GetValue("Release") ?? 0);
                    if (value >= 528040)
                        return new Version(4, 8, 0);

                    if (value >= 461808)
                        return new Version(4, 7, 2);

                    if (value >= 461308)
                        return new Version(4, 7, 1);

                    if (value >= 460798)
                        return new Version(4, 7, 0);

                    if (value >= 394802)
                        return new Version(4, 6, 2);

                    if (value >= 394254)
                        return new Version(4, 6, 1);

                    if (value >= 393295)
                        return new Version(4, 6, 0);

                    if (value >= 379893)
                        return new Version(4, 5, 2);

                    if (value >= 378675)
                        return new Version(4, 5, 1);

                    if (value >= 378389)
                        return new Version(4, 5, 0);

                    throw new NotSupportedException($"No 4.5 or later framework version detected, framework key value: {value}");
                }

                throw new NotSupportedException(@"No registry key found under 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full' to determine running framework version");
            }
        }
        public static string UpperFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public static dynamic GetSetting(string Name, object DefaultValue = null, string SubKey = "")
        {

            //regkey is built from CompanyName\ProductName\MajorVersion.MinorVersion
            Version AN = Assembly.GetExecutingAssembly().GetName().Version;
            string Cname = System.Windows.Forms.Application.CompanyName;
            string Pname = System.Windows.Forms.Application.ProductName;
            string version = AN.Major + "." + AN.Minor;
            object RetVal = DefaultValue;
            string SKey = "";
            if (!string.IsNullOrWhiteSpace(SubKey))
                SKey = "\\" + SubKey.Trim();
            try
            {
                string RKey = $"Software\\{Cname}\\{Pname}\\{version}{SKey}";

                using (RegistryKey reg = Registry.CurrentUser.OpenSubKey(RKey, false))

                    if (reg != null)
                    {
                        bool Found = false;
                        string[] Values = reg.GetValueNames();
                        foreach (string valu in Values)
                            if (valu.ToLower() == Name.ToLower())
                            {
                                Found = true;
                                RetVal = reg.GetValue(Name, DefaultValue);
                                break;
                            }
                        if (Found)
                        {
                            if (reg.GetValueKind(Name) == RegistryValueKind.MultiString)
                            {
                                if (DefaultValue is List<string>)
                                    RetVal = ((string[])RetVal).ToList();
                                else if (DefaultValue is object[])
                                    RetVal = (string[])RetVal;
                                else if (DefaultValue is string[])
                                    RetVal = (string[])RetVal;
                            }
                            else if (RetVal is string && DefaultValue is Point)
                            {
                                //{X=965,Y=399}
                                int X = GetNumberInt(Global.GetWordBetween(RetVal.ToString(), "X=", ","));
                                int Y = GetNumberInt(Global.GetWordBetween(RetVal.ToString(), "Y=", "}"));
                                RetVal = new Point(X, Y);

                            }
                            else if (RetVal is string && DefaultValue is Size)
                            {
                                //{Width=931, Height=592}
                                int Wid = GetNumberInt(Global.GetWordBetween(RetVal.ToString(), "Width=", ","));
                                int Hei = GetNumberInt(Global.GetWordBetween(RetVal.ToString(), "Height=", "}"));
                                RetVal = new Size(Wid, Hei);
                            }
                            else if (DefaultValue != null)
                                RetVal = Convert.ChangeType(RetVal, DefaultValue.GetType());
                            //Else
                            //    RetVal = Convert.ChangeType(RetVal, DefaultValue.GetType)


                        }
                    }


            }
            catch (Exception)
            {
                //Log($"Error: {Global.ExMsg(ex)}");
            }

            return RetVal;

        }

        public static int GetNumberInt(object Obj)
        {
            //gets a number from anywhere within a string
            int Ret = 0;
            if (Obj != null)
            {
                if (Obj is string)
                {
                    string o = System.Convert.ToString(Obj);
                    //If o.Trim.Length > 0 AndAlso o.Trim.Length < 10 Then
                    int outint = 0;
                    string OnlyNums = System.Convert.ToString(Regex.Match(o, "\\d+").Value);
                    if (int.TryParse(OnlyNums, out outint))
                        Ret = outint;
                    //End If
                }
                else if (Obj is int)
                    Ret = (int)Obj;
            }
            return Ret;

        }
        public static bool SaveSetting(string name, object value, string SubKey = "")
        {
            bool ret = false;
            //regkey is built from CompanyName\ProductName\MajorVersion.MinorVersion
            Version AN = Assembly.GetExecutingAssembly().GetName().Version;
            string Cname = System.Windows.Forms.Application.CompanyName;
            string Pname = System.Windows.Forms.Application.ProductName;
            string version = AN.Major + "." + AN.Minor;
            string SKey = "";
            if (!string.IsNullOrWhiteSpace(SubKey))
                SKey = "\\" + SubKey.Trim();
            try
            {
                string RKey = $"Software\\{Cname}\\{Pname}\\{version}{SKey}";
                using (RegistryKey reg = Registry.CurrentUser.CreateSubKey(RKey, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    if (reg != null)
                    {
                        if (value is List<string>)
                        {
                            List<string> strlist = (List<string>)value;
                            reg.SetValue(name, strlist.ToArray(), RegistryValueKind.MultiString);
                        }
                        else if (value is object[])
                        {
                            List<string> strlist = new List<string>();
                            object[] objects = (object[])value;
                            foreach (object obj in objects)
                                strlist.Add(obj.ToString());
                            reg.SetValue(name, strlist.ToArray(), RegistryValueKind.MultiString);
                        }
                        else
                            reg.SetValue(name, value);
                        ret = true;
                    }


            }
            catch (Exception ex)
            {
                Log($"Error: {Global.ExMsg(ex)}");
            }
            finally
            {

            }

            return ret;
        }

        public static string ReplaceCaseInsensitive(string input, string search, string replacement)
        {
            string result = Regex.Replace(
                input,
                Regex.Escape(search),
                replacement.Replace("$", "$$"),
                RegexOptions.IgnoreCase
            );
            return result;
        }


        public static String WildCardToRegular(String value)
        {
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }

        public static void SendMessage(MessageType MT, string Descript = "", object Payload = null, [CallerMemberName] string memberName = null)
        {
            if (progress == null)
            {
                //TODO: trying to catch a logging failure, should take out messagebox later
                if (!warnedlog && !Global.IsService)
                {
                    warnedlog = true;
                    string member = "unknown";
                    if (memberName != null)
                        member = memberName;

                    MessageBox.Show($"SendMessage {MT.ToString()} Debug warning: Progress event logger is null? calling function='{member}'");
                }
                return;
            }

            ClsMessage msg = new ClsMessage(MT, Descript, Payload, memberName);

            progress.Report(msg);

        }

        public static void DeleteHistoryItem(string filename, [CallerMemberName] string memberName = null)
        {
            if (progress == null)
            {
                //TODO: trying to catch a logging failure, should take out messagebox later
                if (!warnedlog && !Global.IsService)
                {
                    warnedlog = true;
                    string member = "unknown";
                    if (memberName != null)
                        member = memberName;

                    MessageBox.Show($"DeleteHistoryItem Debug warning: Progress event logger is null? calling function='{member}'");
                }
                return;
            }

            ClsMessage msg = new ClsMessage(MessageType.DeleteHistoryItem, filename, null, memberName);

            progress.Report(msg);

        }
        public static void UpdateProgressBar(string label, int CurVal = -1, int MinVal = -1, int MaxVal = -1, [CallerMemberName] string memberName = null)
        {
            if (progress == null)
            {
                //TODO: trying to catch a logging failure, should take out messagebox later
                if (!warnedlog && !Global.IsService)
                {
                    warnedlog = true;
                    string member = "unknown";
                    if (memberName != null)
                        member = memberName;

                    MessageBox.Show($"UpdateProgressBar Debug warning: Progress event logger is null? calling function='{member}'");
                }
                return;
            }

            ClsMessage msg = new ClsMessage(MessageType.UpdateProgressBar, label, null, memberName, CurVal, MinVal, MaxVal);

            progress.Report(msg);

        }

        public static void CreateHistoryItem(History hist, [CallerMemberName] string memberName = null)
        {
            if (progress == null)
            {
                //TODO: trying to catch a logging failure, should take out messagebox later
                if (!warnedlog && !Global.IsService)
                {
                    warnedlog = true;
                    string member = "unknown";
                    if (memberName != null)
                        member = memberName;

                    MessageBox.Show($"CreateHistoryItem Debug warning: Progress event logger is null? calling function='{member}'");
                }
                return;
            }

            ClsMessage msg = new ClsMessage(MessageType.CreateHistoryItem, "", hist, memberName);

            progress.Report(msg);

        }

        public static void UpdateLabel(string Message, string LabelControlName, [CallerMemberName] string memberName = null)
        {
            if (progress == null)
            {
                //TODO: trying to catch a logging failure, should take out messagebox later
                if (!warnedlog && !Global.IsService)
                {
                    warnedlog = true;
                    string member = "unknown";
                    if (memberName != null)
                        member = memberName;

                    MessageBox.Show($"UpdateLabel Debug warning: Progress event logger is null? calling function='{member}'");
                }
                return;
            }

            ClsMessage msg = new ClsMessage(MessageType.UpdateLabel, Message, LabelControlName, memberName);

            progress.Report(msg);

        }
        private static bool warnedlog = false;
        public static void LogMessage(string Message, [CallerMemberName] string memberName = null)
        {
            if (progress == null)
            {
                //TODO: trying to catch a logging failure, should take out messagebox later
                if (!warnedlog && !Global.IsService)
                {
                    warnedlog = true;
                    string member = "unknown";
                    if (memberName != null)
                        member = memberName;

                    MessageBox.Show($"Log Debug warning: Progress event logger is null? calling function='{member}'");
                }
                return;
            }

            ClsMessage msg = new ClsMessage(MessageType.LogEntry, "", null, memberName);

            //this is for logging in non-gui classes.  Reports back to real logger
            //progress needs to be subscribed to in main gui
            string mn = "";
            if (memberName != null && !string.IsNullOrEmpty(memberName))
            {
                mn = $"{memberName}>> ";
            }
            msg.Description = $"{mn}{Message}";

            SaveSetting("LastLogEntry", msg.Description);
            Global.SaveSetting("LastShutdownState", $"checkpoint: Global.Log: {DateTime.Now}");


            progress.Report(msg);

        }
        public static Regex RegEx_ValidDate = new Regex("(19|20)[0-9]{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])_[0-9][0-9]_[0-9][0-9]_[0-9][0-9]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        //[DllImport("ntdll.dll")]
        //public static extern int RtlNtStatusToDosError(int status);

        /// <summary>
        /// Flags used by <see cref="WinError.FormatMessage"/> method.
        /// </summary>
        [Flags]
        public enum FormatMessageFlags : uint
        {
            /// <summary>
            /// The function allocates a buffer large enough to hold the formatted message, and places a pointer to the
            /// allocated buffer at the address specified by <c>lpBuffer</c>. The <c>lpBuffer</c> parameter is a pointer
            /// to an <c>LPTSTR</c>. The <c>nSize</c> parameter specifies the minimum number of <c>TCHARs</c> to allocate
            /// for an output message buffer. The caller should use the <c>LocalFree</c> function to free the buffer when
            /// it is no longer needed.
            /// If the length of the formatted message exceeds 128K bytes, then <c>FormatMessage</c> will fail and a
            /// subsequent call to <c>GetLastError</c> will return <c>ERROR_MORE_DATA</c>.
            /// This value is not available for use when compiling Windows Store apps.
            /// </summary>
            FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100,

            /// <summary>
            /// Insert sequences in the message definition are to be ignored and passed through to the output buffer
            /// unchanged. This flag is useful for fetching a message for later formatting. If this flag is set, the
            /// <c>Arguments</c> parameter is ignored.
            /// </summary>
            FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200,

            /// <summary>
            /// The <c>lpSource</c> parameter is a pointer to a null-terminated string that contains a message definition.
            /// The message definition may contain insert sequences, just as the message text in a message table resource
            /// may. This flag cannot be used with <see cref="FORMAT_MESSAGE_FROM_HMODULE"/> or
            /// <see cref="FORMAT_MESSAGE_FROM_SYSTEM"/>.
            /// </summary>
            FORMAT_MESSAGE_FROM_STRING = 0x00000400,

            /// <summary>
            /// The <c>lpSource</c> parameter is a module handle containing the message-table resource(s) to search. If
            /// this <c>lpSource</c> handle is <c>null</c>, the current process's application image file will be searched.
            /// This flag cannot be used with <see cref="FORMAT_MESSAGE_FROM_STRING"/>.
            /// If the module has no message table resource, the function fails with <c>ERROR_RESOURCE_TYPE_NOT_FOUND</c>.
            /// </summary>
            FORMAT_MESSAGE_FROM_HMODULE = 0x00000800,

            /// <summary>
            /// The function should search the system message-table resource(s) for the requested message. If this flag is
            /// specified with <see cref="FORMAT_MESSAGE_FROM_HMODULE"/>, the function searches the system message table
            /// if the message is not found in the module specified by <c>lpSource</c>. This flag cannot be used with
            /// <see cref="FORMAT_MESSAGE_FROM_STRING"/>.
            /// If this flag is specified, an application can pass the result of the <c>GetLastError</c> function to
            /// retrieve the message text for a system-defined error.
            /// </summary>
            FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000,

            /// <summary>
            /// The Arguments parameter is not a <c>va_list</c> structure, but is a pointer to an array of values that
            /// represent the arguments. This flag cannot be used with 64-bit integer values. If you are using a 64-bit
            /// integer, you must use the <c>va_list</c> structure.
            /// </summary>
            FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x00002000
        }
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint FormatMessage(
                FormatMessageFlags dwFlags,
                IntPtr lpSource,
                uint dwMessageId,
                uint dwLanguageId,
                StringBuilder lpBuffer,
                uint nSize,
                IntPtr arguments);
        //[DllImport("Kernel32.dll", SetLastError = true)]
        //static extern uint FormatMessage(uint dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId, ref IntPtr lpBuffer, uint nSize, IntPtr pArguments);
        //[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        //static extern uint FormatMessage(uint dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId, StringBuilder lpBuffer, uint nSize, IntPtr Arguments);
        public static string FormatMessageFromHRESULT(int errorcode)
        {
            const int nCapacity = 1024; // max error length
                                        //const uint FORMAT_MSG_FROM_SYS = 0x01000;

            //const uint FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x00000100;
            //const uint FORMAT_MESSAGE_IGNORE_INSERTS = 0x00000200;
            //const uint FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;
            //const uint FORMAT_MESSAGE_ARGUMENT_ARRAY = 0x00002000;
            //const uint FORMAT_MESSAGE_FROM_HMODULE = 0x00000800;
            //const uint FORMAT_MESSAGE_FROM_STRING = 0x00000400;
            //const int HresultWin32Prefix = unchecked((int)0x80070000);
            StringBuilder defSb = new StringBuilder(nCapacity);

            const FormatMessageFlags Flags = FormatMessageFlags.FORMAT_MESSAGE_FROM_SYSTEM
                | FormatMessageFlags.FORMAT_MESSAGE_IGNORE_INSERTS
                | FormatMessageFlags.FORMAT_MESSAGE_ARGUMENT_ARRAY;

            var buffer = new StringBuilder(nCapacity);
            uint result = FormatMessage(Flags, IntPtr.Zero, (uint)errorcode, 0, buffer, nCapacity, IntPtr.Zero);
            string ret = "";
            if (result != 0)
            {
                ret = buffer.ToString().Trim();
            }
            return ret;
            //uint dwChars = FormatMessage(
            //    FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS | FORMAT_MESSAGE_FROM_HMODULE,
            //    IntPtr.Zero,
            //    (uint)hresult,
            //    0, // Default language
            //    ref lpMsgBuf,
            //    0,
            //    IntPtr.Zero);
            //must specify the FORMAT_MESSAGE_ARGUMENT_ARRAY flag when pass an array
            //uint length = FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM, IntPtr.Zero, (uint)code, 0, defSb, nCapacity, IntPtr.Zero);



            //string sRet = "(unknown)";
            //if (dwChars > 0)
            //{
            //    sRet = Marshal.PtrToStringAnsi(lpMsgBuf).TrimEnd(' ', '.', '\r', '\n');
            //}

            //string sDefMsg = defSb.ToString().TrimEnd(' ', '.', '\r', '\n');
            ////nothing left to do:
            //return sDefMsg;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private extern static SafeFileHandle CreateFile(string lpFileName, FileSystemRights dwDesiredAccess, FileShare dwShareMode, IntPtr securityAttrs, FileMode dwCreationDisposition, FileOptions dwFlagsAndAttributes, IntPtr hTemplateFile);

        private const int ERROR_SHARING_VIOLATION = 32;
        private const int ERROR_LOCK_VIOLATION = 33;
        public static async Task<WaitFileAccessResult> WaitForFileAccessAsync(string filename, FileSystemRights rights = FileSystemRights.Read, FileShare share = FileShare.Read, long WaitMS = 30000, int RetryDelayMS = 20)
        {
            //run the function in another thread
            return await Task.Run(() => WaitForFileAccess(filename, rights, share, WaitMS, RetryDelayMS));
        }

        public class WaitFileAccessResult
        {
            public bool Success = false;
            public long TimeMS = 0;
            public int ErrRetryCnt = 0;
            public string ResultString = "";

        }

        public static WaitFileAccessResult WaitForFileAccess(string filename, FileSystemRights rights = FileSystemRights.Read, FileShare share = FileShare.Read, long WaitMS = 30000, int RetryDelayMS = 20)
        {
            WaitFileAccessResult ret = new WaitFileAccessResult();
            try
            {
                //lets give it an initial tiny wait
                //await Task.Delay(RetryDelayMS);

                if (File.Exists(filename))
                {
                    Stopwatch SW = new Stopwatch();
                    SW.Start();

                    while ((ret.ErrRetryCnt < 2000) && (SW.ElapsedMilliseconds < WaitMS))
                    {
                        if (new FileInfo(filename).Length > 0)
                        {
                            using (SafeFileHandle fileHandle = CreateFile(filename, rights, share, IntPtr.Zero, FileMode.Open, FileOptions.None, IntPtr.Zero))
                            {

                                if (fileHandle.IsInvalid)
                                {
                                    int LastErr = Marshal.GetLastWin32Error();
                                    ret.ErrRetryCnt += 1;

                                    if (LastErr != ERROR_SHARING_VIOLATION && LastErr != ERROR_LOCK_VIOLATION)
                                    {
                                        //unexpected error, break out
                                        Log($"Error: Unexpected Win32Error waiting for access to {filename}: {LastErr}: {new Win32Exception(LastErr)}");
                                        break;
                                    }

                                }
                                else
                                {
                                    ret.Success = true;
                                    break;
                                }

                                if (!fileHandle.IsClosed)
                                {
                                    //the using statement should make sure the handle is closed but I just feel better about doing this...
                                    fileHandle.Close();
                                }

                            }

                        }

                        Thread.Sleep(RetryDelayMS);
                        //await Task.Delay(RetryDelayMS);
                    }
                    SW.Stop();

                    ret.TimeMS = SW.ElapsedMilliseconds;

                    if (!ret.Success || ret.ErrRetryCnt > 3)
                    {
                        ret.ResultString = $"Debug: lock time: {ret.TimeMS}ms, {ret.ErrRetryCnt} retries with a {RetryDelayMS}ms retry delay.";
                        Log(ret.ResultString);
                    }

                }
                else
                {
                    Log("Error: File not found: " + filename);
                }

            }
            catch (Exception ex)
            {
                ret.ResultString = $"Error: {filename}: {Global.ExMsg(ex)}";
                Log(ret.ResultString);
            }


            return ret;

        }

        public static bool IsInList(List<string> FindStrList, string SearchList, string Separators = ",;|", bool TrueIfEmpty = true)
        {
            if (TrueIfEmpty && string.IsNullOrWhiteSpace(SearchList))
                return true;  //If there is no searchlist, always return true

            return IsInList(FindStrList, Global.Split(SearchList, Separators, true, true, true));
        }
        public static bool IsInList(string FindStr, List<string> SearchList, string Separators = ",;|", bool TrueIfEmpty = true)
        {
            if (TrueIfEmpty && SearchList.Count == 0)
                return true;  //If there is no searchlist, always return true

            return IsInList(Global.Split(FindStr, Separators, true, true, true), SearchList);
        }
        public static bool IsInList(string FindStr, string SearchList, string Separators = ",;|", bool TrueIfEmpty = true)
        {
            if (TrueIfEmpty && string.IsNullOrWhiteSpace(SearchList))
                return true;  //If there is no searchlist, always return true

            return IsInList(Global.Split(FindStr, Separators, true, true, true), Global.Split(SearchList, Separators, true, true, true));
        }
        private static bool IsInList(List<string> FindStrsList, List<string> SearchList)
        {
            foreach (string findstr in FindStrsList)
            {
                foreach (string searchstr in SearchList)
                {
                    if (findstr == searchstr || searchstr == "*")
                        return true;
                }
            }
            return false;
        }

        public static List<string> Split(string InList, string Separators = "|", bool RemoveEmpty = true, bool TrimStr = true, bool ToLower = false)
        {
            List<string> Ret = new List<string>();
            if (!string.IsNullOrWhiteSpace(InList))
            {
                StringSplitOptions SSO = StringSplitOptions.None;

                if (RemoveEmpty)
                    SSO = StringSplitOptions.RemoveEmptyEntries;

                string[] splt = InList.Split(Separators.ToCharArray(), SSO);
                for (int i = 0; i < splt.Count(); i++)
                {
                    if (ToLower)
                        splt[i] = splt[i].ToLower();

                    if (RemoveEmpty && !string.IsNullOrWhiteSpace(splt[i]))
                    {
                        if (TrimStr)
                            Ret.Add(splt[i].Trim());
                        else
                            Ret.Add(splt[i]);
                    }
                    else if (!RemoveEmpty)
                    {
                        if (TrimStr)
                            Ret.Add(splt[i].Trim());
                        else
                            Ret.Add(splt[i]);
                    }

                }
            }
            return Ret;
        }

        public static string ConvertToBase64(this Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        public static string ExMsg(Exception MyEx2, [CallerMemberName] string MemberName = null)
        {
            //Gets the nested/inner exception if found, and also line and column of error - assuming PDB is in same folder
            string msg = "";
            try
            {
                string typ = "";
                Exception BaseEx = MyEx2.GetBaseException();
                Exception InnerEx = MyEx2.InnerException;
                if (InnerEx != null)
                {
                    typ = InnerEx.GetType().Name;
                    string Extra = GetExtraExceptionInfo(MyEx2);
                    msg = InnerEx.Message + " [" + typ + "] " + Extra;
                }
                else if (BaseEx != null)
                {
                    typ = BaseEx.GetType().Name;
                    string Extra = GetExtraExceptionInfo(MyEx2);
                    msg = BaseEx.Message + " [" + typ + "] " + Extra;
                }
                else
                {
                    typ = MyEx2.GetType().Name;
                    string Extra = GetExtraExceptionInfo(MyEx2);
                    msg = MyEx2.Message + " [" + typ + "] " + Extra;
                }

                //ExtraInfo = $"Mod: {LastMod} Line:{Frames(Frames.Count - 1).GetFileLineNumber}:{Frames(Frames.Count - 1).GetFileColumnNumber}"

                if (msg.IndexOf(MemberName, StringComparison.OrdinalIgnoreCase) == -1)
                {
                    msg = $"{msg} ({MemberName})";
                }

            }
            catch (Exception)
            {
            }

            msg = msg.Replace("\r\n", " | ");
            return msg;

        }

        public static string GetExtraExceptionInfo(Exception ThisEX)
        {
            string ExtraInfo = "";
            try
            {
                //Dim st As StackTrace = New StackTrace(ThisEX, True)
                if (ThisEX.StackTrace != null)
                {
                    string ST = ThisEX.StackTrace;
                    string[] SpltStr = new string[1] { " at " };
                    string[] Splt = ST.Split(SpltStr, StringSplitOptions.None);
                    //at FreeDNSUpdateTool.SharedModule.VB$StateMachine_1_GetPublicIPAddress.MoveNext() in
                    //                                  VB$StateMachine_16_GetPublicIPAddress
                    string Lst = Splt[Splt.Count() - 1].Replace(".MoveNext()", "").Trim();
                    Lst = GetWordBetween(Lst, "", " in ");
                    //FreeDNSUpdateTool.SharedModule.VB$StateMachine_1_GetPublicIPAddress
                    string[] Splt2 = Lst.Split('.');
                    string LastMod = Splt2[Splt2.Count() - 1].Trim();
                    //for (int statenum = 0; statenum <= 256; statenum++)
                    //{
                    //    LastMod = LastMod.Replace($"VB$StateMachine_{statenum}_", "");
                    //}
                    //GetPublicIPAddress
                    StackFrame[] Frames = (new StackTrace(ThisEX, true)).GetFrames();

                    //string mods = "";

                    //for (int i = 1; i < Frames.Count(); i++)
                    //{

                    //}

                    ExtraInfo = $"Mod: {LastMod} Line:{Frames[Frames.Count() - 1].GetFileLineNumber()}:{Frames[Frames.Count() - 1].GetFileColumnNumber()}";
                }
            }
            catch (Exception)
            {
            }
            return ExtraInfo;
        }

        public static void Startup(bool Enable)
        {
            try
            {

                using (RegistryKey RK = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {

                    string AppName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
                    string AppCmd = Application.ExecutablePath + " /min";
                    bool Enabled = false;
                    object CurVal = RK.GetValue(AppName, null);

                    if (CurVal == null || string.IsNullOrWhiteSpace(CurVal.ToString()))
                    {
                        Log("Application is NOT set to start with Windows: HKCU\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");

                        Enabled = false;
                    }
                    else
                    {
                        if (string.Equals(CurVal.ToString(), AppCmd, StringComparison.OrdinalIgnoreCase))
                        {
                            Log("Application is already set to start with Windows: HKCU\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                            Enabled = true;
                        }
                        else
                        {
                            Log($"Application is NOT set to start with Windows (bad path={CurVal}): HKCU\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                        }

                    }

                    if (Enable && !Enabled)
                    {
                        Log("Enabling Application startup: " + AppCmd);
                        if (!Debugger.IsAttached)
                        {
                            RK.SetValue(AppName, AppCmd);
                        }
                    }
                    else if (!Enable && Enabled)
                    {
                        Log("Disabling Application startup.");
                        if (!Debugger.IsAttached)
                        {
                            RK.DeleteValue(AppName);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
            finally
            {
            }
        }

        public static string CleanString(string inp)
        {
            if (inp == null)
            {
                return "";
            }
            else
            {
                return inp.Replace("\0", " ").Replace("\r", " ").Replace("\n", " ");
            }
        }


        public static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 32768, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }

        public static HttpContent CreateHttpContentString(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var json = JsonConvert.SerializeObject(content);
                httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            }

            return httpContent;
        }

        public static HttpContent CreateHttpContentStream(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            }

            return httpContent;
        }
        /// <summary>
        /// Writes the given object instance to a Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [JsonIgnore] attribute.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static string WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            string Ret = "";
            TextWriter writer = null;
            try
            {
                JsonSerializerSettings jset = new JsonSerializerSettings { };
                jset.TypeNameHandling = TypeNameHandling.All;
                jset.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                jset.ContractResolver = Global.JSONContractResolver;

                Ret = JsonConvert.SerializeObject(objectToWrite, Formatting.Indented, jset);
                if (jset.Error == null)
                {

                    writer = new StreamWriter(filePath, append);
                    writer.Write(Ret);
                }
                else
                {
                    Ret = "";
                    Log($"Error: While writing '{filePath}', got: " + jset.Error.ToString());
                }
            }
            catch (Exception ex)
            {
                Ret = "";
                Log($"Error: While writing '{filePath}', got: " + Global.ExMsg(ex));
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
            return Ret;

        }

        /// <summary>
        /// Reads an object instance from an Json file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the Json file.</returns>
        public static T ReadFromJsonFile<T>(string filePath) where T : new()
        {


            T Ret = default(T);

            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();

                JsonSerializerSettings jset = new JsonSerializerSettings { };
                jset.TypeNameHandling = TypeNameHandling.All;
                jset.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                jset.ContractResolver = Global.JSONContractResolver;

                Ret = JsonConvert.DeserializeObject<T>(fileContents, jset);
            }
            catch (Exception ex)
            {
                Log($"Error: While reading '{filePath}', got: " + Global.ExMsg(ex));
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return Ret;

        }
        public static bool IsClassEqual(string clsjson, object cls2)
        {

            bool Ret = false;
            try
            {

                JsonSerializerSettings jset = new JsonSerializerSettings { };
                jset.TypeNameHandling = TypeNameHandling.All;
                jset.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                jset.ContractResolver = Global.JSONContractResolver;

                string contents2 = JsonConvert.SerializeObject(cls2, Formatting.Indented, jset);
                if (jset.Error == null)
                {

                    if (clsjson == contents2)
                    {
                        Ret = true;
                    }
                }
                else
                {
                    Log($"Error: " + jset.Error.ToString());
                }

            }
            catch (Exception ex)
            {
                Log($"Error: " + Global.ExMsg(ex));
            }
            finally
            {
            }

            return Ret;

        }

        public static string GetJSONString(object cls2)
        {

            string Ret = "";
            try
            {

                JsonSerializerSettings jset = new JsonSerializerSettings { };
                jset.TypeNameHandling = TypeNameHandling.All;
                jset.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                jset.ContractResolver = Global.JSONContractResolver;

                string contents2 = JsonConvert.SerializeObject(cls2, Formatting.Indented, jset);
                if (jset.Error == null)
                {

                    Ret = contents2;
                }
                else
                {
                    Log($"Error: " + jset.Error.ToString());
                }

            }
            catch (Exception ex)
            {
                Log($"Error: " + Global.ExMsg(ex));
            }
            finally
            {
            }

            return Ret;

        }

        public static T SetJSONString<T>(string JSONString) where T : new()
        {


            T Ret = default(T);

            try
            {

                JsonSerializerSettings jset = new JsonSerializerSettings { };
                jset.TypeNameHandling = TypeNameHandling.All;
                jset.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                jset.ContractResolver = Global.JSONContractResolver;

                Ret = JsonConvert.DeserializeObject<T>(JSONString, jset);
            }
            catch (Exception ex)
            {
                Log($"Error: While converting json string '{JSONString}', got: " + Global.ExMsg(ex));
            }
            finally
            {
            }

            return Ret;

        }
        public static DateTime RetrieveLinkerTimestamp()
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);

            try
            {
                string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
                const int c_PeHeaderOffset = 60;
                const int c_LinkerTimestampOffset = 8;
                byte[] b = new byte[2048];
                using (System.IO.Stream s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {

                    try
                    {
                        //s = New System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read)
                        s.Read(b, 0, 2048);
                    }
                    finally
                    {
                        if (s != null)
                        {
                            s.Close();
                        }
                    }

                    int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
                    int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
                    dt = dt.AddSeconds(secondsSince1970);
                    dt = dt.AddHours(System.TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours);
                }

            }
            catch (System.Exception ex)
            {
                Log("Error: " + Global.ExMsg(ex));
            }

            return dt;

        }


        public static DateTime GetTimeFromFileName(string FileName)
        {
            DateTime OutDate = DateTime.MinValue;

            try
            {
                if (string.IsNullOrWhiteSpace(FileName))
                {
                    return OutDate;
                }
                string Fil = Path.GetFileNameWithoutExtension(FileName);
                string StrDate = "";
                MatchCollection Matches = RegEx_ValidDate.Matches(Fil);
                if (Matches != null && Matches.Count > 0)
                {
                    StrDate = Matches[0].Value;
                    StrDate = StrDate.Replace("_", ":");
                    //pos 10=T
                    StrDate = StrDate.Remove(10, 1).Insert(10, "T");
                    if (!GetDateStrict(StrDate, ref OutDate))
                    {
                        Log("Error: There was a problem parsing '" + FileName + "' for a date.");
                        OutDate = DateTime.MinValue;
                    }
                }

            }
            catch (Exception ex)
            {
                Log("Error: " + Global.ExMsg(ex));
            }

            return OutDate;

        }

        public class ClsDateFormat
        {
            public string Fmt = "";
            public long Cnt = 0;
            public override string ToString()
            {
                return $"Cnt='{this.Cnt}', Fmt='{this.Fmt}'";
            }
        }

        public static long DateFormatHitCnt = 0;

        public static List<ClsDateFormat> DateFormatList = new List<ClsDateFormat>();

        public static void CreateFormatList()
        {
            //28-Feb-2015 17:21:56.155
            //11/8/2012 09:28:33:941
            //15-May-2018 18:19:20.173
            //15-May-2018 18:05:28.457
            //dd-MMMM-yyyy HH:mm:ss.fff
            //7/15/2015 13:10:46:788
            //8/7/2017 13:00:15:330
            //6/14/2016 15:03:01:360
            //2018-04-10T14:32:26
            //yyyy-MM-ddTHH:mm:ss

            //most popular first
            DateFormatList.Add(new ClsDateFormat { Fmt = "dd-MMM-yyyy HH:mm:ss.fff" });
            DateFormatList.Add(new ClsDateFormat { Fmt = "yyyy-MM-dd HH:mm:ss.fff" });
            DateFormatList.Add(new ClsDateFormat { Fmt = "M/d/yyyy HH:mm:ss:fff" });
            DateFormatList.Add(new ClsDateFormat { Fmt = "dd-MMMM-yyyy HH:mm:ss.fff" });
            DateFormatList.Add(new ClsDateFormat { Fmt = "yyyy-MM-ddTHH:mm:ss" });

            DateFormatList.Add(new ClsDateFormat { Fmt = "M/d/yyyy H:mm:ss:fff" });
            DateFormatList.Add(new ClsDateFormat { Fmt = "M/dd/yyyy hh:mm:ss:fff" });
            DateFormatList.Add(new ClsDateFormat { Fmt = "M/dd/yyyy HH:mm:ss:fff" });
            DateFormatList.Add(new ClsDateFormat { Fmt = "M/dd/yyyy H:mm:ss:fff" });
            DateFormatList.Add(new ClsDateFormat { Fmt = "M/dd/yyyy hh:mm:ss:fff" });
            DateFormatList.Add(new ClsDateFormat { Fmt = "M/dd/yyyy HH:mm:ss:fff" });
            DateFormatList.Add(new ClsDateFormat { Fmt = "M/dd/yyyy H:mm:ss:fff" });
            DateFormatList.Add(new ClsDateFormat { Fmt = "MM/dd/yyyy hh:mm:ss:fff" });
            DateFormatList.Add(new ClsDateFormat { Fmt = "MM/dd/yyyy HH:mm:ss:fff" });
            DateFormatList.Add(new ClsDateFormat { Fmt = "MM/dd/yyyy H:mm:ss:fff" });
            DateFormatList.Add(new ClsDateFormat { Fmt = "d-MMMM-yyyy HH:mm:ss.fff" });
            DateFormatList.Add(new ClsDateFormat { Fmt = "d-MMM-yyyy HH:mm:ss.fff" });

        }
        public static bool GetDateStrict(string InpDate, ref DateTime OutDate, string format = "")
        {
            bool Ret = false;
            try
            {
                if (!string.IsNullOrEmpty(format))
                {
                    Ret = DateTime.TryParseExact(InpDate, format, null, System.Globalization.DateTimeStyles.None, out OutDate); //New CultureInfo("en-US")
                    if (Ret)
                        return Ret;
                }

                if (DateFormatList.Count == 0)
                {
                    CreateFormatList();
                }
                for (int i = 0; i < DateFormatList.Count; i++)
                {
                    ClsDateFormat df = DateFormatList[i];
                    Ret = DateTime.TryParseExact(InpDate, df.Fmt, null, System.Globalization.DateTimeStyles.None, out OutDate); //New CultureInfo("en-US")
                    if (Ret)
                    {
                        //First double check that date is in normal-ish range..
                        if (OutDate > new DateTime(2000, 1, 1) && OutDate <= DateTime.Now.AddHours(12))
                        {
                            df.Cnt = df.Cnt + 1;
                            DateFormatHitCnt = DateFormatHitCnt + 1;
                            //If DateFormatHitCnt < 15 OrElse (CurCnt > 1 AndAlso (DateFormatHitCnt Mod 25 = 0)) Then
                            //    'Sort the list by most frequent found cnt
                            //    DateFormatList = DateFormatList.OrderByDescending(Function(d) d.Cnt).ToList
                            //End If                        
                            break;
                        }
                        else
                        {
                            Ret = false;
                        }
                    }

                }

                if (!Ret)
                {
                    //last ditch
                    Ret = DateTime.TryParse(InpDate, out OutDate);
                    if (Ret)
                    {
                        if (OutDate > new DateTime(2010, 1, 1) && OutDate < new DateTime(2050, 1, 1))
                        {
                        }
                        else
                        {
                            Ret = false;
                            OutDate = DateTime.MinValue;
                        }
                    }
                }


            }
            catch (Exception)
            {
                Ret = false;
            }
            return Ret;
        }
        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static Process GetaProcess(string processname)
        {
            try
            {
                if (Path.HasExtension(processname))
                    processname = Path.GetFileNameWithoutExtension(processname);
                Process[] aProc = Process.GetProcessesByName(processname);
                if (aProc.Length > 0)
                    return aProc[0];
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool ProcessValid(ClsProcess prc)
        {

            if (prc != null && prc.process != null)
            {
                try
                {
                    if (!prc.process.HasExited)
                    {
                        //if (!string.IsNullOrEmpty(prc.CommandLine) || !string.IsNullOrEmpty(prc.process.StartInfo.Arguments))
                        //{
                        return true;
                        //}
                    }
                }
                catch { }
            }
            return false;
        }

        public static List<ClsProcess> GetProcessesByPath(string processname)
        {
            List<ClsProcess> Ret = new List<ClsProcess>();
            try
            {
                string pname = Path.GetFileNameWithoutExtension(processname);

                Process[] aProc = Process.GetProcessesByName(pname);

                ProcessDetail PD = null;

                if (aProc.Length > 0)

                {
                    foreach (Process curproc in aProc)
                    {
                        //accessing 64 bit process from 32 bit app may not allow to get process properties, only name
                        //Stopwatch SW = Stopwatch.StartNew();
                        ClsProcess CurPrc = new ClsProcess();

                        try
                        {
                            //if (IsAdministrator())
                            //{
                            //    Process.EnterDebugMode();
                            //}
                            PD = new ProcessDetail(curproc.Id);
                            CurPrc.FileName = PD.Win32ProcessImagePath;
                            //Todo: This is not working for some reason, even with admin rights
                            //if (IsAdministrator())
                            //{
                            //    Ret.CommandLine = PD.CommandLine;  //.Replace((char)34,"");
                            //}
                        }
                        catch
                        {
                            CurPrc = null;
                        }

                        //asdf
                        //if (string.IsNullOrEmpty(Ret.CommandLine))
                        //{
                        //    //Having trouble obtaining the command line?
                        //    //Log($"Cannot get command line for '{curproc.ProcessName}', must be running as administrator.");
                        //}

                        if (Ret != null && !string.IsNullOrEmpty(CurPrc.FileName) && CurPrc.FileName.ToLower() == processname.ToLower())
                        {
                            CurPrc.process = curproc;
                            Ret.Add(CurPrc);
                        }
                        else
                        {
                            CurPrc = null;
                        }
                    }

                }
            }
            catch
            {
                Ret = new List<ClsProcess>();
            }

            return Ret;
        }

        public static ClsProcess GetaProcessByPath(string processname)
        {
            ClsProcess Ret = null;
            try
            {
                string pname = Path.GetFileNameWithoutExtension(processname);

                Process[] aProc = Process.GetProcessesByName(pname);

                ProcessDetail PD = null;

                if (aProc.Length > 0)

                {
                    foreach (Process curproc in aProc)
                    {
                        //accessing 64 bit process from 32 bit app may not allow to get process properties, only name
                        //Stopwatch SW = Stopwatch.StartNew();
                        Ret = new ClsProcess();

                        try
                        {
                            //if (IsAdministrator())
                            //{
                            //    Process.EnterDebugMode();
                            //}
                            PD = new ProcessDetail(curproc.Id);
                            Ret.FileName = PD.Win32ProcessImagePath;
                            //Todo: This is not working for some reason, even with admin rights
                            //if (IsAdministrator())
                            //{
                            //    Ret.CommandLine = PD.CommandLine;  //.Replace((char)34,"");
                            //}
                        }
                        catch
                        {
                            Ret = null;
                        }

                        //asdf
                        //if (string.IsNullOrEmpty(Ret.CommandLine))
                        //{
                        //    //Having trouble obtaining the command line?
                        //    //Log($"Cannot get command line for '{curproc.ProcessName}', must be running as administrator.");
                        //}

                        if (Ret != null && !string.IsNullOrEmpty(Ret.FileName) && Ret.FileName.ToLower() == processname.ToLower())
                        {
                            Ret.process = curproc;
                            break;
                        }
                        else
                        {
                            Ret = null;
                        }
                    }

                }
            }
            catch
            {
                Ret = null;
            }

            return Ret;
        }

        public static bool IsProcessRunning(string ProcessName)
        {
            bool Ret = false;

            try
            {
                Process Proc = GetaProcess(ProcessName);

                if (Proc == null)
                    Log($"Process is not running: '{ProcessName}'.");
                else
                {
                    Log($"Process IS running: '{ProcessName}'.");
                    Ret = true;
                }
            }
            catch (Exception ex)
            {
                Log("Error: " + Global.ExMsg(ex));
            }

            return Ret;
        }


        public static string GetWordBetween(string InpStr, string JustBefore, string JustAfter, Int32 LastPos = 0, Int32 FirstPos = 0, bool NoTrim = false, bool MustFindJustAfter = false)
        {
            string Ret = "";

            try
            {
                string[] JB = JustBefore.Split('|');
                string[] JA = JustAfter.Split('|');
                int JBPos = 0;
                int JAPos = 0;
                string BefStr = "";
                string AftStr = "";
                int WordLen = 0;
                string RetWord = "";

                if (JustBefore.Length > 0)
                {
                    foreach (string BefStrTmp in JB)
                    {
                        BefStr = BefStrTmp;
                        if (BefStr.Length > 0)
                        {
                            JBPos = InpStr.IndexOf(BefStr, FirstPos, StringComparison.OrdinalIgnoreCase);
                            if (JBPos >= 0)
                                break;
                        }
                    }
                }
                else
                    JBPos = FirstPos;
                if (JBPos == -1)
                    return Ret;
                int FirstFnd = InpStr.Length;
                foreach (string AftStrTmp in JA)
                {
                    AftStr = AftStrTmp;
                    if (AftStr.Length > 0)
                    {
                        Int32 count = InpStr.Length - (JBPos + BefStr.Length);
                        Int32 StartIndex = JBPos + BefStr.Length;
                        JAPos = InpStr.IndexOf(AftStr, StartIndex, count, StringComparison.OrdinalIgnoreCase);
                        if (JAPos >= 0)
                            // If JAPos <= FirstFnd Then FirstFnd = JAPos
                            FirstFnd = Math.Min(JAPos, FirstFnd);
                    }
                }

                // If FirstFnd <= JAPos Then
                JAPos = FirstFnd;
                // End If

                if (JAPos == -1 || JAPos == 0 || JustAfter.Length == 0)
                {
                    if (!MustFindJustAfter)
                        JAPos = InpStr.Length;
                }

                if (JAPos <= JBPos)
                    return Ret;

                WordLen = JAPos - (JBPos + BefStr.Length);
                if (WordLen > 0)
                {
                    RetWord = InpStr.Substring(JBPos + BefStr.Length, WordLen);
                    LastPos = JAPos; // JBPos + BefStr.Length + RetWord.Length
                    if (NoTrim)
                        Ret = RetWord;
                    else
                        Ret = RetWord.Trim();
                }
            }
            // Return ""

            catch (Exception)
            {
            }
            finally
            {

            }

            return Ret;
        }

        public static string GetWMIPropertyFromProcess(Int32 PID, string PropName)
        {

            // THIS IS SLOW AS FUCK

            // .net PROCESS object cannot seem to get the command line from processes that we did not start
            // resort to using WMI
            //https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-process
            //
            //CommandLine, ExecutablePath, ProcessId
            string Ret = "";
            try
            {
                Stopwatch SW = Stopwatch.StartNew();

                //method 2 - 400ms faster:

                string wmiQuery = $"select ProcessId, CommandLine, ExecutablePath from Win32_Process where ProcessId='{Convert.ToUInt32(PID)}'";
                using (ManagementObjectSearcher search = new ManagementObjectSearcher(wmiQuery))
                {

                    // By definition, the query returns at most 1 match, because the process 
                    // is looked up by ID (which is unique by definition).
                    using (var matchEnum = search.Get().GetEnumerator())
                    {
                        if (matchEnum.MoveNext()) // Move to the 1st item.
                        {
                            object obj = matchEnum.Current[PropName];

                            if (obj == null)
                                obj = "";

                            Ret = obj.ToString();

                        }
                    }

                    //ManagementObjectCollection processList = search.Get();
                    //foreach (ManagementObject process in processList)
                    //{
                    //    //Log("{0,6} - {1} - {2}", process["ProcessId"], process["Name"], process["CommandLine"]);
                    //    object obj = process[PropName];

                    //    if (obj == null)
                    //        obj = "";

                    //    Ret = obj.ToString();

                    //    break;

                    //}
                }

                //Method 1 - 450 ms slowest
                //ManagementClass mgmtClass = new ManagementClass("Win32_Process");
                //foreach (ManagementObject process in mgmtClass.GetInstances())
                //{
                //    // Get pid
                //    UInt32 CurPID = (System.UInt32)process["ProcessId"];

                //    if (CurPID == Convert.ToUInt32(PID))
                //    {
                //        object obj = process[PropName];

                //        if (obj == null)
                //            obj = "";

                //        Ret = obj.ToString();

                //        break;
                //    }

                //}

                SW.Stop();
                Log($"Got '{PropName}' for PID '{PID}' in '{SW.ElapsedMilliseconds}'ms: {Ret}");

            }
            catch (Exception ex)
            {

                Log("Error: Could not get command line: " + Global.ExMsg(ex));
            }
            return Ret;
        }

        //I had to make this class since win32 apps cant access 64 bit command line and module info
        public class ClsProcess
        {
            public Process process = null;
            public string FileName = "";
            public string CommandLine = "";
            public ClsProcess()
            {
                this.process = new Process();
            }
        }





        public static string GetXValue(XElement XE, string Name, string AttributeName = "")
        {
            string Ret = "";
            try
            {
                if (XE.HasElements)
                {
                    if (XE.Element(Name) != null)
                    {
                        if (string.IsNullOrEmpty(AttributeName))
                        {
                            Ret = XE.Element(Name).Value;
                        }
                        else
                        {
                            if (XE.Element(Name).HasAttributes)
                            {
                                if (XE.Element(Name).Attribute(AttributeName) != null)
                                {
                                    Ret = XE.Element(Name).Attribute(AttributeName).Value;
                                }
                                else
                                {
                                    //M.DbgLog($"Warning: Attribute not found '{AttributeName}' for Element '{Name}'.");
                                }
                            }
                            else
                            {
                                //M.DbgLog($"Warning: Attribute not found '{AttributeName}' for Element '{Name}'.");
                            }
                        }
                    }
                    else
                    {
                        //M.DbgLog($"Warning: Elements not found '{Name}'.");
                    }
                }
                else
                {
                    //M.Log($"Error: No elements found While getting '{Name}'.");
                }
            }
            catch (Exception)
            {
                //M.Log($"Error: While getting '{Name}', got error '{ex.Message}'.");
            }
            return Ret;
        }

        public static List<System.IO.FileInfo> GetFiles(string CurDirectory, string FileName = "*", System.IO.SearchOption SearchOptions = System.IO.SearchOption.AllDirectories, DateTime? MinLastWriteTime = null, DateTime? MaxLastWriteTime = null)
        {

            List<System.IO.FileInfo> files = new List<System.IO.FileInfo>();
            try
            {
                //If Directory.Exists(CurDirectory) Then
                List<string> Folders = Global.Split(CurDirectory, ";|");
                List<string> Names = Global.Split(FileName, ";|");
                bool HasDate = MinLastWriteTime.HasValue;
                foreach (string fld in Folders)
                {
                    string fldr = fld;

                    //so we can be lazy and pass filenames...
                    if (Path.HasExtension(fldr))
                        fldr = Path.GetDirectoryName(fldr);

                    if (Directory.Exists(fldr))
                    {
                        DirectoryInfo DirInfo = new DirectoryInfo(fldr);
                        foreach (string nam in Names)
                        {
                            //M.DbgLog($"Getting '{nam}' files from folder '{fldr}'...");
                            try
                            {
                                foreach (FileInfo fi in DirInfo.EnumerateFiles(nam, SearchOptions))
                                {
                                    if (HasDate)
                                    {
                                        if (MaxLastWriteTime.HasValue)
                                        {
                                            if (fi.LastWriteTime >= MinLastWriteTime.Value && fi.LastWriteTime <= MaxLastWriteTime)
                                            {
                                                files.Add(fi);
                                            }

                                        }
                                        else
                                        {
                                            if (fi.LastWriteTime == MinLastWriteTime.Value)
                                            {
                                                files.Add(fi);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        files.Add(fi);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Log("Error: While getting file list, received error: " + Global.ExMsg(ex));
                            }
                        }

                    }
                    else
                    {
                        Log($"Debug: Directory doesn't exist: '{fldr}' - ({FileName})");
                    }
                }
                //files.AddRange(IO.Directory.GetFiles(CurDirectory, nam, SearchOptions).Select(Function(p) New IO.FileInfo(p)).ToList)
                //M.DbgLog("Found " & files.Count & " " & FileName & " files in " & CurDirectory)
                //Else
                //M.DbgLog("Error: Folder does not exist: " & CurDirectory)
                //End If

            }
            catch (Exception ex)
            {
                Log("Error: While getting file list, received error: " + Global.ExMsg(ex));
            }
            finally
            {
            }

            return files;

        }

    }

    internal static class Utility
    {
        public static string[] SplitArgs(string unsplitArgumentLine)
        {
            if (unsplitArgumentLine == null)
                return new string[0];

            int numberOfArgs;
            IntPtr ptrToSplitArgs;
            string[] splitArgs;

            ptrToSplitArgs = NativeMethods.CommandLineToArgvW(unsplitArgumentLine, out numberOfArgs);

            // CommandLineToArgvW returns NULL upon failure.
            if (ptrToSplitArgs == IntPtr.Zero)
                throw new ArgumentException("Unable to split argument.", new Win32Exception());

            // Make sure the memory ptrToSplitArgs to is freed, even upon failure.
            try
            {
                splitArgs = new string[numberOfArgs];

                // ptrToSplitArgs is an array of pointers to null terminated Unicode strings.
                // Copy each of these strings into our split argument array.
                for (int i = 0; i < numberOfArgs; i++)
                    splitArgs[i] = Marshal.PtrToStringUni(
                        Marshal.ReadIntPtr(ptrToSplitArgs, i * IntPtr.Size));

                return splitArgs;
            }
            finally
            {
                // Free memory obtained by CommandLineToArgW.
                NativeMethods.LocalFree(ptrToSplitArgs);
            }
        }

        public static T ReadUnmanagedStructFromProcess<T>(IntPtr processHandle,
                                                          IntPtr addressInProcess)
        {
            int bytesRead;
            int bytesToRead = Marshal.SizeOf(typeof(T));
            IntPtr buffer = Marshal.AllocHGlobal(bytesToRead);
            if (!NativeMethods.ReadProcessMemory(processHandle, addressInProcess, buffer, bytesToRead,
                    out bytesRead))
                throw new Win32Exception();
            T result = (T)Marshal.PtrToStructure(buffer, typeof(T));
            Marshal.FreeHGlobal(buffer);
            return result;
        }

        public static string ReadStringUniFromProcess(IntPtr processHandle,
                                                      IntPtr addressInProcess,
                                                      int NumChars)
        {
            int bytesRead;
            IntPtr outBuffer = Marshal.AllocHGlobal(NumChars * 2);

            bool bresult = NativeMethods.ReadProcessMemory(processHandle,
                                                           addressInProcess,
                                                           outBuffer,
                                                           NumChars * 2,
                                                           out bytesRead);
            if (!bresult)
                throw new Win32Exception();

            string result = Marshal.PtrToStringUni(outBuffer, bytesRead / 2);
            Marshal.FreeHGlobal(outBuffer);
            return result;
        }

        public static int UnmanagedStructSize<T>()
        {
            return Marshal.SizeOf(typeof(T));
        }
    }
    public static class LowLevelTypes
    {

        #region Constants and Enums
        // Represents the image format of a DLL or executable.
        public enum ImageFormat
        {
            NATIVE,
            MANAGED,
            UNKNOWN
        }

        // Flags used for opening a file handle (e.g. in a call to CreateFile), that determine the
        // requested permission level.
        [Flags]
        public enum FileAccessFlags : uint
        {
            GENERIC_WRITE = 0x40000000,
            GENERIC_READ = 0x80000000
        }

        // Value used for CreateFile to determine how to behave in the presence (or absence) of a
        // file with the requested name.  Used only for CreateFile.
        public enum FileCreationDisposition : uint
        {
            CREATE_NEW = 1,
            CREATE_ALWAYS = 2,
            OPEN_EXISTING = 3,
            OPEN_ALWAYS = 4,
            TRUNCATE_EXISTING = 5
        }

        // Flags that determine what level of sharing this application requests on the target file.
        // Used only for CreateFile.
        [Flags]
        public enum FileShareFlags : uint
        {
            EXCLUSIVE_ACCESS = 0x0,
            SHARE_READ = 0x1,
            SHARE_WRITE = 0x2,
            SHARE_DELETE = 0x4
        }

        // Flags that control caching and other behavior of the underlying file object.  Used only for
        // CreateFile.
        [Flags]
        public enum FileFlagsAndAttributes : uint
        {
            NORMAL = 0x80,
            OPEN_REPARSE_POINT = 0x200000,
            SEQUENTIAL_SCAN = 0x8000000,
            RANDOM_ACCESS = 0x10000000,
            NO_BUFFERING = 0x20000000,
            OVERLAPPED = 0x40000000
        }

        // The target architecture of a given executable image.  The various values correspond to the
        // magic numbers defined by the PE Executable Image File Format.
        // http://www.microsoft.com/whdc/system/platform/firmware/PECOFF.mspx
        public enum MachineType : ushort
        {
            UNKNOWN = 0x0,
            X64 = 0x8664,
            X86 = 0x14c,
            IA64 = 0x200
        }

        // A flag indicating the format of the path string that Windows returns from a call to
        // QueryFullProcessImageName().
        public enum ProcessQueryImageNameMode : uint
        {
            WIN32_FORMAT = 0,
            NATIVE_SYSTEM_FORMAT = 1
        }

        // Flags indicating the level of permission requested when opening a handle to an external
        // process.  Used by OpenProcess().
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            NONE = 0x0,
            ALL = 0x001F0FFF,
            VM_OPERATION = 0x00000008,
            VM_READ = 0x00000010,
            QUERY_INFORMATION = 0x00000400,
            QUERY_LIMITED_INFORMATION = 0x00001000
        }

        // Defines return value codes used by various Win32 System APIs.
        public enum NTSTATUS : int
        {
            SUCCESS = 0,
        }

        // Determines the amount of information requested (and hence the type of structure returned)
        // by a call to NtQueryInformationProcess.
        public enum PROCESSINFOCLASS : int
        {
            PROCESS_BASIC_INFORMATION = 0
        };

        [Flags]
        public enum SHGFI : uint
        {
            Icon = 0x000000100,
            DisplayName = 0x000000200,
            TypeName = 0x000000400,
            Attributes = 0x000000800,
            IconLocation = 0x000001000,
            ExeType = 0x000002000,
            SysIconIndex = 0x000004000,
            LinkOverlay = 0x000008000,
            Selected = 0x000010000,
            Attr_Specified = 0x000020000,
            LargeIcon = 0x000000000,
            SmallIcon = 0x000000001,
            OpenIcon = 0x000000002,
            ShellIconSize = 0x000000004,
            PIDL = 0x000000008,
            UseFileAttributes = 0x000000010,
            AddOverlays = 0x000000020,
            OverlayIndex = 0x000000040,
        }
        #endregion

        #region Structures
        // In general, for all structures below which contains a pointer (represented here by IntPtr),
        // the pointers refer to memory in the address space of the process from which the original
        // structure was read.  While this seems obvious, it means we cannot provide an elegant
        // interface to the various fields in the structure due to the de-reference requiring a
        // handle to the target process.  Instead, that functionality needs to be provided at a
        // higher level.
        //
        // Additionally, since we usually explicitly define the fields that we're interested in along
        // with their respective offsets, we frequently specify the exact size of the native structure.

        // Win32 UNICODE_STRING structure.
        [StructLayout(LayoutKind.Sequential)]
        public struct UNICODE_STRING
        {
            // The length in bytes of the string pointed to by buffer, not including the null-terminator.
            private ushort length;
            // The total allocated size in memory pointed to by buffer.
            private ushort maximumLength;
            // A pointer to the buffer containing the string data.
            private IntPtr buffer;

            public ushort Length { get { return this.length; } }
            public ushort MaximumLength { get { return this.maximumLength; } }
            public IntPtr Buffer { get { return this.buffer; } }
        }

        // Win32 RTL_USER_PROCESS_PARAMETERS structure.
        [StructLayout(LayoutKind.Explicit, Size = 72)]
        public struct RTL_USER_PROCESS_PARAMETERS
        {
            [FieldOffset(56)]
            private UNICODE_STRING imagePathName;
            [FieldOffset(64)]
            private UNICODE_STRING commandLine;

            public UNICODE_STRING ImagePathName { get { return this.imagePathName; } }
            public UNICODE_STRING CommandLine { get { return this.commandLine; } }
        };

        // Win32 PEB structure.  Represents the process environment block of a process.
        [StructLayout(LayoutKind.Explicit, Size = 472)]
        public struct PEB
        {
            [FieldOffset(2), MarshalAs(UnmanagedType.U1)]
            private bool isBeingDebugged;
            [FieldOffset(12)]
            private IntPtr ldr;
            [FieldOffset(16)]
            private IntPtr processParameters;
            [FieldOffset(468)]
            private uint sessionId;

            public bool IsBeingDebugged { get { return this.isBeingDebugged; } }
            public IntPtr Ldr { get { return this.ldr; } }
            public IntPtr ProcessParameters { get { return this.processParameters; } }
            public uint SessionId { get { return this.sessionId; } }
        };

        // Win32 PROCESS_BASIC_INFORMATION.  Contains a pointer to the PEB, and various other
        // information about a process.
        [StructLayout(LayoutKind.Explicit, Size = 24)]
        public struct PROCESS_BASIC_INFORMATION
        {
            [FieldOffset(4)]
            private IntPtr pebBaseAddress;
            [FieldOffset(16)]
            private UIntPtr uniqueProcessId;

            public IntPtr PebBaseAddress { get { return this.pebBaseAddress; } }
            public UIntPtr UniqueProcessId { get { return this.uniqueProcessId; } }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHFILEINFO
        {
            // C# doesn't support overriding the default constructor of value types, so we need to use
            // a dummy constructor.
            public SHFILEINFO(bool dummy)
            {
                this.hIcon = IntPtr.Zero;
                this.iIcon = 0;
                this.dwAttributes = 0;
                this.szDisplayName = "";
                this.szTypeName = "";
            }
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };
        #endregion
    }
    public static class NativeMethods
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReadProcessMemory(IntPtr hProcess,
                                                    IntPtr lpBaseAddress,
                                                    IntPtr lpBuffer,
                                                    int dwSize,
                                                    out int lpNumberOfBytesRead);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern LowLevelTypes.NTSTATUS NtQueryInformationProcess(
            IntPtr hProcess,
            LowLevelTypes.PROCESSINFOCLASS pic,
            ref LowLevelTypes.PROCESS_BASIC_INFORMATION pbi,
            int cb,
            out int pSize);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern IntPtr CommandLineToArgvW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine,
            out int pNumArgs);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LocalFree(IntPtr hMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(
            LowLevelTypes.ProcessAccessFlags dwDesiredAccess,
            [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
            int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode)]
        public static extern uint QueryFullProcessImageName(
            IntPtr hProcess,
            [MarshalAs(UnmanagedType.U4)] LowLevelTypes.ProcessQueryImageNameMode flags,
            [Out] StringBuilder lpImageName, ref int size);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern SafeFileHandle CreateFile(string lpFileName,
                                                       LowLevelTypes.FileAccessFlags dwDesiredAccess,
                                                       LowLevelTypes.FileShareFlags dwShareMode,
                                                       IntPtr lpSecurityAttributes,
                                                       LowLevelTypes.FileCreationDisposition dwDisp,
                                                       LowLevelTypes.FileFlagsAndAttributes dwFlags,
                                                       IntPtr hTemplateFile);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SHGetFileInfo(string pszPath,
                                                  uint dwFileAttributes,
                                                  ref LowLevelTypes.SHFILEINFO psfi,
                                                  uint cbFileInfo,
                                                  uint uFlags);
    }
    internal class ProcessDetail : IDisposable
    {


        public ProcessDetail(int pid)
        {
            // Initialize everything to null in case something fails.
            this.processId = pid;
            this.processHandleFlags = LowLevelTypes.ProcessAccessFlags.NONE;
            this.cachedProcessBasicInfo = null;
            this.machineTypeIsLoaded = false;
            this.machineType = LowLevelTypes.MachineType.UNKNOWN;
            this.cachedPeb = null;
            this.cachedProcessParams = null;
            this.cachedCommandLine = null;
            this.processHandle = IntPtr.Zero;

            this.OpenAndCacheProcessHandle();
        }

        // Returns the machine type (x86, x64, etc) of this process.  Uses lazy evaluation and caches
        // the result.
        public LowLevelTypes.MachineType MachineType
        {
            get
            {
                if (this.machineTypeIsLoaded)
                    return this.machineType;
                if (!this.CanQueryProcessInformation)
                    return LowLevelTypes.MachineType.UNKNOWN;

                this.CacheMachineType();
                return this.machineType;
            }
        }

        public string NativeProcessImagePath
        {
            get
            {
                if (this.nativeProcessImagePath == null)
                {
                    this.nativeProcessImagePath = this.QueryProcessImageName(
                        LowLevelTypes.ProcessQueryImageNameMode.NATIVE_SYSTEM_FORMAT);
                }
                return this.nativeProcessImagePath;
            }
        }

        public string Win32ProcessImagePath
        {
            get
            {
                if (this.win32ProcessImagePath == null)
                {
                    this.win32ProcessImagePath = this.QueryProcessImageName(
                        LowLevelTypes.ProcessQueryImageNameMode.WIN32_FORMAT);
                }
                return this.win32ProcessImagePath;
            }
        }

        //public Icon SmallIcon
        //{
        //    get
        //    {
        //        LowLevelTypes.SHFILEINFO info = new LowLevelTypes.SHFILEINFO(true);
        //        LowLevelTypes.SHGFI flags = LowLevelTypes.SHGFI.Icon
        //                                             | LowLevelTypes.SHGFI.SmallIcon
        //                                             | LowLevelTypes.SHGFI.OpenIcon
        //                                             | LowLevelTypes.SHGFI.UseFileAttributes;
        //        int cbFileInfo = Marshal.SizeOf(info);
        //        NativeMethods.SHGetFileInfo(Win32ProcessImagePath,
        //                                             256,
        //                                             ref info,
        //                                             (uint)cbFileInfo,
        //                                             (uint)flags);
        //        return Icon.FromHandle(info.hIcon);
        //    }
        //}

        // Returns the command line that this process was launched with.  Uses lazy evaluation and
        // caches the result.  Reads the command line from the PEB of the running process.
        public string CommandLine
        {
            get
            {
                if (!this.CanReadPeb)
                    return ""; //throw new InvalidOperationException();
                this.CacheProcessInformation();
                this.CachePeb();
                this.CacheProcessParams();
                this.CacheCommandLine();
                return this.cachedCommandLine;
            }
        }

        // Determines if we have permission to read the process's PEB.
        public bool CanReadPeb
        {
            get
            {
                LowLevelTypes.ProcessAccessFlags required_flags =
                    LowLevelTypes.ProcessAccessFlags.VM_READ
                  | LowLevelTypes.ProcessAccessFlags.QUERY_INFORMATION;

                // In order to read the PEB, we must have *both* of these flags.
                if ((this.processHandleFlags & required_flags) != required_flags)
                    return false;

                // If we're on a 64-bit OS, in a 32-bit process, and the target process is not 32-bit,
                // we can't read its PEB.
                if (Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess
                    && (this.MachineType != LowLevelTypes.MachineType.X86))
                    return false;

                return true;
            }
        }

        // If we can't read the process's PEB, we may still be able to get other kinds of information
        // from the process.  This flag determines if we can get lesser information.
        private bool CanQueryProcessInformation
        {
            get
            {
                LowLevelTypes.ProcessAccessFlags required_flags =
                    LowLevelTypes.ProcessAccessFlags.QUERY_LIMITED_INFORMATION
                  | LowLevelTypes.ProcessAccessFlags.QUERY_INFORMATION;

                // In order to query the process, we need *either* of these flags.
                return (this.processHandleFlags & required_flags) != LowLevelTypes.ProcessAccessFlags.NONE;
            }
        }

        private string QueryProcessImageName(LowLevelTypes.ProcessQueryImageNameMode mode)
        {
            StringBuilder moduleBuffer = new StringBuilder(1024);
            int size = moduleBuffer.Capacity;
            NativeMethods.QueryFullProcessImageName(
                this.processHandle,
                mode,
                moduleBuffer,
                ref size);
            if (mode == LowLevelTypes.ProcessQueryImageNameMode.NATIVE_SYSTEM_FORMAT)
                moduleBuffer.Insert(0, "\\\\?\\GLOBALROOT");
            return moduleBuffer.ToString();
        }

        // Loads the top-level structure of the process's information block and caches it.
        private void CacheProcessInformation()
        {
            System.Diagnostics.Debug.Assert(this.CanReadPeb);

            // Fetch the process info and set the fields.
            LowLevelTypes.PROCESS_BASIC_INFORMATION temp = new LowLevelTypes.PROCESS_BASIC_INFORMATION();
            int size;
            LowLevelTypes.NTSTATUS status = NativeMethods.NtQueryInformationProcess(
                this.processHandle,
                LowLevelTypes.PROCESSINFOCLASS.PROCESS_BASIC_INFORMATION,
                ref temp,
                Utility.UnmanagedStructSize<LowLevelTypes.PROCESS_BASIC_INFORMATION>(),
                out size);

            if (status != LowLevelTypes.NTSTATUS.SUCCESS)
            {
                //-1073741820 = STATUS_INFO_LENGTH_MISMATCH  - Probably a 64 bit process accessing 32 bit process memory related error
                throw new Win32Exception(Convert.ToInt32(status));
            }

            this.cachedProcessBasicInfo = temp;
        }

        // Follows a pointer from the PROCESS_BASIC_INFORMATION structure in the target process's
        // address space to read the PEB.
        private void CachePeb()
        {
            System.Diagnostics.Debug.Assert(this.CanReadPeb);

            if (this.cachedPeb == null)
            {
                this.cachedPeb = Utility.ReadUnmanagedStructFromProcess<LowLevelTypes.PEB>(
                    this.processHandle,
                    this.cachedProcessBasicInfo.Value.PebBaseAddress);
            }
        }

        // Follows a pointer from the PEB structure in the target process's address space to read the
        // RTL_USER_PROCESS_PARAMETERS structure.
        private void CacheProcessParams()
        {
            System.Diagnostics.Debug.Assert(this.CanReadPeb);

            if (this.cachedProcessParams == null)
            {
                this.cachedProcessParams =
                    Utility.ReadUnmanagedStructFromProcess<LowLevelTypes.RTL_USER_PROCESS_PARAMETERS>(
                        this.processHandle, this.cachedPeb.Value.ProcessParameters);
            }
        }

        private void CacheCommandLine()
        {
            System.Diagnostics.Debug.Assert(this.CanReadPeb);

            if (this.cachedCommandLine == null)
            {
                this.cachedCommandLine = Utility.ReadStringUniFromProcess(
                    this.processHandle,
                    this.cachedProcessParams.Value.CommandLine.Buffer,
                    this.cachedProcessParams.Value.CommandLine.Length / 2);
            }
        }

        private void CacheMachineType()
        {
            System.Diagnostics.Debug.Assert(this.CanQueryProcessInformation);

            // If our extension is running in a 32-bit process (which it is), then attempts to access
            // files in C:\windows\system (and a few other files) will redirect to C:\Windows\SysWOW64
            // and we will mistakenly think that the image file is a 32-bit image.  The way around this
            // is to use a native system format path, of the form:
            //    \\?\GLOBALROOT\Device\HarddiskVolume0\Windows\System\foo.dat
            // NativeProcessImagePath gives us the full process image path in the desired format.
            string path = this.NativeProcessImagePath;

            // Open the PE File as a binary file, and parse just enough information to determine the
            // machine type.
            //http://www.microsoft.com/whdc/system/platform/firmware/PECOFF.mspx
            using (SafeFileHandle safeHandle = NativeMethods.CreateFile(
                       path,
                       LowLevelTypes.FileAccessFlags.GENERIC_READ,
                       LowLevelTypes.FileShareFlags.SHARE_READ,
                       IntPtr.Zero,
                       LowLevelTypes.FileCreationDisposition.OPEN_EXISTING,
                       LowLevelTypes.FileFlagsAndAttributes.NORMAL,
                       IntPtr.Zero))
            {
                FileStream fs = new FileStream(safeHandle, FileAccess.Read);
                using (BinaryReader br = new BinaryReader(fs))
                {
                    fs.Seek(0x3c, SeekOrigin.Begin);
                    Int32 peOffset = br.ReadInt32();
                    fs.Seek(peOffset, SeekOrigin.Begin);
                    UInt32 peHead = br.ReadUInt32();
                    if (peHead != 0x00004550) // "PE\0\0", little-endian
                        throw new Exception("Can't find PE header");
                    this.machineType = (LowLevelTypes.MachineType)br.ReadUInt16();
                    this.machineTypeIsLoaded = true;
                }
            }
        }

        private void OpenAndCacheProcessHandle()
        {
            // Try to open a handle to the process with the highest level of privilege, but if we can't
            // do that then fallback to requesting access with a lower privilege level.
            this.processHandleFlags = LowLevelTypes.ProcessAccessFlags.QUERY_INFORMATION
                               | LowLevelTypes.ProcessAccessFlags.VM_READ;
            this.processHandle = NativeMethods.OpenProcess(this.processHandleFlags, false, this.processId);
            if (this.processHandle == IntPtr.Zero)
            {
                this.processHandleFlags = LowLevelTypes.ProcessAccessFlags.QUERY_LIMITED_INFORMATION;
                this.processHandle = NativeMethods.OpenProcess(this.processHandleFlags, false, this.processId);
                if (this.processHandle == IntPtr.Zero)
                {
                    this.processHandleFlags = LowLevelTypes.ProcessAccessFlags.NONE;
                    throw new Win32Exception();
                }
            }
        }

        // An open handle to the process, along with the set of access flags that the handle was
        // open with.
        private int processId;
        private IntPtr processHandle;
        private LowLevelTypes.ProcessAccessFlags processHandleFlags;
        private string nativeProcessImagePath;
        private string win32ProcessImagePath;

        // The machine type is read by parsing the PE image file of the running process, so we cache
        // its value since the operation expensive.
        private bool machineTypeIsLoaded;
        private LowLevelTypes.MachineType machineType;

        // The following fields exist ultimately so that we can access the command line.  However,
        // each field must be read separately through a pointer into another process's address
        // space so the access is expensive, hence we cache the values.
        private Nullable<LowLevelTypes.PROCESS_BASIC_INFORMATION> cachedProcessBasicInfo;
        private Nullable<LowLevelTypes.PEB> cachedPeb;
        private Nullable<LowLevelTypes.RTL_USER_PROCESS_PARAMETERS> cachedProcessParams;
        private string cachedCommandLine;

        ~ProcessDetail()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (this.processHandle != IntPtr.Zero)
                NativeMethods.CloseHandle(this.processHandle);
            this.processHandle = IntPtr.Zero;
        }
    }


    public static class StringExt
    {
        public static string Truncate(this string value, int maxLength, bool ellipsis)
        {
            if (string.IsNullOrEmpty(value)) return value;

            if (value.Length <= maxLength) return value;

            if (ellipsis) return value.Substring(0, maxLength) + "...";

            return value.Substring(0, maxLength);

        }
    }

}

