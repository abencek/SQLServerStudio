using System;
using System.Windows.Input;

namespace SQLServerStudio
{
    /// <summary>
    /// A basic command
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private Members

        /// <summary>
        /// A delegate that is executed by <see cref="Execute(object)"/> method
        /// </summary>
        private readonly Action<object> _execute;

        /// <summary>
        /// A delegate that is executed by <see cref="CanExecute(object)"/> method
        /// </summary>
        private readonly Func<object, bool> _canExecute;

        #endregion


        #region Public Events

        /// <summary>
        /// Event fired when <see cref="CanExecute(object)"/> may have changed
        /// Command Manager will automatically detect changes that might invalidate command
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion


        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="execute">Generic delegate of type <see cref="Action{T1}"/></param>
        /// <param name="canExecute">Generic delegate of type <see cref="Func{T, TResult}"/></param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Defines whether this command can execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>True, if this command can execute</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Executes the command action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        } 

        #endregion

    }
}
