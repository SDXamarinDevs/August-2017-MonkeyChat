using AzureMobileClient.Helpers;

namespace MonkeyChat.Models
{
    public class ChatMessage : EntityData
    {
        public string Message { get; set; }

        public string FromEmail { get; set; }
    }
}