using System;

namespace SQLServerStudio
{
    /// <summary>
    /// Event data for CloseRequested event handler
    /// </summary>
    public class DialogCloseRequestedEventArgs : EventArgs
    {
        #region Public Properties

        /// <summary>
        /// Gets a dialog result value
        /// </summary>
        public bool DialogResult { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dialogResult">Dialog result value</param>
        public DialogCloseRequestedEventArgs(bool dialogResult)
        {
            DialogResult = dialogResult;
        } 

        #endregion
    }
}
