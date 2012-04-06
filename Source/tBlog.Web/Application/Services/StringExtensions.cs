using System.Text.RegularExpressions;
using BinaryAnalysis.UnidecodeSharp;

namespace tBlog.Web.Application.Services
{
    public static class StringExtensions
    {
        public static string ToSlug(this string source)
        {
            var result = source.Unidecode(); //Transliterating

            result = Regex.Replace(result, @"[^a-zA-Z0-9]+", "-", RegexOptions.IgnoreCase);

            string pattern = "--";
            while (Regex.IsMatch(source, pattern))
                result = Regex.Replace(result, pattern, "-", RegexOptions.IgnoreCase);

            pattern = "^-|-$";
            result = Regex.Replace(result, pattern, "", RegexOptions.IgnoreCase);

            return result.ToLower();
        }

        public static string LimitCharacters(this string text, int length)
        {
            if (text.Length <= length)
            {
                return text;
            }

            char[] delimiters = new char[] { ' ', '.', ',', ':', ';' };
            int index = text.LastIndexOfAny(delimiters, length - 3);

            if (index > (length / 2))
            {
                return text.Substring(0, index) + "...";
            }
            else
            {
                return text.Substring(0, length - 3) + "...";
            }
        }

        public static string CutEntry(this string text)
        {
            const string cutter = "{{cut}}";
            int index = text.IndexOf(cutter, System.StringComparison.Ordinal);

            if (index > 0)
            {
                return text.Substring(0, index) + "...";
            }
            else
            {
                return text;
            }
        }

        public static string RemoveCutSign(this string text)
        {
            const string cutter = "{{cut}}";
            int index = text.IndexOf(cutter, System.StringComparison.Ordinal);

            if (index > 0)
            {
                return text.Substring(0, index) + text.Substring(index + cutter.Length);
            }
            else
            {
                return text;
            }
        }
    }
}