using System;
using System.Linq;
using MvvmHelpers;
using Prism;
using Prism.AppModel;
using Prism.Navigation;
using Prism.Services;

namespace MonkeyChat.ViewModels
{
    public class ViewModelBase : BaseViewModel, IApplicationLifecycle, IActiveAware, INavigationAware, IDestructible
    {
        protected IApplicationStore _applicationStore { get; }

        protected IDeviceService _deviceService { get; }

        protected INavigationService _navigationService { get; }

        public ViewModelBase(INavigationService navigationService, IApplicationStore applicationStore, 
                             IDeviceService deviceService)
        {
            _applicationStore = applicationStore;
            _deviceService = deviceService;
            _navigationService = navigationService;
        }

#region IActiveAware

        public bool IsActive { get; set; }

        public event EventHandler IsActiveChanged;

        private void OnIsActiveChanged()
        {
            IsActiveChanged(this, EventArgs.Empty);

            if(IsActive)
            {
                OnIsActive();
            }
            else
            {
                OnIsNotActive();
            }
        }

        protected virtual void OnIsActive() { }

        protected virtual void OnIsNotActive() { }

#endregion IActiveAware

#region IApplicationLifecycle

        public virtual void OnResume() { }

        public virtual void OnSleep() { }

#endregion IApplicationLifecycle

#region INavigationAware

        public virtual void OnNavigatingTo(NavigationParameters parameters) { }

        public virtual void OnNavigatedTo(NavigationParameters parameters) { }

        public virtual void OnNavigatedFrom(NavigationParameters parameters) { }

#endregion INavigationAware

#region IDestructible

        public virtual void Destroy() { }

#endregion IDestructible
    }
}