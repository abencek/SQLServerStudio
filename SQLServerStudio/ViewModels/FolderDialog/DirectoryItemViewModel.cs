using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace SQLServerStudio
{
    /// <summary>
    /// A View Model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// Backing field for Type Property
        /// </summary>
        private DirectoryItemType _type;

        /// <summary>
        /// Backing field for FullPath Property
        /// </summary>
        private string _fullPath;

        /// <summary>
        /// Backing field for Children Property
        /// </summary>
        private ObservableCollection<DirectoryItemViewModel> _children;

        #endregion


        #region Public Properties

        /// <summary>
        /// The type of this item
        /// </summary>
        public DirectoryItemType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Image name for this item
        /// </summary>
        public string ImageName => IsExpanded == true ? "folder-opened" : "folder-closed";


        /// <summary>
        /// Name for this item
        /// </summary>
        public string DisplayName
        {
            get
            {
                if (Type == DirectoryItemType.Drive)
                    return FullPath;

                return Path.GetFileName(FullPath);
            }
        }


        /// <summary>
        /// Full path (file location) for this item
        /// </summary>
        public string FullPath
        {
            get { return _fullPath; }
            set
            {
                _fullPath = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// A list of all top-level children contained inside this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children
        {
            get { return _children; }
            set
            {
                _children = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Indicates if this item is expanded
        /// </summary>
        public bool IsExpanded
        {
            get { return Children?.Count(x => x != null) > 0; }
            set
            {
                //UI tells us to expand
                if (value == true)
                {
                    Expand(null);
                }
                // UI tells us to close
                else
                {
                    ClearChildren();
                }                   

                //Update image name
                OnPropertyChanged("ImageName");

            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// A command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fullPath">Full path to this item</param>
        /// <param name="type">Type of this item</param>
        public DirectoryItemViewModel(string fullPath, DirectoryItemType type)
        {
            ExpandCommand = new RelayCommand(Expand);

            FullPath = fullPath;
            Type = type;

            ClearChildren();
        }

        #endregion


        #region Command Methods

        /// <summary>
        /// Expands this item and gets all subdirectories
        /// </summary>
        private void Expand(object parameter)
        {

            Children = new ObservableCollection<DirectoryItemViewModel>(
                DirectoryStructure.GetDirectoryContent(FullPath)
                .Select(x => new DirectoryItemViewModel(x.FullPath, x.Type))
                );

        }

        #endregion


        #region Helper Methods

        /// <summary>
        /// Removes all children from the list
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void ClearChildren()
        {
            Children = new ObservableCollection<DirectoryItemViewModel>();

            //Create dummy item to show expand arrow icon
            Children.Add(null);
        }
     
        #endregion
    }
}
