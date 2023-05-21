using System;

namespace SQLServerStudio
{
    /// <summary>
    /// An interface for dialog View Model which can handle dialog close event
    /// </summary>
    public interface IDialogCloseRequested
    {
        /// <summary>
        /// Occurs when the dialog is about to close
        /// </summary>
        event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
    }

}
