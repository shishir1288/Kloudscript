using Kloudscript.Quiz.Url.Shortening.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Kloudscript.Quiz.Url.Shortening.Context
{
    public class UrlShortenerContext : DbContext, IUrlShortenerContext
    {
        public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options) : base(options)
        {

        }
        public DbSet<ShortUrl> ShortUrls { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
