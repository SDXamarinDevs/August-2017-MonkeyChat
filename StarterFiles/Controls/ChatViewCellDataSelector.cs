using MonkeyChat.Helpers;
using MonkeyChat.Models;
using Xamarin.Forms;

namespace MonkeyChat.Controls
{
    public class ChatViewCellDataSelector : DataTemplateSelector
    {
        public ChatViewCellDataSelector()
        {
            // Retain instances!
            this.incomingDataTemplate = new DataTemplate(typeof(IncomingChatMessage));
            this.outgoingDataTemplate = new DataTemplate(typeof(OutgoingChatMessage));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var message = item as ChatMessage;
            if (message == null)
                return null;

            return message.FromEmail != Settings.Current.Email ? this.incomingDataTemplate : this.outgoingDataTemplate;
        }

        private readonly DataTemplate incomingDataTemplate;
        private readonly DataTemplate outgoingDataTemplate;
    }
}