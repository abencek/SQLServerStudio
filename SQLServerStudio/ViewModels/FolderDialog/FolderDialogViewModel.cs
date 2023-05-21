
namespace SQLServerStudio
{
    /// <summary>
    /// View Model for Folder Dialog that allows browsing and selecting directories
    /// </summary>
    public class FolderDialogViewModel: BaseDialogViewModel
    {
        #region Public Properties

        /// <summary>
        /// Represents directory structure on machine
        /// </summary>
        public DirectoryStructureViewModel DirectoryStructure { get; private set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public FolderDialogViewModel() :base()
        {
            DirectoryStructure = new DirectoryStructureViewModel();
        }

        #endregion
    }
}
