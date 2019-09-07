using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppEngine
{
    /// <summary>
    /// The standard log factory for AppEngine
    /// Logs details to the Debug by default
    /// </summary>
    public class BaseLogEngine : ILogEngine
    {

        #region Properties

        /// <summary>
        /// The list of logger in this factory.
        /// </summary>
        protected List<ILogger> loggers = new List<ILogger>();

        /// <summary>
        /// A lock for the logger list to keep it thread safe.
        /// </summary>
        protected object loggersLock = new object();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="loggers">The loggers to add to the factory, on top of the stock loggers already included</param>
        public BaseLogEngine(ILogger[] loggers = null)
        {
            AddLogger(new DebugLogger());

            if (loggers != null)
                foreach (var logger in loggers)
                    AddLogger(logger);
        }

        #endregion
        

        #region Methods

        /// <summary>
        /// Adds the specific logger to this factory.
        /// </summary>
        /// <param name="logger">The logger</param>
        public void AddLogger(ILogger logger)
        {
            lock (loggersLock)
            {
                if (!loggers.Contains(logger))
                    loggers.Add(logger);
            }
        }

        /// <summary>
        /// Removes the specified logger from this factory.
        /// </summary>
        /// <param name="logger">The logger</param>
        public void RemoveLogger(ILogger logger)
        {
            lock (loggersLock)
            {
                if (loggers.Contains(logger))
                    loggers.Remove(logger);

            }
        }

        /// <summary>
        /// Logs the specific message to all logger in this factory.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The level of the message to logged</param>
        /// <param name="memberName">The method/function this message was logged from</param>
        /// <param name="filePath">The file of location that this message was logged from</param>
        /// <param name="number">The line of code in this message was logged from</param>
        public void Log(string message, LogLevel level = LogLevel.Debug,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int LineNumber = 0)
        {
            message = $"Message: [{message}] - " +
                      $"File Name: [{Path.GetFileName(memberName)}{Path.GetExtension(memberName)}] - " +
                      $"Location: [{Path.GetFullPath(filePath)}] - " +
                      $"Line: [{LineNumber}]";

            loggers.ForEach(loggers => loggers.Log(message, level));

        } 

        #endregion
    }
}
