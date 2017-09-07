using System.Threading.Tasks;
using MonkeyChat.Data;
using MonkeyChat.Helpers;
using Prism.AppModel;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;

namespace MonkeyChat.ViewModels
{

    public class SplashScreenPageViewModel : ViewModelBase
    {
        private IAppDataContext _context { get; }
        private IEventAggregator _eventAggregator { get; }

        public SplashScreenPageViewModel(INavigationService navigationService, IApplicationStore applicationStore,
                                         IDeviceService deviceService, IAppDataContext context, IEventAggregator eventAggregator)
            : base(navigationService, applicationStore, deviceService)
        {
            _context = context;
            _eventAggregator = eventAggregator;
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            // Simulated long running task. You should remove this in your app.
            await Task.Delay(1500);

            UserHelper.Initialize(_context, _eventAggregator);

            await _navigationService.NavigateAsync("/LoginPage");
        }
    }
}