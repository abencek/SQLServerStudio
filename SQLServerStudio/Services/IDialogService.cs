using System;
using System.Windows;

namespace SQLServerStudio
{
    /// <summary>
    /// An interface for a class that will handle dialogs 
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// This will open and show dialog window
        /// </summary>
        /// <typeparam name="TViewModel">View Model type of dialog window</typeparam>
        /// <typeparam name="TView">View type of dialog window</typeparam>
        /// <param name="closeCallback">Action that runs before dialog is closed</param>
        /// <param name="dialogOwner">Dialog window owner</param>
        void ShowDialog<TViewModel, TView>(Action<object> closeCallback, object dialogOwner)
            where TViewModel : IDialogCloseRequested, new()
            where TView : Window, new();
    }
}
