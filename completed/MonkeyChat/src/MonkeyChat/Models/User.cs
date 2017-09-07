using System;
using AzureMobileClient.Helpers;

namespace MonkeyChat.Models
{
    public class User : EntityData
    {
        public string DisplayName { get; set; }

        public string Email { get; set; }
    }
}
