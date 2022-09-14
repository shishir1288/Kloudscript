using Kloudscript.Quiz.Url.Shortening.Constants;
using Kloudscript.Quiz.Url.Shortening.Context;
using Kloudscript.Quiz.Url.Shortening.Models;
using Kloudscript.Quiz.Url.Shortening.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Kloudscript.Quiz.Url.Shortening.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly UrlShortenerContext _urlShortenerContext;
        private readonly IOptions<ApplicationSettings> _applicationSettings;

        public ShortUrlService(UrlShortenerContext urlShortenerContext, 
            IOptions<ApplicationSettings> applicationSettings)
        {
             _urlShortenerContext = urlShortenerContext;
             _applicationSettings = applicationSettings;
        }

        public async Task<string> ShortenUrl(string originalUrl)
        { 
            // get shortened url collection from DB
            // and check if we have the url stored
            var shortenedUrl = await _urlShortenerContext.ShortUrls.AsQueryable().
                                               FirstOrDefaultAsync(x => x.OriginalUrl == originalUrl);

            // if the long url has not been shortened
            if (shortenedUrl == null)
            {
                //paasing lenght to generate shorten length string
                //and generate shortcode
                var shortCode = !_applicationSettings.Value.IsShortCodeAvailable ? 
                                GenerateShortCode(_applicationSettings.Value.ShortCodeLength.Value) :
                                Defaults.DefaultShortCode;

                shortenedUrl = new ShortUrl
                {
                    CreatedAt = DateTime.UtcNow,
                    OriginalUrl = originalUrl,
                    ShortCode = shortCode,
                    TinyUrl = $"{_applicationSettings?.Value?.ServiceUrl}/{shortCode}"
                };
                 
                //add to database
                _urlShortenerContext.ShortUrls.Add(shortenedUrl);
                await _urlShortenerContext.SaveChangesAsync();
            }

            return shortenedUrl.TinyUrl;
        }

        public async Task<string> GetResolveUrl(string shortCode)
        {
            // get shortened url collection from DB
            // first check if we have the url stored
            var shortenedUrl = await _urlShortenerContext.ShortUrls.AsQueryable().
                                               FirstOrDefaultAsync(x => x.ShortCode == shortCode);

            //if the long url has not been shortened
            if (shortenedUrl != null)
            {
                //for unit testing....
                if (_applicationSettings.Value.IsOrignialUrlSetToNull)
                {
                    shortenedUrl.OriginalUrl = null; 
                }
                return shortenedUrl.OriginalUrl;
            }
            return null;
        }



        /// <summary>
        /// This will produce the ShortCode
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string GenerateShortCode(int length = 0)
        {
            var newGuid = Guid.NewGuid();
            var messageID = Convert.ToBase64String(newGuid.ToByteArray());
            return messageID.Remove(messageID.Length - length);
        }
    }
}
