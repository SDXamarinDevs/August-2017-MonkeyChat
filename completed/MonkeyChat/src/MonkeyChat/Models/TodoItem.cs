using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AzureMobileClient.Helpers;
using MvvmHelpers;

namespace MonkeyChat.Models
{
    public class TodoItem : EntityData
    {
        public string Name { get; set; }

        public bool Done { get; set; }
    }
}