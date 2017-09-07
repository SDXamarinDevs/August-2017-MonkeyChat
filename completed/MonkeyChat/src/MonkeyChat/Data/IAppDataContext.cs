using AzureMobileClient.Helpers;
using MonkeyChat.Models;

namespace MonkeyChat.Data
{
    public interface IAppDataContext
    {
        //ICloudSyncTable<TodoItem> TodoItems { get; }
        ICloudSyncTable<ChatMessage> Messages { get; }

        ICloudSyncTable<ChatRoom> Rooms { get; }

        ICloudSyncTable<Models.User> Users { get; }
    }
}