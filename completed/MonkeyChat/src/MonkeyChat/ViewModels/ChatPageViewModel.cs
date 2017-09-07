using System.Linq;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using MonkeyChat.Models;
using Prism.AppModel;
using MvvmHelpers;
using MonkeyChat.Data;
using MonkeyChat.Helpers;
using MonkeyChat.Events;

namespace MonkeyChat.ViewModels
{
    public class ChatPageViewModel : ViewModelBase
    {
        private IAppDataContext _context { get; }
        private IEventAggregator _eventAggregator { get; }

        public ChatPageViewModel(INavigationService navigationService, IApplicationStore applicationStore,
                                 IDeviceService deviceService, IAppDataContext context, IEventAggregator eventAggregator)
            : base(navigationService, applicationStore, deviceService)
        {
            _context = context;
            _eventAggregator = eventAggregator;

            Messages = new ObservableRangeCollection<ChatMessage>();
            RefreshCommand = new DelegateCommand(OnRefreshCommandExecuted, () => IsNotBusy).ObservesProperty(() => IsNotBusy);
            AddMessageCommand = new DelegateCommand(OnAddMessageCommandExecuted);
            AddMessageCompositeCommand = new CompositeCommand();
            AddMessageCompositeCommand.RegisterCommand(AddMessageCommand);
            AddMessageCompositeCommand.RegisterCommand(RefreshCommand);
        }

        public ObservableRangeCollection<ChatMessage> Messages { get; set; }

        public string Text { get; set; }

        public DelegateCommand AddMessageCommand { get; }

        public DelegateCommand RefreshCommand { get; }

        public CompositeCommand AddMessageCompositeCommand { get; }

        public ChatRoom Room { get; set; }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            Room = parameters.GetValue<ChatRoom>("room");
            _eventAggregator.GetEvent<RefreshUsersEvent>().Publish();
            Title = Room.Name;
            OnRefreshCommandExecuted();
        }

        private async void OnRefreshCommandExecuted()
        {
            IsBusy = true;
            await _context.Messages.SyncAsync();
            Messages.ReplaceRange((await _context.Messages.ReadAllItemsAsync()).Where(m => m.RoomId == Room.Id));
            IsBusy = false;
        }

        private async void OnAddMessageCommandExecuted()
        {
            IsBusy = true;

            await _context.Messages.CreateItemAsync(new ChatMessage()
            {
                Message = Text.Trim(),
                UserId = Settings.Current.UserId,
                RoomId = Room.Id
            });
            Text = null;

            IsBusy = false;
        }
    }
}