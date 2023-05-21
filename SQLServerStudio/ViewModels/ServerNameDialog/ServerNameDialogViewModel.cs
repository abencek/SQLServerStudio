

namespace SQLServerStudio
{
    /// <summary>
    /// View Model for dialog that sets new server name
    /// </summary>
    public class ServerNameDialogViewModel: BaseDialogViewModel
    {

        #region Public Properties

        /// <summary>
        /// SQL server instance name
        /// </summary>
        public string ServerName { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ServerNameDialogViewModel():base()
        {
        }

        #endregion
    }
}
