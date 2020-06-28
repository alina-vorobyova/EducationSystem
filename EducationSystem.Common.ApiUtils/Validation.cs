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
            var regex = new Regex(@"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$");
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

        public static bool IsValidPhone(string phone)
        {
            var regex = new Regex(@"^([+]?[\s0-9]+)?(\d{3}|[(]?[0-9]+[)])?([-]?[\s]?[0-9])+$");
            return regex.IsMatch(phone);
        }
    }
}
