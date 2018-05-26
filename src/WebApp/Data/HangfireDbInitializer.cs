using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Data
{
    public static class HangfireDbInitializer
    {
        public static async Task InitializeAsync(DbContextOptions<HangfireDbContext> options)
        {
            var hangfireDbContext = new HangfireDbContext(options);
            await hangfireDbContext.Database.EnsureCreatedAsync().ConfigureAwait(false);
        }
    }
}