using OSVersionExt.Win32API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace OSVersionExt
{
    /// <summary>
    /// Win32 API Provider
    /// </summary>
    /// <remarks>CLR wrapper https://github.com/microsoft/referencesource/blob/master/mscorlib/microsoft/win32/win32native.cs </remarks>
    public class Win32ApiProvider : IWin32API
    {
        private const String NTDLL = "ntdll.dll";
        private const String USER32 = "user32.dll";

        [SecurityCritical]
        [DllImport(NTDLL, EntryPoint = "RtlGetVersion", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern NTSTATUS ntdll_RtlGetVersion(ref OSVERSIONINFOEX versionInfo);

        [DllImport(USER32, EntryPoint = "GetSystemMetrics")]
        internal static extern int ntdll_GetSystemMetrics(SystemMetric smIndex);


        public NTSTATUS RtlGetVersion(ref OSVERSIONINFOEX versionInfo)
        {
            return ntdll_RtlGetVersion(ref versionInfo);
        }

        public int GetSystemMetrics(SystemMetric smIndex)
        {
            return ntdll_GetSystemMetrics(smIndex);
        }
    }
}
