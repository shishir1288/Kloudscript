using Kloudscript.Quiz.Url.Shortening.Constants;
using Kloudscript.Quiz.Url.Shortening.Models;
using Kloudscript.Quiz.Url.Shortening.Utilities;
using Microsoft.Extensions.Options;
using System;

namespace Kloudscript.Quiz.Url.Shortening.Test
{
    public class URLShorteningControllerTestData
    {
        private readonly IOptions<ApplicationSettings> _applicationSettings = Options.Create(new ApplicationSettings()
        {
            ServiceUrl = "https://localhost:44391", 
        });
        public const string ConnString = "server=.\\SQLEXPRESS; database=KloudscriptDb;Trusted_Connection=True;";
        public const string OriginalUrl = "https://gotos.in/you+have+to+write+a+really+really+long+url+to+get+to+650+characters.+like+seriously+you+have+no+idea+how+long+it+has+to+be+650+characters+is+absolutely+freaking+enormous.+You+can+fit+sooooooooooooooooooooooooooooooooo+much+data+into+650+characters.+My+hands+are+getting+tired+typing+this+many+characters.+I+didnt+even+realise+how+long+it+was+going+to+take+to+type+them+all.+So+many+characters.+I'm+bored+now+so+I'll+just+copy+and+paste.+I'm+bored+now+so+I'll+just+copy+and+paste+fL0uJ+.I'm+bored+now+so+I'll+just+copy+and+paste.I'm+bored+now+so+I'll+just+copy+and+paste.I'm+bored+now+so+I'll+just+copy+and+paste.+It+has+to+be+freaking+enormously+freaking+enormous";
        public const string RedirectUrl = "https://kloudscript.com/solutions/";
        public ShortUrl GetMockShortUrlData()
        {
            var shortenedUrl = new ShortUrl
            {
                CreatedAt = DateTime.UtcNow,
                OriginalUrl = OriginalUrl,
                ShortCode = Defaults.DefaultShortCode,
                TinyUrl = $"{_applicationSettings?.Value?.ServiceUrl}/{Defaults.DefaultShortCode}"
            };

            return shortenedUrl;
        }
    
    }
}
