namespace Kloudscript.Quiz.PhoneNumberSorting
{
    // [MemoryDiagnoser]
    public class PhoneNumber
    {
        private static List<long>? _values;
       

        // [Benchmark]
        public int SortPhoneNumbers()
        {
            //Reading CSV file using StreamReader
            using (StreamReader sr = new StreamReader(Constants.FilePath))
            {
                //Create Table. According to the CSV file format, I create a table 
                string s = string.Empty;
                _values = new List<long>();
                while (!sr.EndOfStream)
                {
                    s = sr.ReadLine();
                    string[] str = s.Split(',');
                    string str1 = str[0].ToString();
                    if (!str1.Equals("PhoneNumber"))
                    {
                        _values.Add(long.Parse(str1));
                    }
                }
            }

            var sortFunction = new QuickSort();
            var sortedArray = sortFunction.SortArray(_values.ToArray(), 0, _values.ToArray().Length - 1);

            if (sortedArray.Length > 0)
            {
                ProduceSortedCSV(sortedArray);
                Console.WriteLine("Sorted Phone Number csv has been generated on below path. \n");
                Console.WriteLine("Path : {0} \nFileName : {1}", @"C:\temp\files\", "Sorted-PhoneNumbers-8-digits.csv");
                //sucessfull call
                return 1;
            }

            //unsucessfull call
            return 0;
        }

        private void ProduceSortedCSV(long[] arr)
        {
            using (StreamWriter sw = new StreamWriter(Constants.SortedFilePath))
            {
                sw.WriteLine("PhoneNumber");
                for (int i = 0; i < arr.Length; i++)
                {
                    sw.WriteLine(arr[i]);
                }
            }
        }
    }
}
