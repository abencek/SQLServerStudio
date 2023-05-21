
using System;
using System.Windows.Input;

namespace SQLServerStudio
{

    /// <summary>
    /// A base class for dialog View Model
    /// </summary>
    public abstract class BaseDialogViewModel: BaseViewModel, IDialogCloseRequested
    {

        #region Commands

        /// <summary>
        /// A command for OK button
        /// </summary>
        public ICommand OkDialogCommand { get; set; }

        /// <summary>
        /// A command for Cancel button
        /// </summary>
        public ICommand CancelDialogCommand { get; set; }

        #endregion


        #region Public Events

        /// <summary>
        /// This event occurs when dialog is about to close
        /// </summary>
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested = delegate { }; //Empty delegate will avoid null check before event invocation

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseDialogViewModel()
        {

            OkDialogCommand = new RelayCommand(
                (o) => CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelDialogCommand = new RelayCommand(
                (o) => CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }     

        #endregion
    }
}
