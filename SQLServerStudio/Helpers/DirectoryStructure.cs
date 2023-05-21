using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SQLServerStudio
{
    /// <summary>
    /// A helper class that queries data about directories
    /// </summary>
    public static class DirectoryStructure
    {
        /// <summary>
        /// Get all logical drives on the computer
        /// </summary>
        /// <returns>List of logical drives</returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {

            return Directory.GetLogicalDrives()
                .Select(drive => new DirectoryItem {
                    FullPath = drive,
                    Type = DirectoryItemType.Drive
                })
                .ToList();
        }


        /// <summary>
        /// Get directory top-level content
        /// </summary>
        /// <param name="fullPath">Directory full path</param>
        /// <returns>Directory top-level content</returns>
        public static List<DirectoryItem> GetDirectoryContent(string fullPath)
        { 
            try
            {
                //Get all top-level subdirectories
                var dirs = Directory.GetDirectories(fullPath);

                //Convert them to list of DirectoryItems
                var items = dirs.Select(dir => new DirectoryItem {
                    FullPath = dir,
                    Type = DirectoryItemType.Folder
                }).ToList();

                return items;
            }
            catch
            {
                //No available subdirectories (permission, etc.), return empty list
                return new List<DirectoryItem>();
            }

        }
    }
}
