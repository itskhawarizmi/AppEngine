using System.Diagnostics;

namespace AppEngine
{
    /// <summary>
    /// Logs the messages to the Debug log.
    /// </summary>
    public class DebugLogger : ILogger
    {
        /// <summary>
        /// Handles the logged message.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The level of the log message</param>
        public void Log(string message, LogLevel Level)
        {

            var category = default(string);

            switch (Level)
            {
                case LogLevel.Trace:
                    category = "[TRACE]";
                    break;

                case LogLevel.Debug:
                    category = "[DEBUG]";
                    break;

                case LogLevel.Information:
                    category = "[INFORMATION]";
                    break;

                case LogLevel.Warning:
                    category = "[WARNING]";
                    break;

                case LogLevel.Error:
                    category = "[!ERROR!]";
                    break;

                case LogLevel.Critical:
                    category = "[!CRITICAL!]";
                    break;

                case LogLevel.None:
                default:
                    category = "[--------]";
                    break;


            }



            Debug.WriteLine(message, $"Alert: {category} ");
        }
    }
}
