using System.Threading.Tasks;

namespace Kloudscript.Quiz.Url.Shortening.Services
{
    public interface IShortUrlService
    {
        Task<string> GetResolveUrl(string shortCode);
        Task<string> ShortenUrl(string originalUrl);
    }
}