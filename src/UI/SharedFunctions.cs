using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32.SafeHandles;

namespace WindowsFormsApp2
{
    public static class SharedFunctions
    {

        public static Regex RegEx_ValidDate = new Regex("(19|20)[0-9]{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])_[0-9][0-9]_[0-9][0-9]_[0-9][0-9]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private extern static SafeFileHandle CreateFile(string lpFileName, FileSystemRights dwDesiredAccess, FileShare dwShareMode, IntPtr securityAttrs, FileMode dwCreationDisposition, FileOptions dwFlagsAndAttributes, IntPtr hTemplateFile);

        private const int ERROR_SHARING_VIOLATION = 32;
        private const int ERROR_LOCK_VIOLATION = 33;
        public static async Task<bool> WaitForFileAccessAsync(string filename)
        {
            //run the function in another thread
            return await Task.Run(() => WaitForFileAccess(filename));
        }

        private static async Task<bool> WaitForFileAccess(string filename)
        {
            bool Success = false;

            try
            {
                if (File.Exists(filename))
                {
                    int errs = 0;
                    Stopwatch SW = new Stopwatch();
                    SW.Start();

                    while ((errs < 2000) && (SW.ElapsedMilliseconds < 30000))
                    {

                        using (SafeFileHandle fileHandle = CreateFile(filename, FileSystemRights.Read, FileShare.Read, IntPtr.Zero, FileMode.Open, FileOptions.None, IntPtr.Zero))
                        {

                            if (fileHandle.IsInvalid)
                            {
                                int LastErr = Marshal.GetLastWin32Error();
                                errs += 1;

                                if (LastErr != ERROR_SHARING_VIOLATION && LastErr != ERROR_LOCK_VIOLATION)
                                {
                                    //unexpected error, break out
                                    Console.WriteLine("Unexpected Win32Error waiting for access: " + new Win32Exception(LastErr));
                                    break;
                                }

                            }
                            else
                            {
                                Success = true;
                                break;
                            }

                            if (!fileHandle.IsClosed)
                            {
                                fileHandle.Close();
                            }

                            await Task.Delay(10);

                        }
                    }
                    SW.Stop();

                    if (errs > 0)
                    {
                        Console.WriteLine("WaitForFileAccess lock time: " + SW.ElapsedMilliseconds + "ms, errcount=" + errs);
                    }

                }
                else
                {
                    Console.WriteLine("Error: File not found: " + filename);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("WaitForFileAccess Error: " + ex.Message);
            }


            return Success;

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
                Console.WriteLine("Error: " + ex.Message);
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
                        Console.WriteLine("Error: There was a problem parsing '" + FileName + "' for a date.");
                        OutDate = DateTime.MinValue;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return OutDate;

        }

        public class ClsDateFormat
        {
            public string Fmt = "";
            public long Cnt = 0;
            public override string ToString()
            {
                return $"Cnt='{Cnt}', Fmt='{Fmt}'";
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
        public static bool GetDateStrict(string InpDate, ref DateTime OutDate)
        {
            bool Ret = false;
            try
            {
                if (DateFormatList.Count == 0)
                {
                    CreateFormatList();
                }
                int CurCnt = 0;
                foreach (ClsDateFormat df in DateFormatList)
                {
                    CurCnt = CurCnt + 1;
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
            catch (Exception ex)
            {
                Ret = false;
            }
            return Ret;
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
            catch (Exception ex)
            {
                return null;
            }
        }

        public static ClsProcess GetaProcessByFullPath(string processname)
        {
            ClsProcess Ret = new ClsProcess();
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

                        try
                        {
                            PD = new ProcessDetail(curproc.Id);
                            Ret.FileName = PD.Win32ProcessImagePath;
                            Ret.CommandLine = PD.CommandLine;  //.Replace((char)34,"");
                            if (string.IsNullOrEmpty(Ret.CommandLine))
                            {
                                Console.WriteLine($"Error: Cannot get command line for {curproc.ProcessName}- Either permissions or due to 32 bit app trying to access 64 bit process.");
                            }
                        }
                        catch { }

                        //SW.Stop();
                        //Console.WriteLine($"Time to get command line: {SW.ElapsedMilliseconds}ms");

                        if (string.IsNullOrEmpty(Ret.FileName) || Ret.FileName.ToLower() == processname.ToLower())
                        {
                            Ret.process = curproc;
                            break;
                        }
                    }

                }
            }
            catch {}

            return Ret;
        }

        public static bool IsProcessRunning(string ProcessName)
        {
            bool Ret = false;

            try
            {
                Process Proc = GetaProcess(ProcessName);

                if (Proc == null)
                    Console.WriteLine($"Process is not running: '{ProcessName}'.");
                else
                {
                    Console.WriteLine($"Process IS running: '{ProcessName}'.");
                    Ret = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
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

            catch (Exception ex)
            {
            }
            finally
            {

            }

            return Ret;
        }

        public static string GetWMIPropertyFromProcess(Int32 PID, string PropName)
        {
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
                    //    //Console.WriteLine("{0,6} - {1} - {2}", process["ProcessId"], process["Name"], process["CommandLine"]);
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
                Console.WriteLine($"Got '{PropName}' for PID '{PID}' in '{SW.ElapsedMilliseconds}'ms: {Ret}");

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: Could not get command line: " + ex.Message);
            }
            return Ret;
        }

        //I had to make this class since win32 apps cant access 64 bit command line and module info
        public class ClsProcess
        {
            public Process process = null;
            public string FileName = "";
            public string CommandLine = "";
        }

        
       

        public static void InvokeIFRequired(Control control, MethodInvoker action)
        {
            // This will let you update any control from another thread - It only invokes IF NEEDED for better performance 
            // See TextBoxLogger.Log for example

            if (control != null)
            {
                if (control.InvokeRequired)
                {
                    control.Invoke(action);
                }
                else
                {
                    action();
                }

            }
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
        public enum FileAccessFlags:uint
        {
            GENERIC_WRITE = 0x40000000,
            GENERIC_READ = 0x80000000
        }

        // Value used for CreateFile to determine how to behave in the presence (or absence) of a
        // file with the requested name.  Used only for CreateFile.
        public enum FileCreationDisposition:uint
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
        public enum FileShareFlags:uint
        {
            EXCLUSIVE_ACCESS = 0x0,
            SHARE_READ = 0x1,
            SHARE_WRITE = 0x2,
            SHARE_DELETE = 0x4
        }

        // Flags that control caching and other behavior of the underlying file object.  Used only for
        // CreateFile.
        [Flags]
        public enum FileFlagsAndAttributes:uint
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
        public enum MachineType:ushort
        {
            UNKNOWN = 0x0,
            X64 = 0x8664,
            X86 = 0x14c,
            IA64 = 0x200
        }

        // A flag indicating the format of the path string that Windows returns from a call to
        // QueryFullProcessImageName().
        public enum ProcessQueryImageNameMode:uint
        {
            WIN32_FORMAT = 0,
            NATIVE_SYSTEM_FORMAT = 1
        }

        // Flags indicating the level of permission requested when opening a handle to an external
        // process.  Used by OpenProcess().
        [Flags]
        public enum ProcessAccessFlags:uint
        {
            NONE = 0x0,
            ALL = 0x001F0FFF,
            VM_OPERATION = 0x00000008,
            VM_READ = 0x00000010,
            QUERY_INFORMATION = 0x00000400,
            QUERY_LIMITED_INFORMATION = 0x00001000
        }

        // Defines return value codes used by various Win32 System APIs.
        public enum NTSTATUS:int
        {
            SUCCESS = 0,
        }

        // Determines the amount of information requested (and hence the type of structure returned)
        // by a call to NtQueryInformationProcess.
        public enum PROCESSINFOCLASS:int
        {
            PROCESS_BASIC_INFORMATION = 0
        };

        [Flags]
        public enum SHGFI:uint
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

            public ushort Length { get { return length; } }
            public ushort MaximumLength { get { return maximumLength; } }
            public IntPtr Buffer { get { return buffer; } }
        }

        // Win32 RTL_USER_PROCESS_PARAMETERS structure.
        [StructLayout(LayoutKind.Explicit, Size = 72)]
        public struct RTL_USER_PROCESS_PARAMETERS
        {
            [FieldOffset(56)]
            private UNICODE_STRING imagePathName;
            [FieldOffset(64)]
            private UNICODE_STRING commandLine;

            public UNICODE_STRING ImagePathName { get { return imagePathName; } }
            public UNICODE_STRING CommandLine { get { return commandLine; } }
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

            public bool IsBeingDebugged { get { return isBeingDebugged; } }
            public IntPtr Ldr { get { return ldr; } }
            public IntPtr ProcessParameters { get { return processParameters; } }
            public uint SessionId { get { return sessionId; } }
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

            public IntPtr PebBaseAddress { get { return pebBaseAddress; } }
            public UIntPtr UniqueProcessId { get { return uniqueProcessId; } }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHFILEINFO
        {
            // C# doesn't support overriding the default constructor of value types, so we need to use
            // a dummy constructor.
            public SHFILEINFO(bool dummy)
            {
                hIcon = IntPtr.Zero;
                iIcon = 0;
                dwAttributes = 0;
                szDisplayName = "";
                szTypeName = "";
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
    internal class ProcessDetail:IDisposable
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

            OpenAndCacheProcessHandle();
        }

        // Returns the machine type (x86, x64, etc) of this process.  Uses lazy evaluation and caches
        // the result.
        public LowLevelTypes.MachineType MachineType
        {
            get
            {
                if (machineTypeIsLoaded)
                    return machineType;
                if (!CanQueryProcessInformation)
                    return LowLevelTypes.MachineType.UNKNOWN;

                CacheMachineType();
                return machineType;
            }
        }

        public string NativeProcessImagePath
        {
            get
            {
                if (nativeProcessImagePath == null)
                {
                    nativeProcessImagePath = QueryProcessImageName(
                        LowLevelTypes.ProcessQueryImageNameMode.NATIVE_SYSTEM_FORMAT);
                }
                return nativeProcessImagePath;
            }
        }

        public string Win32ProcessImagePath
        {
            get
            {
                if (win32ProcessImagePath == null)
                {
                    win32ProcessImagePath = QueryProcessImageName(
                        LowLevelTypes.ProcessQueryImageNameMode.WIN32_FORMAT);
                }
                return win32ProcessImagePath;
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
                if (!CanReadPeb)
                    return ""; //throw new InvalidOperationException();
                CacheProcessInformation();
                CachePeb();
                CacheProcessParams();
                CacheCommandLine();
                return cachedCommandLine;
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
                if ((processHandleFlags & required_flags) != required_flags)
                    return false;

                // If we're on a 64-bit OS, in a 32-bit process, and the target process is not 32-bit,
                // we can't read its PEB.
                if (Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess
                    && (MachineType != LowLevelTypes.MachineType.X86))
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
                return (processHandleFlags & required_flags) != LowLevelTypes.ProcessAccessFlags.NONE;
            }
        }

        private string QueryProcessImageName(LowLevelTypes.ProcessQueryImageNameMode mode)
        {
            StringBuilder moduleBuffer = new StringBuilder(1024);
            int size = moduleBuffer.Capacity;
            NativeMethods.QueryFullProcessImageName(
                processHandle,
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
            System.Diagnostics.Debug.Assert(CanReadPeb);

            // Fetch the process info and set the fields.
            LowLevelTypes.PROCESS_BASIC_INFORMATION temp = new LowLevelTypes.PROCESS_BASIC_INFORMATION();
            int size;
            LowLevelTypes.NTSTATUS status = NativeMethods.NtQueryInformationProcess(
                processHandle,
                LowLevelTypes.PROCESSINFOCLASS.PROCESS_BASIC_INFORMATION,
                ref temp,
                Utility.UnmanagedStructSize<LowLevelTypes.PROCESS_BASIC_INFORMATION>(),
                out size);

            if (status != LowLevelTypes.NTSTATUS.SUCCESS)
            {
                throw new Win32Exception();
            }

            cachedProcessBasicInfo = temp;
        }

        // Follows a pointer from the PROCESS_BASIC_INFORMATION structure in the target process's
        // address space to read the PEB.
        private void CachePeb()
        {
            System.Diagnostics.Debug.Assert(CanReadPeb);

            if (cachedPeb == null)
            {
                cachedPeb = Utility.ReadUnmanagedStructFromProcess<LowLevelTypes.PEB>(
                    processHandle,
                    cachedProcessBasicInfo.Value.PebBaseAddress);
            }
        }

        // Follows a pointer from the PEB structure in the target process's address space to read the
        // RTL_USER_PROCESS_PARAMETERS structure.
        private void CacheProcessParams()
        {
            System.Diagnostics.Debug.Assert(CanReadPeb);

            if (cachedProcessParams == null)
            {
                cachedProcessParams =
                    Utility.ReadUnmanagedStructFromProcess<LowLevelTypes.RTL_USER_PROCESS_PARAMETERS>(
                        processHandle, cachedPeb.Value.ProcessParameters);
            }
        }

        private void CacheCommandLine()
        {
            System.Diagnostics.Debug.Assert(CanReadPeb);

            if (cachedCommandLine == null)
            {
                cachedCommandLine = Utility.ReadStringUniFromProcess(
                    processHandle,
                    cachedProcessParams.Value.CommandLine.Buffer,
                    cachedProcessParams.Value.CommandLine.Length / 2);
            }
        }

        private void CacheMachineType()
        {
            System.Diagnostics.Debug.Assert(CanQueryProcessInformation);

            // If our extension is running in a 32-bit process (which it is), then attempts to access
            // files in C:\windows\system (and a few other files) will redirect to C:\Windows\SysWOW64
            // and we will mistakenly think that the image file is a 32-bit image.  The way around this
            // is to use a native system format path, of the form:
            //    \\?\GLOBALROOT\Device\HarddiskVolume0\Windows\System\foo.dat
            // NativeProcessImagePath gives us the full process image path in the desired format.
            string path = NativeProcessImagePath;

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
                    machineType = (LowLevelTypes.MachineType)br.ReadUInt16();
                    machineTypeIsLoaded = true;
                }
            }
        }

        private void OpenAndCacheProcessHandle()
        {
            // Try to open a handle to the process with the highest level of privilege, but if we can't
            // do that then fallback to requesting access with a lower privilege level.
            processHandleFlags = LowLevelTypes.ProcessAccessFlags.QUERY_INFORMATION
                               | LowLevelTypes.ProcessAccessFlags.VM_READ;
            processHandle = NativeMethods.OpenProcess(processHandleFlags, false, processId);
            if (processHandle == IntPtr.Zero)
            {
                processHandleFlags = LowLevelTypes.ProcessAccessFlags.QUERY_LIMITED_INFORMATION;
                processHandle = NativeMethods.OpenProcess(processHandleFlags, false, processId);
                if (processHandle == IntPtr.Zero)
                {
                    processHandleFlags = LowLevelTypes.ProcessAccessFlags.NONE;
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
            Dispose();
        }

        public void Dispose()
        {
            if (processHandle != IntPtr.Zero)
                NativeMethods.CloseHandle(processHandle);
            processHandle = IntPtr.Zero;
        }
    }
}
