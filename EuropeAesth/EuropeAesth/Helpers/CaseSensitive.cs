using System;
using System.Collections.Generic;
using System.Text;

namespace EuropeAesth.Helpers
{
    public static class CaseSensitive
    {
        public static string ToLowerWithUtf(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return "";

            return str.ToLower(System.Globalization.CultureInfo.GetCultureInfo("TR-tr"));
        }
    }
}
