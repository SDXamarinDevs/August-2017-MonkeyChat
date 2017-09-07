using System;
using System.Globalization;
using System.Linq;
using MonkeyChat.Data;
using MonkeyChat.Helpers;
using Xamarin.Forms;

namespace MonkeyChat.Converters
{
    [ValueConversion(typeof(string), typeof(ImageSource))]
    public class GravatarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                while(UserHelper.IsLoading)
                {
                    // we have to wait for the UserHelper to finish....
                }

                var user = UserHelper.Users.FirstOrDefault(u => u.Id == $"{value}");

                if(user == null)
                    return null;

                return ImageSource.FromUri(Gravatar.GetUri(user.Email, defaultImage: DefaultImage.MonsterId));
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}