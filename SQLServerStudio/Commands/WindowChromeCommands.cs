
using System.Windows;
using System.Windows.Input;

namespace SQLServerStudio
{
    /// <summary>
    /// Commands for window chrome minimize, maximize and close buttons
    /// </summary>
    public class WindowChromeCommands
    {
        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public static readonly ICommand MinimizeCommand = new RelayCommand((window) => ((Window) window).WindowState = WindowState.Minimized);

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public static readonly ICommand MaximizeCommand = new RelayCommand((window) => ((Window)window).WindowState ^= WindowState.Maximized);

        /// <summary>
        /// The command to close the window
        /// </summary>
        public static readonly ICommand CloseCommand = new RelayCommand((window) => ((Window)window).Close());
    }
}
