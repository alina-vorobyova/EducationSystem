using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EducationSystem.Common.ApiUtils
{
    public static class Validation
    {
        public static bool IsValidUrl(string url)
        {
            var regex = new Regex(@"[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&\/\/=]*)");
            return regex.IsMatch(url);
        }

        public static bool IsValidName(string name)
        {
            var regex = new Regex(@"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$", RegexOptions.IgnoreCase);
            return regex.IsMatch(name);
        }

        public static bool IsValidPassport(string passport)
        {
            var regex = new Regex("^(?!^0+$)[a-zA-Z0-9]{3,20}$");
            return regex.IsMatch(passport);
        }
    }
}
