
using System.Collections.ObjectModel;


namespace SQLServerStudio
{
    /// <summary>
    /// A View Model for FileList control
    /// </summary>
    public class FileListViewModel : BaseViewModel
    {

        #region Private Members

        /// <summary>
        /// Backing field for Items Property
        /// </summary>
        private ObservableCollection<FileListItemViewModel> _items;

        /// <summary>
        /// Backing field for ActiveItem Property
        /// </summary>
        private FileListItemViewModel _activeItem;

        #endregion


        #region Public Properties

        /// <summary>
        /// List of available items
        /// </summary>
        public ObservableCollection<FileListItemViewModel> Items 
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Selected item from <see cref="Items"/>
        /// </summary>
        public FileListItemViewModel ActiveItem
        {
            get { return _activeItem; }
            set
            {
                _activeItem = value;
                OnPropertyChanged();
            }
        }

        #endregion

    }
}

