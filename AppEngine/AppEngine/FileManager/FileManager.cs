﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppEngine
{
    /// <summary>
    ///  Provides any methods for handle manipulation data 
    ///  like writes or reads text from files and so on.
    /// </summary>
    public class FileManager : IFileManager
    {
        /// <summary>
        /// Normalizing a path based on the current operating systems like Windows,
        /// Linux, Mac OS.
        /// </summary>
        /// <param name="filePath">The path of file to normalize</param>
        /// <returns></returns>
        public string NormalizePath(string filePath) => filePath?.Replace('/', '\\');


        /// <summary>
        /// Resolve any relative element of the path to absolute path.
        /// </summary>
        /// <param name="filePath">The path of file</param>
        /// <returns></returns>
        public string ResolvePath(string filePath) => Path.GetFullPath(filePath);

        /// <summary>
        /// Reads some text from file.
        /// </summary>
        /// <param name="fileName">The name of file</param>
        /// <param name="fileFormat">The file format extension</param>
        /// <param name="filePath">The location of file to write to</param>
        /// <param name="isCreateNewAndSave">If value is true, indicating save file as new file</param>
        /// <returns></returns>

        public async Task<string> ReadTextFromFileAsync(string fileName, FileTypeExtension fileFormat, string filePath, bool isCreateNewAndSave = true)
        {
            var texts = default(string);

            filePath = NormalizePath(filePath);

            filePath = ResolvePath(filePath);

            try
            {
                await Task.Run(() =>
                {
                    using (var streamReader = (TextReader)new StreamReader(File.Open($"{filePath}/{fileName}{FileExtensions.FileTypeExtensions(fileFormat)}", FileMode.Open)))
                    {
                        while (streamReader.Peek() > -1)
                        {
                            texts = streamReader.ReadLine();
                        }
                    }

                });


               
            }
            catch(Exception Ex)
            {

            }

            return texts;



        }

        /// <summary>
        /// Writes some text to add to the file.
        /// </summary>
        /// <param name="text">The text to write to</param>
        /// <param name="fileName">The name of file</param>
        /// <param name="fileFormat">The file format extension</param>
        /// <param name="filePath">The location of file to write to</param>
        /// <param name="isAppend">If value is true, indicating add text to the end of file</param>
        /// <returns></returns>
        public async Task WriteTextToFileAsync(string fileName, FileTypeExtension fileFormat, string filePath, string text, bool isAppend)
        {
            FileExtensions.FileTypeExtensions(fileFormat);

            filePath = NormalizePath(filePath);

            filePath = ResolvePath(filePath);

            try
            {
                await AsyncEngine.AwaitAsync(nameof(FileManager) + filePath, async () =>
                {
                    await Task.Run(() =>
                    {
                        using (var streamWriter = (TextWriter)new StreamWriter(File.Open($"{filePath}{fileName}{FileExtensions.FileTypeExtensions(fileFormat)}", isAppend ? FileMode.Append : FileMode.Create)))
                        {
                            streamWriter.WriteLineAsync(text);
                        }

                    });

                });
            }
            catch (Exception Ex)
            {

            }
            
        }


    }
}