using Kloudscript.Quiz.Url.Shortening.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Kloudscript.Quiz.Url.Shortening.Context
{
    public interface IUrlShortenerContext
    {
        DbSet<ShortUrl> ShortUrls { get; set; }

        Task<int> SaveChangesAsync();
    }
}