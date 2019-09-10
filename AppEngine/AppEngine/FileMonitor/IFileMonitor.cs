using System;
using System.ComponentModel;
using System.IO;

namespace AppEngine
{
    /// <summary>
    /// Watch the directory specified at run time.
    /// </summary>
    public interface IFileMonitor
    {
        /// <summary>
        /// Create a new instance of FileSystemWatcher and set its properties.
        /// </summary>
        FileSystemWatcher EngineMonitor { get; set; }

        /// <summary>
        /// The file path to watch for.
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// If true, indicating watch all files from subdirectories too
        /// the default is true.
        /// </summary>
        bool IncludeSubdirectories { get; set; }

        /// <summary>
        /// The type of files to watch
        /// The default is (*.*) / Watch all files.
        /// </summary>
        string Filter { get; set; }

        /// <summary>
        /// Gets and sets the origin path.
        /// </summary>
        string OriginFilePath { get; set; }

        /// <summary>
        /// Indicating whether the component is should be enabled or not
        /// the default is true.
        /// </summary>
        bool EnableRaisingEvents { get; set; }

        void OnError(object sender, ErrorEventArgs e);

        void OnDeleted(object sender, FileSystemEventArgs e);

        void OnRenamed(object sender, RenamedEventArgs e);

        void OnCreated(object sender, FileSystemEventArgs e);

        void OnChanged(object sender, FileSystemEventArgs e);

        void Message(string message, ConsoleColor color = ConsoleColor.Blue);

        void EngineMonitoring();
    }
}
