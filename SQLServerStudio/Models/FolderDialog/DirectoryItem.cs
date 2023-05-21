using System.IO;

namespace SQLServerStudio
{
    /// <summary>
    /// Model for directory item (can be drive or folder)
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// Full path to this item
        /// </summary>
        public string FullPath { get; set; }


        /// <summary>
        /// The short name of this item (drive or directory name)
        /// </summary>
        public string Name
        {
            get
            {
                return this.Type == DirectoryItemType.Drive ? this.FullPath : Path.GetDirectoryName(FullPath);
            }
        }

    }
}
