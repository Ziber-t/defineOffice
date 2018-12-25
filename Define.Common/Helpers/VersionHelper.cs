using System.Diagnostics;

namespace Define.Common.Helpers
{
    public static class VersionHelper
    {
        /// <summary>
        /// Returns the version of the MS Word
        /// </summary>
        /// <returns>MS Word version</returns>
        public static string GetWordVersion()
        {
            return FileVersionInfo.GetVersionInfo(Process.GetCurrentProcess().MainModule.FileName).ProductVersion;
        }

        /// <summary>
        /// Returns the version of the Add-In
        /// </summary>
        /// <returns>MS Word version</returns>
        public static string GetAddInVersion()
        {
            return FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).ProductVersion;
        }
    }
}
