using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace Manager
{
  public static  class StringHelpers
    {
        public static string ToSeoUrl(this string url)
        {
            // make the url lowercase
            string encodedUrl = (url ?? "").ToLower();

            // replace & with and
            encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");

            // remove characters
            encodedUrl = encodedUrl.Replace("'", "");

            // remove invalid characters
            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");

            // remove duplicates
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            // trim leading & trailing characters
            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }

        public static string UppercaseFirst(this string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string RevertSeoUrl(this string url)
        {
            // make the url upercase
            string encodedUrl = (url ?? "").ToUpper();

            // remove characters
            encodedUrl = encodedUrl.Replace("-", " ");


            // trim leading & trailing characters
            //encodedUrl = encodedUrl.Trim(' ');

            return encodedUrl;
        }
        public static string ToTitle(this string url, string s)
        {
           // string id = "";

            string catname = s.RevertSeoUrl();

            int indexofSpace = catname.LastIndexOf(" ");
            if (indexofSpace > 0)
            {
                catname = catname.Substring(0, indexofSpace);

            }
           // int.TryParse(abc, out id);

            return catname;
        }
        public static int ToId(this string url, string s)
        {
            int id = 0;
            string encodedUrl = (url ?? "").ToLower();

            int lastInexofDash = encodedUrl.LastIndexOf("-", System.StringComparison.OrdinalIgnoreCase);
            string abc = encodedUrl.Substring(lastInexofDash + 1);
            int.TryParse(abc, out id);

            return id;
        }
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            if (string.IsNullOrEmpty(toCheck) || string.IsNullOrEmpty(source))
                return true;

            return source.IndexOf(toCheck, comp) >= 0;
        } 
    }
}
