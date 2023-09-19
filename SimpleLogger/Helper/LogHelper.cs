namespace SimpleLogger.Helper
{
    public static class LogHelper
    {
        private static LogBase? logger = null;
        public static void Log(string fileName, string eventDesc, string logLevel, LogType logType = LogType.File)
        {
            switch (logType)
            {
                case LogType.File:
                    logger = new FileLogger();
                    logger.Log(fileName, eventDesc, logLevel, logType);
                    break;
                default:
                    return;
            }
        }
    }
}
