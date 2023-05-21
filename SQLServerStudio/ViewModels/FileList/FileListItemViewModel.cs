using System.Collections.Generic;
using System.Data;
using System.Windows.Input;

namespace SQLServerStudio
{
    /// <summary>
    /// A View Model for each item in FileList control
    /// </summary>
    public class FileListItemViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// Backing field for ItemState Property
        /// </summary>
        private FileListItemState _itemState;

        /// <summary>
        /// Backing field for IsChecked Property
        /// </summary>
        private bool _isChecked;

        /// <summary>
        /// Backing field for FileName Property
        /// </summary>
        private string _fileName;

        /// <summary>
        /// Backing field for ActionText Property
        /// </summary>
        private string _actionText;

        /// <summary>
        /// Backing field for FilePath Property
        /// </summary>
        private string _filePath;

        /// <summary>
        /// Backing field for FileContent Property
        /// </summary>
        private string _fileContent;

        /// <summary>
        /// Backing field for Results Property
        /// </summary>
        private List<DataTable> _results;

        #endregion


        #region Public Properties

        /// <summary>
        /// Current state of this item 
        /// </summary>
        public FileListItemState ItemState
        {
            get { return _itemState; }
            set
            {
                _itemState = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Indicates if this item is selected for execution
        /// </summary>
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Display name of this item
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Action name that this item can run
        /// </summary>
        public string ActionText
        {
            get { return _actionText; }
            set
            {
                _actionText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Full file path to sql script
        /// </summary>
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// SQL script content
        /// </summary>
        public string FileContent
        {
            get { return _fileContent; }
            set
            {
                _fileContent = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Results returned from the server after execution of SQL script
        /// </summary>
        public List<DataTable> Results
        {
            get { return _results; }
            set
            {
                _results = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Public Commands

        /// <summary>
        /// Command that sets this item as active/selected
        /// </summary>
        public ICommand SetActiveItemCommand { get; set; }

        #endregion

    }
      
}
