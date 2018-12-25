using log4net;
using System;

namespace Define.Common.Helpers
{
    public class LogHelper
    {
        private static readonly ILog _logger;

        private static ILog GetLogger(string logName)
        {
            var log = LogManager.GetLogger(logName);
            return log;
        }

        static LogHelper()
        {
            _logger = GetLogger("Define");
        }

        public static void Debug(string message)
        {
            _logger.Debug(message);
        }

        public static void Info(string message)
        {
            _logger.Info(message);
        }

        public static void Warn(string message)
        {
            _logger.Warn(message);
        }

        public static void Error(string message, Exception e = null)
        {
            _logger.Error(message, e);
        }

        public static void Fatal(string message, Exception e = null)
        {
            _logger.Fatal(message, e);
        }
    }
}
