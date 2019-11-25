namespace Stark.MVVM
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Base ViewModel class. Extend this class to implement your own ViewModels.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        /// <summary>
        /// Event that fires when <see cref="OnPropertyChanged"/> is called.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Event that fires when <see cref="OnPropertyChanging"/> is called.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Should be called in the setter of a property after setting the value. Providing propertyName parameter is optional.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed. Calling memeber name optional.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Should be called in the setter of a property before setting the value. Providing propertyName parameter is optional.
        /// </summary>
        /// <param name="propertyName">The name of the property that is changing. Calling memeber name optional.</param>
        protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }
    }
}
