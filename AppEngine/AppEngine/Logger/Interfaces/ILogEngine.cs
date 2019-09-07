using System;
using System.Runtime.CompilerServices;

namespace AppEngine
{
    /// <summary>
    /// The loggers to log messages for the developer.
    /// </summary>
    public interface ILogEngine
    {
        /// <summary>
        /// Logs the specific message to all logger in this factory.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The level of the message to logged</param>
        /// <param name="memberName">The method/function this message was logged from</param>
        /// <param name="filePath">The file of location that this message was logged from</param>
        /// <param name="number">The line of code in this message was logged from</param>
        void Log(string message, LogLevel level = LogLevel.Debug,
                [CallerMemberName]string memberName = "", 
                [CallerFilePath]string filePath = "", 
                [CallerLineNumber]int LineNumber = 0
        );
        

        /// <summary>
        /// Adds the specific logger to this factory.
        /// </summary>
        /// <param name="logger">The logger</param>
        void AddLogger(ILogger logger);

        /// <summary>
        /// Removes the specified logger from this factory.
        /// </summary>
        /// <param name="logger">The logger</param>
        void RemoveLogger(ILogger logger);



    }
}
