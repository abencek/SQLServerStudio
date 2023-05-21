using System.Collections.ObjectModel;


namespace SQLServerStudio
{
    /// <summary>
    /// A design View Model with mock data for FileList control
    /// </summary>
    public class FileListDesignModel : FileListViewModel
    {
        #region Public Properties

        /// <summary>
        /// A single static instance of design model
        /// </summary>
        public static FileListDesignModel Instance { get; set; } = new();
        
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public FileListDesignModel()
        {
            Items = new ObservableCollection<FileListItemViewModel> {
                new FileListItemViewModel
                {
                    IsChecked = false,
                    FileName = "File with script 1.sql",
                    ActionText = "Edit"
                } ,
                 new FileListItemViewModel
                {
                    IsChecked = false,
                    FileName = "File with script 2.sql",
                    ActionText = "Edit"
                },
                  new FileListItemViewModel
                {
                    IsChecked = false,
                    FileName = "File with script 3.sql",
                    ActionText = "Edit"
                },
                   new FileListItemViewModel
                {
                    IsChecked = false,
                    FileName = "File with script 4.sql",
                    ActionText = "Edit"
                }

            };
        } 

        #endregion
    }
}
