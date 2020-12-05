namespace OSVersionExt.Environment
{
    public interface IEnvironment
    {
        /// <summary>
        /// Determines whether the current operating system is a 64-bit operating system.
        /// </summary>
        /// <returns></returns>
        bool Is64BitOperatingSystem();
    }
}
