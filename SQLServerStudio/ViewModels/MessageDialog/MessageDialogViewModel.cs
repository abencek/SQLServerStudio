
using System;
using System.Windows.Input;

namespace SQLServerStudio
{
    /// <summary>
    /// View Model for dialog that can display message
    /// </summary>
    public class MessageDialogViewModel: BaseDialogViewModel
    {
        #region Public Properties

        /// <summary>
        /// Message to be displayed
        /// </summary>
        public string Message { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MessageDialogViewModel() :base()
        {
        }

        #endregion
    }
}
