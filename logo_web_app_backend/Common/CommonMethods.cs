using System.Text;

namespace logo_web_app_backend.Common
{
    public static class CommonMethods
    {
        public static string Key = "Zd1awxZ0";

        public static string ConvertoEncrypt(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";

            password += Key;

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(passwordBytes);

        }

        public static string ConvertoDecrypt(string base64EncodeData)
        {
            if (string.IsNullOrEmpty(base64EncodeData)) return "";

            var base64EncodeBytes = Convert.FromBase64String(base64EncodeData);
            var result = Encoding.UTF8.GetString(base64EncodeBytes);
            result = result.Substring(0, result.Length - Key.Length);
            return result;

        }

    }
}
