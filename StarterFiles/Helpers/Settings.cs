using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MonkeyChat.Helpers
{
    public class Settings : INotifyPropertyChanged
    {
        private static ISettings AppSettings
        {
            get => CrossSettings.Current;
        }

        private static Lazy<Settings> _lazy = new Lazy<Settings>(() => new Settings());
        public static Settings Current
        {
            get => _lazy.Value;
        }

        private Settings() { }

        public string EmailAddress
        {
            get => GetProperty();
            set => SetProperty(value);
        }

        public string DisplayName
        {
            get => GetProperty();
            set => SetProperty(value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string GetProperty(string defaultValue = null, [CallerMemberName]string propertyName = null) =>
            AppSettings.GetValueOrDefault(propertyName, defaultValue);

        private bool SetProperty(string value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (AppSettings.AddOrUpdateValue(propertyName, value))
            {
                onChanged?.Invoke();
                RaisePropertyChanged(propertyName);
                return true;
            }

            return false;
        }

        private void RaisePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}