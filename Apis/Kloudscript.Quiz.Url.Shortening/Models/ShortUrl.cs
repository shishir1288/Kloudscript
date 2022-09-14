using System;

namespace Kloudscript.Quiz.Url.Shortening.Models
{
    public class ShortUrl
    {
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortCode { get; set; }
        public string TinyUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
