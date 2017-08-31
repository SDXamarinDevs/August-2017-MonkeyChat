using System;
using System.ComponentModel;
using System.Net;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;


namespace MonkeyChat.Helpers
{
    /// <summary>
    /// Globally Recognized Avatar - http://gravatar.com
    /// </summary>
    public static class Gravatar
    {
        public static Uri GetUri(string emailAddress, int size = 80, DefaultImage defaultImage = DefaultImage.Default,
                                        string defaultImageUrl = "", Rating rating = Rating.PG)
        {
            emailAddress = string.IsNullOrEmpty(emailAddress) ? string.Empty : emailAddress.Trim().ToLower();
            defaultImageUrl = (!string.IsNullOrEmpty(defaultImageUrl) ? WebUtility.UrlEncode(defaultImageUrl) : defaultImage.GetDescription());

            return new Uri($"https://secure.gravatar.com/avatar/{GetMd5Hash(emailAddress)}?s={size}&r={rating.GetDescription()}&d={defaultImageUrl}");
        }

        /// <summary>
        /// Generates an MD5 hash of the given string
        /// </summary>
        /// <remarks>Source: http://msdn.microsoft.com/en-us/library/system.security.cryptography.md5.aspx </remarks>
        private static string GetMd5Hash(string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for(int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// Returns the value of a DescriptionAttribute for a given Enum value
        /// </summary>
        /// <remarks>Source: http://blogs.msdn.com/b/abhinaba/archive/2005/10/21/483337.aspx </remarks>
        /// <param name="en"></param>
        /// <returns></returns>
        private static string GetDescription(this Enum en)
        {

            MemberInfo memInfo = en.GetType().GetTypeInfo()
                                   .DeclaredMembers
                                   .FirstOrDefault(member => member.Name == en.ToString());

            var attr = memInfo.GetCustomAttribute<DescriptionAttribute>(false);
            return attr.Description ?? en.ToString();
        }
    }

    /// <summary>
    /// In addition to allowing you to use your own image, Gravatar has a number of built in options which you can also use as defaults. Most of these work by taking the requested email hash and using it to generate a themed image that is unique to that email address
    /// </summary>
    public enum DefaultImage
    {
        /// <summary>Default Gravatar logo</summary>
        [Description("")]
        Default,
        /// <summary>404 - do not load any image if none is associated with the email hash, instead return an HTTP 404 (File Not Found) response</summary>
        [Description("404")]
        Http404,
        /// <summary>Mystery-Man - a simple, cartoon-style silhouetted outline of a person (does not vary by email hash)</summary>
        [Description("mm")]
        MysteryMan,
        /// <summary>Identicon - a geometric pattern based on an email hash</summary>
        [Description("identicon")]
        Identicon,
        /// <summary>MonsterId - a generated 'monster' with different colors, faces, etc</summary>
        [Description("monsterid")]
        MonsterId,
        /// <summary>Wavatar - generated faces with differing features and backgrounds</summary>
        [Description("wavatar")]
        Wavatar,
        /// <summary>Retro - awesome generated, 8-bit arcade-style pixelated faces</summary>
        [Description("retro")]
        Retro
    }

    /// <summary>
    /// Gravatar allows users to self-rate their images so that they can indicate if an image is appropriate for a certain audience. By default, only 'G' rated images are displayed unless you indicate that you would like to see higher ratings
    /// </summary>
    public enum Rating
    {
        /// <summary>Suitable for display on all websites with any audience type</summary>
        [Description("g")]
        G,
        /// <summary>May contain rude gestures, provocatively dressed individuals, the lesser swear words, or mild violence</summary>
        [Description("pg")]
        PG,
        /// <summary>May contain such things as harsh profanity, intense violence, nudity, or hard drug use</summary>
        [Description("r")]
        R,
        /// <summary>May contain hardcore sexual imagery or extremely disturbing violence</summary>
        [Description("x")]
        X
    }
}