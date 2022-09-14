using Kloudscript.Quiz.Url.Shortening.Constants;
using Kloudscript.Quiz.Url.Shortening.Context;
using Kloudscript.Quiz.Url.Shortening.Controllers;
using Kloudscript.Quiz.Url.Shortening.Models;
using Kloudscript.Quiz.Url.Shortening.Services;
using Kloudscript.Quiz.Url.Shortening.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Kloudscript.Quiz.Url.Shortening.Test
{
    [TestFixture]
    public class URLShorteningTest
    {
        #region private members
        private static UrlShortenerContext _urlShortenerContext;
        private static ShortUrl _shortUrl;
        private readonly IOptions<ApplicationSettings> _applicationSettings = Options.Create(new ApplicationSettings()
        {
            ConnectionStrings = new Connectionstrings() { DefaultConnection = URLShorteningControllerTestData.ConnString },
            ServiceUrl = "https://localhost:44391",
            ShortCodeLength = 17,
            IsShortCodeAvailable = true,
            IsOrignialUrlSetToNull=false
        });
        private static IShortUrlService _shortUrlService;
        private static URLShorteningController _urlShorteningController;
        private static URLShorteningControllerTestData _uRLShorteningControllerTestData;

        #endregion


        [SetUp]
        public void Setup()
        {
            _uRLShorteningControllerTestData = new URLShorteningControllerTestData();
            var options = new DbContextOptionsBuilder<UrlShortenerContext>()
            .UseSqlServer(_applicationSettings?.Value.ConnectionStrings.DefaultConnection)
            .Options;

            _urlShortenerContext = new UrlShortenerContext(options);
            _shortUrlService = new ShortUrlService(_urlShortenerContext, _applicationSettings);
            _shortUrl = _uRLShorteningControllerTestData.GetMockShortUrlData();
            _urlShorteningController = new URLShorteningController(_shortUrlService);
        }

        [Test]
        public void CreateShortUrl_Return_Status_200()
        { 
            var result = _urlShorteningController.CreateShortUrlAsync(URLShorteningControllerTestData.OriginalUrl).Result;
            var createdResult = (result as ObjectResult)?.StatusCode;

            // Assert 
            Assert.That(createdResult, Is.Not.Null);
            Assert.That(createdResult, Is.EqualTo(201));
        }

        [TestCase("")]
        [TestCase(null)]
        public void CreateShortUrl_If_NoUrl_HasBeenEntered_Return_StatusCode_400(string originalUrl)
        {
            var result = _urlShorteningController.CreateShortUrlAsync(originalUrl);
            var badRequest = (result.Result as BadRequestObjectResult);

            // Assert 
            Assert.That(badRequest, Is.Not.Null); 
            Assert.That(badRequest.StatusCode, Is.EqualTo(400));
        }

        [TestCase("ww.nocontent")]
        [TestCase("www")]
        public void CreateShortUrl_If_InvalidUrl_HasBeenNotEntered_Return_StatusCode_400(string originalUrl)
        {
            var result = _urlShorteningController.CreateShortUrlAsync(originalUrl);
            var invalidUrl = (result.Result as BadRequestObjectResult);

            // Assert 
            Assert.That(invalidUrl, Is.Not.Null);
            Assert.That(invalidUrl.StatusCode, Is.EqualTo(400));
            Assert.That(invalidUrl.Value, Is.EqualTo(Messages.NotValidUrl));
        }

        [Test]
        public void GetResolvedUrl_Return_Status_200()
        {
            var result = _urlShorteningController.GetResolvedUrlAsync(_shortUrl.TinyUrl);
            var okResult = (result.Result as OkObjectResult);

            // Assert 
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetResolvedUrl_If_NoUrl_HasBeenEntered_Return_Status_400(string shortUrl)
        {
            var result = _urlShorteningController.GetResolvedUrlAsync(shortUrl);
            var badRequest = (result.Result as BadRequestObjectResult);

            // Assert 
            Assert.That(badRequest, Is.Not.Null);
            Assert.That(badRequest.StatusCode, Is.EqualTo(400));
        }

        [TestCase("ww.nocontent")]
        [TestCase("www")]
        public void GetResolvedUrl_If_InvalidUrl_HasBeenNotEntered_Return_StatusCode_400(string shortUrl)
        {
            var result = _urlShorteningController.GetResolvedUrlAsync(shortUrl);
            var invalidUrl = (result.Result as BadRequestObjectResult);

            // Assert 
            Assert.That(invalidUrl, Is.Not.Null);
            Assert.That(invalidUrl.StatusCode, Is.EqualTo(400));
            Assert.That(invalidUrl.Value, Is.EqualTo(Messages.NotValidUrl));
        }

        [Test]
        public void GetResolvedUrl_If_ShortUrlIsValid_ButNoOriginalUrlFound_Return_StatusCode_404()
        {
            _applicationSettings.Value.IsOrignialUrlSetToNull = true;
            var result = _urlShorteningController.GetResolvedUrlAsync(_shortUrl.TinyUrl);
            var notFound = (result.Result as NotFoundObjectResult);

            // Assert 
            Assert.That(notFound, Is.Not.Null);
            Assert.That(notFound.StatusCode, Is.EqualTo(404));
        }

        [TestCase("")]
        [TestCase(null)]
        public void GetForwardFullUrl_If_NoUrl_HasBeenEntered_Return_Status_400(string redirectUrl)
        {
            var result = _urlShorteningController.GetForwardFullUrlAsync(redirectUrl);
            var badRequest = (result as BadRequestObjectResult);

            // Assert 
            Assert.That(badRequest, Is.Not.Null);
            Assert.That(badRequest.StatusCode, Is.EqualTo(400));
        }

        [TestCase("ww.nocontent")]
        [TestCase("www")]
        public void GetForwardFullUrl_If_InvalidUrl_HasBeenNotEntered_Return_StatusCode_400(string redirectUrl)
        {
            var result = _urlShorteningController.GetForwardFullUrlAsync(redirectUrl);
            var invalidUrl = (result as BadRequestObjectResult);

            // Assert 
            Assert.That(invalidUrl, Is.Not.Null);
            Assert.That(invalidUrl.StatusCode, Is.EqualTo(400));
            Assert.That(invalidUrl.Value, Is.EqualTo(Messages.NotValidUrl));
        }

        [Test]
        public void GetForwardFullUrl_Return_Status_200()
        {
            var result = _urlShorteningController.GetForwardFullUrlAsync(URLShorteningControllerTestData.RedirectUrl);
            var redirectResult = (result as RedirectResult);
            int statusCode = 0;
            if (redirectResult.Url != null)
            {
                statusCode = 302;
            }
            // Assert 
            Assert.That(redirectResult, Is.Not.Null);
            Assert.That(statusCode, Is.EqualTo(302));
        }
    }
}