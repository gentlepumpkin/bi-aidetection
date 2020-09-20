using OSVersionExt.Environment;


namespace OSVersionExt
{
    public class EnvironmentProvider : IEnvironment
    {
        public EnvironmentProvider()
        {
            // NOP
        }

        /// <summary>
        /// Determines whether the current operating system is a 64-bit operating system.
        /// </summary>
        /// <returns>true if the operating system is 64-bit; otherwise, false.</returns>
        public bool Is64BitOperatingSystem()
        {
            return System.Environment.Is64BitOperatingSystem;
        }
    }
}
