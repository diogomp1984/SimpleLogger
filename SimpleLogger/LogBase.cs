namespace SimpleLogger
{
    public abstract class LogBase
    {
        public abstract void Log(string fileName, string eventDesc, string logLevel, LogType logType);
    }
}
