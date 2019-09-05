using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppEngine
{
    /// <summary>
    ///  Provides any methods for handle manipulation data 
    ///  like writes or reads text from files and so on.
    /// </summary>
    public interface IFileManager
    {
        /// <summary>
        /// Normalizing path of file based on the current operating systems
        /// like windows, linux, and mac.
        /// </summary>
        /// <param name="filePath">The path of file to normalize</param>
        /// <returns></returns>
        string NormalizePath(string filePath);


        /// <summary>
        /// Resolve any relative element of the path to absolute path.
        /// </summary>
        /// <param name="filePath">The path of file</param>
        /// <returns></returns>
        string ResolvePath(string filePath);


        /// <summary>
        /// Writes some text to add to the file.
        /// </summary>
        /// <param name="text">The text to write to</param>
        /// <param name="fileName">The name of file</param>
        /// <param name="fileFormat">The file format extension</param>
        /// <param name="filePath">The location of file to write to</param>
        /// <param name="isAppend">If value is true, indicating add text to the end of file</param>
        /// <returns></returns>
        Task WriteTextToFileAsync(string fileName, FileTypeExtension fileFormat, string filePath, string text, bool isAppend);


        /// <summary>
        /// Reads some text from file.
        /// </summary>
        /// <param name="fileName">The name of file</param>
        /// <param name="fileFormat">The file format extension</param>
        /// <param name="filePath">The location of file to write to</param>
        /// <param name="isCreateNewAndSave">If value is true, indicating save file as new file</param>
        /// <returns></returns>
        Task<string> ReadTextFromFileAsync(string fileName, FileTypeExtension fileFormat, string filePath, bool isCreateNewAndSave);
    }
}
