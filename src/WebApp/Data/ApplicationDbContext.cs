using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebApp.Models.DataFiles;
using WebApp.Models.TradePoints;

namespace WebApp.Data
{
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    public class ApplicationDbContext : DbContext
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<DataFile> DataFiles { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<TradePoint> TradePoints { get; set; }
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<TradePointSource> TradePointSources { get; set; }
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public DbSet<TradePointType> TradePointTypes { get; set; }


        // ReSharper disable once NotNullMemberIsNotInitialized
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //todo https://github.com/aspnet/EntityFrameworkCore/issues/1100
        public async Task<TradePoint[]> GetNearestAsync(Location location, double radius)
        {
            var sql = $"SELECT * FROM {GetTableName(typeof(TradePoint))} " +
                      $"WHERE Location.STDistance({ToPoint(location)}) <= {radius.ToString(CultureInfo.InvariantCulture)}";

            return await TradePoints.FromSql(sql).Include(tp => tp.Type).ToArrayAsync().ConfigureAwait(false);
        }

        //todo https://github.com/aspnet/EntityFrameworkCore/issues/1100
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            var updateCommands = CreateLocationUpdateSqlCommands();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken)
                .ConfigureAwait(false);

            foreach (var command in updateCommands)
                await Database.ExecuteSqlCommandAsync(command, cancellationToken).ConfigureAwait(false);

            return result;
        }

        //todo https://github.com/aspnet/EntityFrameworkCore/issues/1100
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var updateCommands = CreateLocationUpdateSqlCommands();
            var result = base.SaveChanges(acceptAllChangesOnSuccess);

            foreach (var command in updateCommands)
                Database.ExecuteSqlCommand(command);

            return result;
        }

        protected override void OnModelCreating([NotNull] ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataFile>().HasKey(x => x.Id);
            modelBuilder.Entity<DataFile>().HasIndex(x => x.Hash).IsUnique();
            modelBuilder.Entity<DataFile>().Property(x => x.Content).IsRequired();
            modelBuilder.Entity<DataFile>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<TradePoint>().HasKey(x => x.Id);
            modelBuilder.Entity<TradePoint>().OwnsOne(x => x.Location, builder =>
            {
                builder.Property(p => p.Latitude).IsRequired().HasColumnName(nameof(Location.Latitude));
                builder.Property(p => p.Longitude).IsRequired().HasColumnName(nameof(Location.Longitude));
            });

            modelBuilder.Entity<TradePointType>().HasKey(x => x.Id);
            modelBuilder.Entity<TradePointType>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<TradePointSource>().HasKey(x => new { x.TradePointSourceId, x.DataFileType });
        }

        private string[] CreateLocationUpdateSqlCommands()
        {
            var entries = GetLocationChangedEntities();
            return (from entry in entries
                let tradePoint = entry.Entity
                select $"UPDATE {GetTableName(entry.Metadata.ClrType)} " +
                       $"SET Location = {ToPoint(tradePoint.Location)} " +
                       $"WHERE(Id = '{tradePoint.Id}')").ToArray();
        }

        private EntityEntry<TradePoint>[] GetLocationChangedEntities()
        {
            var changedEntities = ChangeTracker.Entries<TradePoint>().Where(entry =>
                entry.State == EntityState.Added || entry.State == EntityState.Modified).ToArray();
            return changedEntities;
        }

        private string GetTableName(Type type)
        {
            var entityTypes = Model.GetEntityTypes();
            var entityType = entityTypes.First(t => t.ClrType == type);
            var tableNameAnnotation = entityType.GetAnnotation("Relational:TableName");
            return tableNameAnnotation.Value.ToString();
        }

        private static string ToPoint(Location location)
        {
            return "geography::STPointFromText(" +
                   "'POINT(" +
                   $"{location.Latitude.ToString(CultureInfo.InvariantCulture)} " +
                   $"{location.Longitude.ToString(CultureInfo.InvariantCulture)})'" +
                   ", 4326)";
        }
    }
}
