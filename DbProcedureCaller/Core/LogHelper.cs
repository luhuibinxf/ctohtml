using System;
using System.IO;

namespace DbProcedureCaller.Core
{
    public static class LogHelper
    {
        private static readonly object _lock = new object();
        private static string _logFile = string.Empty;

        public static void Init(string logFile = "")
        {
            if (string.IsNullOrEmpty(logFile))
            {
                logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "server.log");
            }
            _logFile = logFile;
        }

        public static void Log(string message, string level = "INFO")
        {
            try
            {
                if (string.IsNullOrEmpty(_logFile))
                {
                    Init();
                }

                lock (_lock)
                {
                    string logEntry = string.Format("[{0}] [{1}] {2}",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        level,
                        message);
                    File.AppendAllText(_logFile, logEntry + Environment.NewLine);
                }
            }
            catch
            {
            }
        }

        public static void LogInfo(string message)
        {
            Log(message, "INFO");
        }

        public static void LogError(string message)
        {
            Log(message, "ERROR");
        }

        public static void LogWarning(string message)
        {
            Log(message, "WARNING");
        }

        public static void LogDebug(string message)
        {
            Log(message, "DEBUG");
        }

        public static void LogException(Exception ex, string context = "")
        {
            string message = string.IsNullOrEmpty(context)
                ? ex.Message
                : $"{context}: {ex.Message}";
            Log(message, "ERROR");
        }
    }
}