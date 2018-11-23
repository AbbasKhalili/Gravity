using System.Text.RegularExpressions;

namespace Gravity.Tools.RegexTools
{
    public class RegexUtils
    {
        public static bool CheckEmailAddress(string emailaddress)
        {
            return Regex.IsMatch(emailaddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }

        public static bool CheckMobileNumber(string mobilenumber)
        {
            string pattern = @"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$)";
            return Regex.IsMatch(mobilenumber, pattern);
        }

        public static bool CheckUsername(string usernam)
        {
            string pattern = @"(^(?=.{3,32}$)(?!.*[._-]{2})(?!.*[0-9]{20,})[a-z](?:[\w]*|[a-z\d\.]*|[a-z\d-]*)[a-z0-9]$)";
            return Regex.IsMatch(usernam, pattern);
        }
    }
}
