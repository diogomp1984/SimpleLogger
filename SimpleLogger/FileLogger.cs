namespace SimpleLogger
{
    public class FileLogger : LogBase
    {
        public override void Log(string fileName, string eventDesc, string logLevel, LogType logType = LogType.File)
        {
            if (String.IsNullOrEmpty(eventDesc))
            {
                throw new ArgumentException("Event description is null or empty!");
            }

            if (String.IsNullOrEmpty(fileName) || !fileName.EndsWith(".log"))
            {
                throw new ArgumentException("Log File Name parameter with Incorrect format!");
            }

           // using StreamWriter streamWriter = File.AppendText(fileName);

            switch (logLevel)
            {
                case "INFO":
                case "WARNING":
                case "DEBUG":
                case "ERROR":
                case "FATAL":
                case "TRACE":

                    var sw = new StreamWriter(fileName, append: true);

                    sw.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}][{logLevel}] {eventDesc}");
                    sw.Close();
                    break;
                default:
                    throw new ArgumentException("Log level inexistent!");
            }
        }
    }
}