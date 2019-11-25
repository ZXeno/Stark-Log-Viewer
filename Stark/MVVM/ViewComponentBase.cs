namespace Stark.MVVM
{
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// The MVVM extension for <see cref="ComponentBase"/> that handles <see cref="ViewModelBase"/> injection and passes <see cref="System.ComponentModel.INotifyPropertyChanged"/> events to the UI.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ViewComponentBase<T> : ComponentBase
        where T : notnull, ViewModelBase
    {
        /// <summary>
        /// The ViewModel that is injected into this <see cref="ViewComponentBase{T}"/>.
        /// </summary>
        [Inject]
        public T ViewModel { get; set; }

        /// <summary>
        /// Override for base OnInitializeAsync method. If this method is overridden, be sure to call "await base.OnInitializeAsync()" first in your override method.
        /// Subscribes the <see cref="NotifyStateChanged"/> method to the <see cref="ViewModelBase"/>'s <see cref="ViewModelBase.PropertyChanged"/> event.
        /// </summary>
        /// <returns>The completed task.</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            ViewModel.PropertyChanged += NotifyStateChanged;
        }

        /// <summary>
        /// Notifies the UI of state changes. Should be subscribed to the <see cref="ViewModelBase"/>'s <see cref="ViewModelBase.PropertyChanged"/> event.
        /// </summary>
        /// <param name="sender"><see cref="EventHandler"/> sender object</param>
        /// <param name="e"><see cref="EventArgs"/> of the event.</param>
        protected virtual void NotifyStateChanged(object sender, EventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }
    }
}
