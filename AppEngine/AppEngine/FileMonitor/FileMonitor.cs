using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AppEngine
{
    /// <summary>
    /// A class monitor to watch the directory specified at run time.
    /// </summary>
    public class FileMonitor : IFileMonitor, IFileNormalizer
    {
        /// <summary>
        /// Create a new instance of FileSystemWatcher and set its properties.
        /// </summary>
        public FileSystemWatcher EngineMonitor { get; set; } = new FileSystemWatcher();

        /// <summary>
        /// The file path to watch for.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// If true, indicating watch all files from subdirectories too
        /// the default is true.
        /// </summary>
        public bool IncludeSubdirectories { get; set; } = true;

        /// <summary>
        /// The type of files to watch
        /// The default is (*.*) / Watch all files.
        /// </summary>
        public string Filter { get; set; } = "*.*";

        /// <summary>
        /// Gets and sets the origin path.
        /// </summary>
        public string OriginFilePath { get; set; }

        /// <summary>
        /// Indicating whether the component is should be enabled or not
        /// the default is true.
        /// </summary>
        public bool EnableRaisingEvents { get; set; } = true;

        /// <summary>
        /// Gets and sets the current date time.
        /// </summary>
        public string CurrentDateTime { get; set; }

        private FileManager File { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="filePath">The path of files we want to watch it</param>
        /// <param name="filter">The type of files we want to watch</param>
        /// <param name="includeSubdirectories">If true, indicating watch all files from subdirectories too, the default is true</param>
        /// <param name="enableRaisingEvents">Indicating whether the component is should be enabled or not, the default is true</param>
        public FileMonitor(string filePath, string filter = "*.*", bool includeSubdirectories = false, bool enableRaisingEvents = true)
        {
            FilePath = filePath;
            Filter = filter;
            IncludeSubdirectories = includeSubdirectories;
            EnableRaisingEvents = enableRaisingEvents;
        }

        /// <summary>
        /// Fires whenever occurs an error.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnError(object sender, ErrorEventArgs e)
        {
            CurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            Message($"Message: [{e.GetException()}] - Date: [{CurrentDateTime}]", ConsoleColor.Red);
        }

        /// <summary>
        /// Fires whenever files was deleted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnDeleted(object sender, FileSystemEventArgs e)
        {
            OriginFilePath = NormalizeOriginPath(e.FullPath, e.Name, OriginFilePath);

            CurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            Message($"STATUS: {e.ChangeType.ToString().ToUpper()} {Environment.NewLine}" +
                 $"\tFile: [{e.Name}] {Environment.NewLine}" +
                 $"\tLocation: [{OriginFilePath}] {Environment.NewLine}" +
                 $"\tDate: [{CurrentDateTime}]{Environment.NewLine}", ConsoleColor.DarkRed);
        }

        /// <summary>
        /// Fires whenever files was renamed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnRenamed(object sender, RenamedEventArgs e)
        {
            OriginFilePath = NormalizeOriginPath(e.FullPath, e.Name, OriginFilePath);

            CurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            Message($"STATUS: {e.ChangeType.ToString().ToUpper()} {Environment.NewLine}" +
                $"\tFile: [{e.OldName}] Renamed to [{e.Name}] {Environment.NewLine}" +
                $"\tLocation: [{OriginFilePath}] {Environment.NewLine}" +
                $"\tDate: [{CurrentDateTime}]{Environment.NewLine}", ConsoleColor.DarkMagenta);
        }

        /// <summary>
        /// Fires whenever files was created.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnCreated(object sender, FileSystemEventArgs e)
        {
            OriginFilePath = NormalizeOriginPath(e.FullPath, e.Name, OriginFilePath);

            CurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            Message($"STATUS: {e.ChangeType.ToString().ToUpper()} {Environment.NewLine}" +
                 $"\tFile: [{e.Name}] {Environment.NewLine}" +
                 $"\tLocation: [{OriginFilePath}] {Environment.NewLine}" +
                 $"\tDate: [{CurrentDateTime}]{Environment.NewLine}", ConsoleColor.Green);
        }

        /// <summary>
        /// Fires whenever files's changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnChanged(object sender, FileSystemEventArgs e)
        {
            OriginFilePath = NormalizeOriginPath(e.FullPath, e.Name, OriginFilePath);

            CurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            Message($"STATUS: {e.ChangeType.ToString().ToUpper()} {Environment.NewLine}" +
                    $"\tFile: [{e.Name}] {Environment.NewLine}" +
                    $"\tLocation: [{OriginFilePath}] {Environment.NewLine}" +
                    $"\tDate: [{CurrentDateTime}]{Environment.NewLine}", ConsoleColor.DarkMagenta);
        }

        /// <summary>
        /// The message to display any notification that was changed.
        /// </summary>
        /// <param name="message">The message to display</param>
        /// <param name="color">The color for message level</param>
        public void Message(string message, ConsoleColor color = ConsoleColor.Blue)
        {
            Console.ForegroundColor = color;

            Console.WriteLine(message, color);

        }


        /// <summary>
        /// Listens to the file system change notifications and raises events 
        /// when a directory, or file in a directory, changes.
        /// </summary>
        public async void EngineMonitoring()
        {
            using (var EngineMonitor = new FileSystemWatcher())
            {
                var keyInput = default(string);

                var flag = false;

                do
                {
                    EngineMonitor.Path = FilePath;

                    EngineMonitor.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite |
                                                 NotifyFilters.FileName | NotifyFilters.DirectoryName |
                                                 NotifyFilters.Attributes | NotifyFilters.Size | NotifyFilters.CreationTime;

                    EngineMonitor.EnableRaisingEvents = EnableRaisingEvents;

                    EngineMonitor.IncludeSubdirectories = IncludeSubdirectories;

                    EngineMonitor.Created += OnCreated;
                    EngineMonitor.Renamed += OnRenamed;
                    EngineMonitor.Changed += OnChanged;
                    EngineMonitor.Deleted += OnDeleted;

                    keyInput = Console.ReadLine();

                    switch (keyInput.ToLower())
                    {
                        case "appengine -e":
                            flag = true;
                            break;

                        case "appengine -r":
                            flag = true;
                            break;

                        default:
                            Debug.WriteLine($"There's no command for {keyInput.ToString()}");
                            break;
                    }
                    

                    
                } while (!flag);
                
            }

            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// Normalizing a path from fullpath to only path.
        /// </summary>
        /// <param name="fullPath">The current path of file</param>
        /// <param name="fileName">The name of file</param>
        /// <param name="normalPath">The normal path</param>
        /// <returns></returns>
        private string NormalizeOriginPath(string fullPath, string fileName, string normalPath)
        {
            fullPath = NormalizePath(fullPath);
            fullPath = ResolvePath(fullPath);

            return normalPath = fullPath.Substring(0, (fullPath.Length - fileName.Length) - 1);
        }

        /// <summary>
        /// Normalizing a path based on the current operating systems like Windows,
        /// Linux, Mac OS.
        /// </summary>
        /// <param name="filePath">The path of file to normalize</param>
        /// <returns></returns>
        public string NormalizePath(string filePath)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return filePath?.Replace('/', '\\').Trim();
            else
                return filePath?.Replace('\\', '/').Trim();

        }


        /// <summary>
        /// Resolve any relative element of the path to absolute path.
        /// </summary>
        /// <param name="filePath">The path of file</param>
        /// <returns></returns>
        public string ResolvePath(string filePath) => Path.GetFullPath(filePath);

        /// <summary>
        /// Reads all text from our monitor files.
        /// </summary>
        /// <param name="fileName">The name of file</param>
        /// <param name="fileFormat">The file format extension</param>
        /// <param name="filePath">The location of file to write to</param>
        /// <returns></returns>
        public async Task<List<string>> ReadTextFromMonitorFile(string filePath, string fileName, FileTypeExtension fileFormat)
        {
            var content = new List<string>();

            try
            {
                content = await File.ReadTextFromFileAsync(fileName, fileFormat, filePath, false);

                return content;
            }
            catch (Exception Ex)
            {
                Debug.WriteLine($"Message: {Ex.Message}");
            }


            return content;
        }
    }
}
