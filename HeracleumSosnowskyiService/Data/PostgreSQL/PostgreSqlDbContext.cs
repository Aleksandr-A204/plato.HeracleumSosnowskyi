using HeracleumSosnowskyiService.Extensions;
using HeracleumSosnowskyiService.Models;
using Microsoft.EntityFrameworkCore;

namespace HeracleumSosnowskyiService.Data.PostgreSQL
{
    public class PostgreSQLDbContext : DbContext
    {
        public PostgreSQLDbContext(DbContextOptions<PostgreSQLDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<FileInfoApi> FileInfo { get; set; }
        public DbSet<Datasets> Datasets { get; set; }
        public DbSet<SatelliteDataOfSpacesystem> SatelliteData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                foreach (var ulidProperty in entityType.ClrType.GetProperties().Where(x => x.PropertyType == typeof(Ulid) || x.PropertyType == typeof(Ulid?)))
                    if (ulidProperty.PropertyType == typeof(Ulid))
                        modelBuilder.Entity(entityType.ClrType)
                            .Property<Ulid>(ulidProperty.Name)
                            .HasConversion<UlidToBytesConverter>();
                    else
                        modelBuilder.Entity(entityType.ClrType)
                            .Property<Ulid?>(ulidProperty.Name)
                            .HasConversion<UlidToBytesConverter>();
        }
    }
}
