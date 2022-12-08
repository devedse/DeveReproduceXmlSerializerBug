using System;
using System.Collections.Generic;
using System.Text;

namespace DeveReproduceXmlSerializerBug.OtherExtensions
{
    public static class StringExtensions
    {
        public static string TrimStartOnce(this string str, string trimString, StringComparison options = default)
        {
            if (str.StartsWith(trimString, options))
            {
                return str.Substring(trimString.Length);
            }
            return str;
        }

        public static string TrimStart(this string str, string trimString, StringComparison options = default)
        {
            while (str.StartsWith(trimString, options))
            {
                str = str.Substring(trimString.Length);
            }
            return str;
        }

        public static string TrimEndOnce(this string str, string trimString, StringComparison options = default)
        {
            if (str.EndsWith(trimString, options))
            {
                return str.Substring(0, str.Length - trimString.Length);
            }
            return str;
        }

        public static string TrimEnd(this string str, string trimString, StringComparison options = default)
        {
            while (str.EndsWith(trimString, options))
            {
                str = str.Substring(0, str.Length - trimString.Length);
            }
            return str;
        }
    }
}
