using System.Diagnostics.CodeAnalysis;

namespace Kloudscript.Quiz.PhoneNumberSorting
{

    [ExcludeFromCodeCoverage]
    internal class Program
    {
        static void Main()
        {
            //for benchmark testing...
            //var results = BenchmarkRunner.Run<PhoneNumber>();

            PhoneNumber phoneNumber = new PhoneNumber();
            phoneNumber.SortPhoneNumbers();
            Console.ReadLine();
        }
    }
}