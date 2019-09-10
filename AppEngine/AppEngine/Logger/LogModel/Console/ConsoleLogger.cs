using System;

namespace AppEngine
{
    /// <summary>
    /// Logs the messages to the Console log.
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        /// Handles the logged message.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The level of the log message</param>
        public void Log(string message, LogLevel Level)
        {
            var category = default(string);

            var color = new ConsoleColor();
            var oldColor = new ConsoleColor();

            switch (Level)
            {
                case LogLevel.Trace:
                    category = "[TRACE]";
                    color = ConsoleColor.Magenta;
                    break;

                case LogLevel.Debug:
                    category = "[DEBUG]";
                    color = ConsoleColor.Gray;
                    break;

                case LogLevel.Information:
                    category = "[INFORMATION]";
                    color = ConsoleColor.Blue;
                    break;

                case LogLevel.Warning:
                    category = "[WARNING]";
                    color = ConsoleColor.Yellow;
                    break;

                case LogLevel.Error:
                    category = "[!ERROR!]";
                    color = ConsoleColor.Red;
                    break;

                case LogLevel.Critical:
                    category = "[!CRITICAL!]";
                    color = ConsoleColor.DarkRed;
                    break;

                case LogLevel.None:
                default:
                    category = "[--------]";
                    color = ConsoleColor.Gray;
                    break;


            }
            

            Console.ForegroundColor = color;

            Console.WriteLine($"{message}, Alert: {category}", color);

            Console.ForegroundColor = oldColor;
        }
    }
}
