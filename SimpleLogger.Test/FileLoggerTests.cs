namespace SimpleLogger.Test
{
    [TestFixture]
    public class FileLoggerTests
    {
        private const string FILE_NAME = "application.log";
        private FileLogger _fileLogger;

        [SetUp]
        public void Setup()
        {
            if (File.Exists(FILE_NAME))
            {
                File.Delete(FILE_NAME);
            }
            _fileLogger = new FileLogger();
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("INFo")]
        [TestCase(" INFO")]
        [TestCase("INFO ")]
        [TestCase("ABCD")]
        public void Log_WhenLogLevelIsInexistent_ThrowsLogLevelInexistentMessageAndLogFileIsNotCreated(string logLevel)
        {
            Assert.Multiple(() =>
            {
                Assert.That(() => _fileLogger.Log(FILE_NAME, "User logged in", logLevel),
                Throws.ArgumentException.With.Message.EqualTo("Log level inexistent!"));
                Assert.That(File.Exists(path: FILE_NAME), Is.False);
            });
        }

        [TestCase("")]
        [TestCase(null)]
        public void Log_WhenEventDescriptionIsNullOrEmpty_ThrowsErrorEventParameterIsNullOrEmptyAndLogFileIsNotCreated(string eventDesc)
        {
            Assert.Multiple(() =>
            {
                Assert.That(() => _fileLogger.Log(FILE_NAME, eventDesc, "INFO"),
                Throws.ArgumentException.With.Message.EqualTo("Event description is null or empty!"));
                Assert.That(File.Exists(path: FILE_NAME), Is.False);
            });
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("application.txt")]
        public void Log_WhenLogFileNameInputWithIncorrectFormat_ThrowsIncorrectFormatForLogFileNameAndLogFileIsNotCreated(string fileName)
        {
            Assert.Multiple(() =>
            {
                Assert.That(() => _fileLogger.Log(fileName, "User logged in", "INFO"),
                Throws.ArgumentException.With.Message.EqualTo("Log File Name parameter with Incorrect format!"));
                Assert.That(File.Exists(FILE_NAME), Is.False);
            });
        }

        [TestCase("DEBUG")]
        [TestCase("INFO")]
        [TestCase("WARNING")]
        [TestCase("ERROR")]
        [TestCase("FATAL")]
        [TestCase("TRACE")]
        public void Log_WhenLoggingOneEvent_ShouldContainOneEvent(string logLevel)
        {
            _fileLogger.Log(FILE_NAME, "User logged in", logLevel);
            int lines = File.ReadAllLines(FILE_NAME).Length;
            Assert.Multiple(() =>
            {
                Assert.That(lines, Is.EqualTo(1));
                Assert.That(File.Exists(FILE_NAME), Is.True);
            });
        }

        [Test]
        public void Log_WhenLoggingTwoEvents_ShouldContainTwoEvents()
        {
            _fileLogger.Log(FILE_NAME, "User logged in", "INFO");
            _fileLogger.Log(FILE_NAME, "Failed login attempt", "WARNING");

            int lines = File.ReadAllLines(FILE_NAME).Length;
            Assert.Multiple(() =>
            {
                Assert.That(lines, Is.EqualTo(2));
                Assert.That(File.Exists(FILE_NAME), Is.True);
            });
        }
    }
}