using System.Text.RegularExpressions;

namespace Kloudscript.Quiz.Url.Shortening.Utilities
{
    public static class Helper
    {
        /// <summary>
        /// Matching the input with regex if valid url or not
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsUrlValid(string url)
        {
            //skipping localhost url's
            if(url.ToLower().Contains("localhost"))
                return true;

            string pattern = @"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$";
            Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(url);
        }
    }
}
