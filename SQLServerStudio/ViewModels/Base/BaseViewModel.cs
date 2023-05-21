using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SQLServerStudio
{
    /// <summary>
    /// A base View Model that fires Property Change event
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// An event that is fired when any child property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// This will invoke <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name">Caller member's name</param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
