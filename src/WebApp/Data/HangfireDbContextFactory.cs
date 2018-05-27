using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WebApp.Data
{
    public class HangfireDbContextFactory : IDesignTimeDbContextFactory<HangfireDbContext>
    {
        public HangfireDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HangfireDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=hangfiredb");

            return new HangfireDbContext(optionsBuilder.Options);
        }
    }
}