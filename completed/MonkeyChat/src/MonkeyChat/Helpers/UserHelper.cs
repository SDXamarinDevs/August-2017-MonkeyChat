using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonkeyChat.Data;
using MonkeyChat.Events;
using MonkeyChat.Models;
using MvvmHelpers;
using Prism.Events;

namespace MonkeyChat.Helpers
{
    public static class UserHelper
    {
        private static IEventAggregator _eventAggregator;
        private static IAppDataContext _context;

        public static bool IsLoading { get; private set; }

        public static ObservableRangeCollection<User> Users { get; private set; }

        public static void Initialize(IAppDataContext context, IEventAggregator eventAggregator)
        {
            Users = new ObservableRangeCollection<User>();
            _context = context;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<RefreshUsersEvent>().Subscribe(OnRefreshUsersEventPublished);
        }

        private static async void OnRefreshUsersEventPublished()
        {
            try
            {
                IsLoading = true;
                Users.ReplaceRange(await _context.Users.ReadAllItemsAsync());
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
