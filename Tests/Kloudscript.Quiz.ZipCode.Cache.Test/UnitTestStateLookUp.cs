using Kloudscript.Quiz.ZipCode.Cache.Constants;
using Kloudscript.Quiz.ZipCode.Cache.Controllers;
using Kloudscript.Quiz.ZipCode.Cache.Services;
using Kloudscript.Quiz.ZipCode.Cache.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Kloudscript.Quiz.ZipCode.Cache.Test
{
    [TestFixture]
    public class StateLookUpTests
    {
        #region private members 
        private static StateLookUpController _stateLookUpController; 
        private static IUSPSService _iUSPSService;
        private readonly IOptions<Applicationsettings> _applicationSettings = Options.Create(new Applicationsettings() { UserId = "549DEMO03177",
            Url = "https://secure.shippingapis.com/ShippingAPI.dll"
        });
        #endregion

        [SetUp]
        public void Setup()
        { 
            _iUSPSService = new USPSService(_applicationSettings);
            _stateLookUpController = new StateLookUpController(_iUSPSService);
        }

        [TestCase("90210")]
        public void GetStateByZipCode_Return_StatusCode_200(string zipCode)
        { 
            var result = _stateLookUpController.GetStateByZipCode(zipCode);

            var okResult = (result.Result as OkObjectResult);

            // Assert 
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetStateByZipCode_Return_StatusCode_400(string zipCode)
        {
            var result = _stateLookUpController.GetStateByZipCode(zipCode).Result;
            var badRequest = ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode;
            // Assert  
            Assert.That(badRequest, Is.EqualTo(400)); 
        }

        [TestCase("9021066")] 
        public void InValid_ZipCode_Return_StatusCode_400(string zipCode)
        {
            var result = _stateLookUpController.GetStateByZipCode(zipCode);

            var badResult = (result.Result as BadRequestObjectResult);

            // Assert 
            Assert.That(badResult, Is.Not.Null);
            Assert.That(badResult.StatusCode, Is.EqualTo(400));
            Assert.That(badResult.Value, Is.EqualTo(Messages.NotValidZipCode));
        }
    }
}