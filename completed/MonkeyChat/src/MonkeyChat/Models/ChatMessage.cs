using System;
using AzureMobileClient.Helpers;

namespace MonkeyChat.Models
{
    public class ChatMessage : EntityData
    {
        public string Message { get; set; }

        public string RoomId { get; set; }

        public string UserId { get; set; }
    }
}
