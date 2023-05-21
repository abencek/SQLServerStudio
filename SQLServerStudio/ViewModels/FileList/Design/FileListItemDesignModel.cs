

namespace SQLServerStudio
{
    /// <summary>
    /// A design View Model with mock data for FileListItem control
    /// </summary>
    public class FileListItemDesignModel : FileListItemViewModel
    {
        #region Public Properties

        /// <summary>
        /// A single static instance of design model
        /// </summary>
        public static FileListItemDesignModel Instance { get; set; } = new();

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public FileListItemDesignModel()
        {

            IsChecked = false;
            FileName = "File with script 1.sql";
            ActionText = "Edit Script";

        }

        #endregion
    }
}
