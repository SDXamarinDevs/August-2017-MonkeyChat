using System;
using System.Globalization;
using System.Linq;
using MonkeyChat.Helpers;
using Xamarin.Forms;

namespace MonkeyChat.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class UserNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            while(UserHelper.IsLoading)
            {
                // we have to wait for the UserHelper to finish....
            }

            var user = UserHelper.Users.FirstOrDefault(u => u.Id == $"{value}");

            if(user == null)
                return null;

            return user.DisplayName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
