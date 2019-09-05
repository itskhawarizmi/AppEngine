namespace AppEngine
{
    /// <summary>
    /// A logger that will handle log message from <see cref="ILogFactory"/>
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Handles the logged message.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The level of the log message</param>
        void Log(string message, LogLevel Level);
    }
}
