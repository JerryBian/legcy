using System;
using System.IO;
using Laobian.Common.Base;

namespace Laobian.Jarvis
{
    /// <summary>
    /// Wrapper for path parsers
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Get full path
        /// </summary>
        /// <param name="path">Supplied path</param>
        /// <returns>The full path</returns>
        public static string GetFullPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = Environment.CurrentDirectory;
            }

            return Path.GetFullPath(path);
        }

        /// <summary>
        /// Check whether supplied path is valid folder path
        /// </summary>
        /// <param name="path">Supplied path</param>
        /// <returns>True if it's valid folder, otherwise false.</returns>
        public static bool IsPathFolder(string path)
        {
            var fullPath = GetFullPath(path);
            if (!Directory.Exists(fullPath))
            {
                if (!File.Exists(path))
                {
                    throw new InvalidOperationException($"Invalid path {path}.");
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Check whether path extension is expected.
        /// </summary>
        /// <param name="path">Supplied path</param>
        /// <param name="extension"></param>
        /// <returns>True if valid, otherwise false.</returns>
        public static bool IsExtensionValid(string path, string extension)
        {
            return Path.GetExtension(path).EqualsIgnoreCase(extension);
        }

        /// <summary>
        /// Get name of path.
        /// </summary>
        /// <param name="path">Supplied path.</param>
        /// <returns>The path name</returns>
        public static string GetPathName(string path)
        {
            return Path.GetFileName(path);
        }

        /// <summary>
        /// Combine folder and path name.
        /// </summary>
        /// <param name="folder">Supplied folder path.</param>
        /// <param name="fileName">Supplied file name.</param>
        /// <returns>The full name.</returns>
        public static string Combine(string folder, string fileName)
        {
            return Path.Combine(folder, fileName);
        }
    }
}
