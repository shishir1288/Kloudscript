namespace Kloudscript.Quiz.PhoneNumber.Test
{
    public class PhoneNumberTest
    {
        private static PhoneNumberSorting.PhoneNumber _phoneNumber; 

        [SetUp]
        public void Setup()
        {
            _phoneNumber = new PhoneNumberSorting.PhoneNumber(); 
        }

        [Test]
        public void Sort_Unsorted_PhoneNumber()
        {
            int statusCode  = _phoneNumber.SortPhoneNumbers();
            Assert.That(statusCode, Is.EqualTo(1)); 
        }
    }
}