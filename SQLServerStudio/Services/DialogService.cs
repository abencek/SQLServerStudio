using System;
using System.Collections.Generic;
using System.Windows;

namespace SQLServerStudio
{
    /// <summary>
    /// Service that handles any interaction with dialog windows
    /// </summary>
    public class DialogService: IDialogService
       
    {
        /// <summary>
        /// This will open and show dialog window
        /// </summary>
        /// <typeparam name="TViewModel">View Model type of dialog window</typeparam>
        /// <typeparam name="TView">View type of dialog window</typeparam>
        /// <param name="closeCallback">Action that runs before dialog is closed</param>
        /// <param name="dialogOwner">Dialog window owner</param>
        public void ShowDialog<TViewModel, TView>(Action<object> closeCallback, object dialogOwner)
            where TViewModel : IDialogCloseRequested, new()
            where TView : Window, new()
        {          
            //Create dialog instance
            var dialog = new TView();
            //Create View Model instance
            var viewModel = new TViewModel();
            //Set dialog
            dialog.DataContext = viewModel;
            dialog.Owner = (Window)dialogOwner;

            //Create event handler for CloseRequested event
            EventHandler<DialogCloseRequestedEventArgs> handler = null;
            handler = (object s, DialogCloseRequestedEventArgs e) =>
            {
                viewModel.CloseRequested -= handler;

                //Run callback (OK button pressed)
                if (e.DialogResult == true)
                {
                    closeCallback(viewModel);
                }
                //Run callback (Cancel button pressed)
                else
                {
                    closeCallback(null);
                }

                dialog.DialogResult = e.DialogResult;
                dialog.Close();
            };

            //Subscribe to View Model CloseRequested event
            viewModel.CloseRequested += handler;

            dialog.ShowDialog();
        }


    }
}
