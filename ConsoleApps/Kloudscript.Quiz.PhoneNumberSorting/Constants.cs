using System.Diagnostics.CodeAnalysis;

namespace Kloudscript.Quiz.PhoneNumberSorting
{

    [ExcludeFromCodeCoverage]
    internal class Constants
    {
        public const string BasePath = @"C:\temp\files\";
        public const string FileName = "PhoneNumbers-8-digits.csv";
        public const string SortedFileName = "Sorted-PhoneNumbers-8-digits.csv";

        public const string FilePath = BasePath + FileName;
        public const string SortedFilePath = BasePath + SortedFileName;

    }
}
