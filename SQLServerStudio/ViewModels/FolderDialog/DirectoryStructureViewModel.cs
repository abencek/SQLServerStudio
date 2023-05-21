using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SQLServerStudio
{
    /// <summary>
    /// View Model for directory structure on machine
    /// </summary>
    public class DirectoryStructureViewModel : BaseViewModel
    {

        #region Public Properties

        /// <summary>
        /// A list of all logical drives on machine
        /// Each directory item can contain child elements
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }


        /// <summary>
        /// Selected/active item from directory structure
        /// </summary>
        public DirectoryItemViewModel SelectedItem { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to set selected item
        /// </summary>
        public ICommand SelectDirectoryItemCommand { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DirectoryStructureViewModel()
        {
            //Get all logical drives on machine
            var drives = DirectoryStructure.GetLogicalDrives();
            //Create View Model for each drive
            Items = new ObservableCollection<DirectoryItemViewModel>(
                drives.Select(x => new DirectoryItemViewModel(x.FullPath, DirectoryItemType.Drive))
                );

            //Set command
            SelectDirectoryItemCommand = new RelayCommand(SelectDirectoryItem);
        }

        #endregion


        #region Command Methods

        /// <summary>
        /// Gets selected item
        /// </summary>
        /// <param name="parameter">View Model of selected directory item</param>
        private void SelectDirectoryItem(object parameter)
        {
            SelectedItem = parameter as DirectoryItemViewModel;
        }

        #endregion
    }
}
