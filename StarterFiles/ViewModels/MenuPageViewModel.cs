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
using MvvmHelpers;
using Prism.AppModel;
using MonkeyChat.Data;

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
            AddRoomCommand = new DelegateCommand(OnAddRoomCommandExecuted);
        }

        public ObservableRangeCollection<ChatRoom> Rooms { get; }

        public DelegateCommand AddRoomCommand { get; }

        public DelegateCommand<ChatRoom> RoomTappedCommand { get; }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            IsBusy = true;
            // TODO: Load the Chat Rooms
            IsBusy = false;
        }

        private async void OnRoomTappedCommandExecuted(ChatRoom room) =>
            await _navigationService.NavigateAsync($"NavigationPage/ChatPage", "chatRoom", room);

        private void OnAddRoomCommandExecuted()
        {
            //TODO Add a new Chat Room
        }
    }
}