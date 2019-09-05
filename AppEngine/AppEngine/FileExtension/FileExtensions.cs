using System.Diagnostics;

namespace AppEngine
{
    /// <summary>
    /// The class provides a specific type of file extension. 
    /// </summary>
    public static class FileExtensions
    {
        /// <summary>
        /// Gets the specific file extension, and return it.
        /// </summary>
        /// <param name="fileType">The type of file extension format</param>
        /// <returns></returns>
        public static string FileTypeExtensions(FileTypeExtension type)
        {
            switch (type)
            {
                case FileTypeExtension.DOC:
                    return ".doc";
                case FileTypeExtension.TXT:
                    return ".txt";
                case FileTypeExtension.PDF:
                    return ".pdf";
                case FileTypeExtension.LOG:
                    return ".log";
                default:
                    Debugger.Break();
                    return null;
            }
        }
    }
}
