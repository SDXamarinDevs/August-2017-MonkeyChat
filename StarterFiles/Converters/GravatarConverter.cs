using System;
using System.Globalization;
using MonkeyChat.Helpers;
using Xamarin.Forms;

namespace MonkeyChat.Converters
{
    [ValueConversion(typeof(string), typeof(Uri))]
    public class GravatarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            Gravatar.GetUri($"{value}", defaultImage: DefaultImage.MonsterId);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}