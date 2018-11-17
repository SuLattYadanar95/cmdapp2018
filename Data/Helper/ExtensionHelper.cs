using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data.Helper
{
    public static class ExtensionHelper
    {
        public static string getCleanedNumber(this string phone)
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            return digitsOnly.Replace(phone, "");
        }
        public static DateTime getLocalTime(this DateTime utc)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utc, TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time"));
        }
        public static string getCode(this string varstring, int newID, string zeroformat, string suffix = null)
        {

            string value = newID.ToString();
            for (int i = value.Length; i < zeroformat.Length; i++)
            {
                value = string.Concat("0", value);
            }
            return string.Concat(varstring, value, suffix);
        }
        public static string RemoveTail(this string value)
        {
            return value.Trim(new Char[] { ' ', '.' });

        }
        public static List<string> RemoveTails(this List<string> values)
        {
            List<string> results = new List<string>();
            foreach (var value in values)
            {
                results.Add(value.Trim(new Char[] { ' ', '.' }));
            }
            return results;

        }

        public static PageCountInfo GetPageCountInfo(this int value, int pagesize)
        {
            return new PageCountInfo(value, pagesize);
        }
        public static int GetPageCount(this int value, int pagesize)
        {
            var mod = value % pagesize;
            return (value / pagesize) + (mod == 0 ? 0 : 1);
        }
        public static string RemoveWhitespace(this string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }


    }
    public class FuzzySearch
    {

        private string _searchTerm;
        private string[] _searchTerms;
        private Regex _searchPattern;

        public FuzzySearch(string searchTerm)
        {
            _searchTerm = searchTerm;
            _searchTerms = searchTerm.Split(new Char[] { ' ' });
            _searchPattern = new Regex(
                "(?i)(?=.*" + String.Join(")(?=.*", _searchTerms) + ")");
        }

        public bool IsMatch(string value)
        {
            // do the cheap stuff first
            if (_searchTerm == value) return true;
            if (value.Contains(_searchTerm)) return true;
            // if the above don't return true, then do the more expensive stuff
            if (_searchPattern.IsMatch(value)) return true;
            // etc.
            return false;
        }
        
    }
}
