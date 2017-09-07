using System;
using System.Linq;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using MonkeyChat.Models;
using Prism.AppModel;
using MonkeyChat.Data;
using MonkeyChat.Helpers;

namespace MonkeyChat.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        IAppDataContext _context { get; }
        IPageDialogService _pageDialogService { get; }
        public LoginPageViewModel(INavigationService navigationService, IApplicationStore applicationStore,
                                  IDeviceService deviceService, IAppDataContext context, IPageDialogService pageDialogService)
            : base(navigationService, applicationStore, deviceService)
        {
            Title = "Login Page";
            _context = context;
            _pageDialogService = pageDialogService;
            Model = new User()
            {
                Email = Settings.Current.Email ?? string.Empty,
                DisplayName = Settings.Current.DisplayName ?? string.Empty
            };
            LoginCommand = new DelegateCommand(OnLoginCommandExecuted);
        }

        public User Model { get; set; }

        public DelegateCommand LoginCommand { get; }

        public bool LoginCommandCanExecute() =>
            !string.IsNullOrWhiteSpace(Model?.Email) && IsNotBusy;

        private async void OnLoginCommandExecuted()
        {
            IsBusy = true;
            try
            {
                await _context.Users.SyncAsync();
                var users = await _context.Users.ReadAllItemsAsync();
                if(users.Any(u => u.Email.Equals(Model.Email, StringComparison.OrdinalIgnoreCase), out User user))
                {
                    Settings.Current.UserId = user.Id;
                    if(!string.IsNullOrWhiteSpace(Model.DisplayName) && user.DisplayName != Model.DisplayName)
                    {
                        Settings.Current.DisplayName = user.DisplayName = Model.DisplayName;
                        user = await _context.Users.UpdateItemAsync(user);
                    }
                }
                else
                {
                    if(string.IsNullOrWhiteSpace(Model.DisplayName))
                    {
                        throw new Exception("The user has not been registered and you must provide an Display Name");
                    }

                    await _context.Users.CreateItemAsync(Model);
                }

                Settings.Current.DisplayName = Model.DisplayName;
                Settings.Current.Email = Model.Email;

                await _context.Rooms.SyncAsync();
                var rooms = await _context.Rooms.ReadAllItemsAsync();
                var defaultRoom = rooms.FirstOrDefault(r => r.Name == "General");
                if(defaultRoom == null)
                {
                    defaultRoom = await _context.Rooms.CreateItemAsync(new ChatRoom
                    {
                        Name = "General"
                    });
                }
                await _navigationService.NavigateAsync("/MenuPage/NavigationPage/ChatPage", "room", defaultRoom);
            }
            catch(Exception ex)
            {
                await _pageDialogService.DisplayAlertAsync("Error", ex.Message, "Ok");
            }

            IsBusy = false;
        }
    }
}