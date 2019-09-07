using System;

namespace AppEngine
{
    /// <summary>
    /// A logger that will handle log message to file.
    /// </summary>
    public class FileLogger : ILogger
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fileName">The name of file logger.</param>
        /// <param name="fileFormat">The format extension of file logger</param>
        /// <param name="filePath">The location of file logger</param>
        public FileLogger(string fileName, FileTypeExtension fileFormat, string filePath)
        {
            FilePath = filePath;
            FileName = fileName;
            FileFormat = fileFormat;
        }


        #endregion


        #region Properties

        /// <summary>
        /// The location path of file logger.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The name of file logger.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The format extension of file logger.
        /// </summary>
        public FileTypeExtension FileFormat { get; set; } = FileTypeExtension.TXT;

        /// <summary>
        /// If true, logs the current time with each message.
        /// </summary>
        public bool LogTime { get; set; } = true;

        /// <summary>
        /// The file manager to handle any logs message.
        /// </summary>
        IFileManager File { get; set; } = new FileManager();

        #endregion


        /// <summary>
        /// Handles the logged message.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="Level">The level of the log message</param>
        public void Log(string message, LogLevel Level)
        {
            var currentTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            var timeLogger = LogTime ? $"Date: [{currentTime}]" : "";

            File.WriteTextToFileAsync(FileName, FileFormat, FilePath, $"{message} - {timeLogger}{Environment.NewLine}", isAppend:true);
        }
    }
}
