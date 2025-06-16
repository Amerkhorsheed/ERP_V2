using System;
using System.IO;

namespace Erp_V1.BLL
{
    public static class Logger
    {
        private static readonly string LogPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "Erp_V1",
            "SecurityLogs.txt"
        );

        public static void LogSecurityEvent(string message)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(LogPath));
                File.AppendAllText(LogPath,
                    $"{DateTime.UtcNow:o} | {message}{Environment.NewLine}");
            }
            catch
            {
                // Fail silently—do not throw further
            }
        }
    }
}
