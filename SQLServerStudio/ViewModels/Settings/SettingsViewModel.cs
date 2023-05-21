using System.Windows.Input;

namespace SQLServerStudio
{
    /// <summary>
    /// A View Model for application settings
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {

        #region Private Members

        /// <summary>
        /// Backing field for ServerName Property
        /// </summary>
        private string _serverName;

        /// <summary>
        /// Backing field for ScriptsLocation Property
        /// </summary>
        private string _scriptsLocation; 

        #endregion


        #region Public Properties

        /// <summary>
        /// SQL server name where scripts executes
        /// </summary>
        public string ServerName {
            get { return _serverName; }
            set
            {
                _serverName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Full path to folder with SQL files
        /// </summary>
        public string ScriptsLocation
        {
            get { return _scriptsLocation; }
            set
            {
                _scriptsLocation = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Public Commands

        /// <summary>
        /// This opens dialog to change server name
        /// </summary>
        public ICommand OpenServerNameDialogCommand { get; set; }

        /// <summary>
        /// This opens dialog to change folder
        /// </summary>
        public ICommand OpenFolderDialogCommand { get; set; }

        /// <summary>
        /// This executes selected SQL scripts
        /// </summary>
        public ICommand ExecuteScriptsCommand { get; set; }

        #endregion

    }
}

