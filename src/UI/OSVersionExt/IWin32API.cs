namespace OSVersionExt.Win32API
{
    public interface IWin32API
    {
        NTSTATUS RtlGetVersion(ref OSVERSIONINFOEX versionInfo);
        int GetSystemMetrics(SystemMetric smIndex);
    }
}
