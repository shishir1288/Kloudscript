using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Kloudscript.Quiz.ZipCode.Cache.Utilities
{
    [ExcludeFromCodeCoverage]
    public static class Helper
    {
        private const string _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
        private const string _caZipRegEx = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";

        /// <summary>
        /// Validation for US or Canadian zip code
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public static bool IsUSOrCanadianZipCode(string zipCode)
        {
            var validZipCode = true;
            if ((!Regex.Match(zipCode, _usZipRegEx).Success) && (!Regex.Match(zipCode, _caZipRegEx).Success))
            {
                validZipCode = false;
            }
            return validZipCode;
        }
    }
}
