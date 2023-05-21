using System;


namespace SQLServerStudio
{
    /// <summary>
    /// Service that handles any interaction with simple text message dialog windows
    /// </summary>
    public class MessageService: IMessageService      
    {
        #region Private Members
        /// <summary>
        /// Owner of message dialog window
        /// </summary>
        private readonly MainWindow _owner;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="owner">Owner window</param>
        public MessageService(MainWindow owner)
        {
            _owner = owner;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This will open and show dialog window
        /// </summary>
        /// <param name="message">Text displayed by dialog</param>
        public void ShowDialog(string message)
        {      
            //Create dialog instance
            var dialog = new MessageDialog();
            //Create View Model instance
            var viewModel = new MessageDialogViewModel
            {
                Message = message
            };
            //Set dialog
            dialog.DataContext = viewModel;
            dialog.Owner = _owner;

            //Create event handler for CloseRequested event
            EventHandler<DialogCloseRequestedEventArgs> handler = null;
            handler = (s, e) =>
            {
                viewModel.CloseRequested -= handler;

                dialog.DialogResult = e.DialogResult;
                dialog.Close();
            };

            //Subscribe to View Model CloseRequested event
            viewModel.CloseRequested += handler;

            dialog.ShowDialog();
        }

        #endregion

    }
}
