using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using MonkeyChat.Strings;
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
                // TODO: Check to see if the user exists, otherwise create a user

                // TODO: Navigate to MenuPage/NavigationPage/ChatPage
            }
            catch (Exception ex)
            {
                await _pageDialogService.DisplayAlertAsync("Error", ex.Message, "Ok");
            }

            IsBusy = false;
        }
    }
}