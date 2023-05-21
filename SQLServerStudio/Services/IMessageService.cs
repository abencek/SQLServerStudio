

using System;
using System.Windows;

namespace SQLServerStudio
{
    /// <summary>
    /// An interface for a class that will handle simple text message dialogs
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// This will open and show message dialog window
        /// </summary>
        /// <param name="message">Text displayed by dialog</param>
        void ShowDialog(string message);
    }
}
