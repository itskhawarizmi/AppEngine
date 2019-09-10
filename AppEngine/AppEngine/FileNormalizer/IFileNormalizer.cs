namespace AppEngine
{
    /// <summary>
    ///  Provides any methods for handle normalize file. 
    /// </summary>
    public interface IFileNormalizer
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
    }
}
