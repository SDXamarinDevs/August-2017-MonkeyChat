using System.Linq;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using MonkeyChat.Strings;
using MonkeyChat.Models;
using MvvmHelpers;
using Prism.AppModel;
using MonkeyChat.Data;
using MonkeyChat.Helpers;

namespace MonkeyChat.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {
        IAppDataContext _context { get; }
        public MenuPageViewModel(INavigationService navigationService, IApplicationStore applicationStore,
                                 IDeviceService deviceService, IAppDataContext context)
            : base(navigationService, applicationStore, deviceService)
        {
            _context = context;
            Title = Resources.MainPageTitle;
            Rooms = new ObservableRangeCollection<ChatRoom>();
            RoomTappedCommand = new DelegateCommand<ChatRoom>(OnRoomTappedCommandExecuted);
            AddRoomCommand = new DelegateCommand(async () =>
            {
                Rooms.Add(await _context.Rooms.CreateItemAsync(new ChatRoom()
                {
                    Name = $"Room {Rooms.Count() + 1}"
                }));
            });

            LogoutCommand = new DelegateCommand(OnLogoutCommandExecuted);
        }

        public ObservableRangeCollection<ChatRoom> Rooms { get; }

        public DelegateCommand AddRoomCommand { get; }

        public DelegateCommand LogoutCommand { get; }

        public DelegateCommand<ChatRoom> RoomTappedCommand { get; }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            IsBusy = true;
            await _context.Rooms.SyncAsync();
            var rooms = await _context.Rooms.ReadAllItemsAsync();
            Rooms.ReplaceRange(rooms);
            IsBusy = false;
        }

        private async void OnRoomTappedCommandExecuted(ChatRoom room) =>
            await _navigationService.NavigateAsync($"NavigationPage/ChatPage", "room", room);

        private async void OnLogoutCommandExecuted()
        {
            Settings.Current.UserId = string.Empty;
            await _navigationService.NavigateAsync("/LoginPage");
        }
    }
}