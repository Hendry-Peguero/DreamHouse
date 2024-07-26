using DreamHouse.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DreamHouse.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        // Principal
        public DbSet<PropertyEntity> Properties { get; set; }
        public DbSet<ImprovementEntity> Improvements { get; set; }
        public DbSet<PropertyTypeEntity> PropertyTypes { get; set; }
        public DbSet<SaleTypeEntity> SaleTypes { get; set; }

        // Intermediates
        public DbSet<PropertyFavoriteEntity> PropertyFavorites { get; set; }
        public DbSet<PropertyImageEntity> PropertyImages { get; set; }
        public DbSet<PropertyImprovementEntity> PropertyImprovements { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            #region Primary Keys

            // Principal
            modelBuilder.Entity<PropertyEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<ImprovementEntity>().HasKey(i => i.Id);
            modelBuilder.Entity<PropertyTypeEntity>().HasKey(pt => pt.Id);
            modelBuilder.Entity<SaleTypeEntity>().HasKey(ts => ts.Id);

            // Intermediates
            modelBuilder.Entity<PropertyFavoriteEntity>().HasKey(fp => fp.Id);
            modelBuilder.Entity<PropertyImageEntity>().HasKey(ip => ip.Id);
            modelBuilder.Entity<PropertyImprovementEntity>().HasKey(ip => ip.Id);

            #endregion

            #region Tables

            // Principal
            modelBuilder.Entity<PropertyEntity>().ToTable("Properties");
            modelBuilder.Entity<ImprovementEntity>().ToTable("Improvements");
            modelBuilder.Entity<PropertyTypeEntity>().ToTable("PropertyTypes");
            modelBuilder.Entity<SaleTypeEntity>().ToTable("SaleTypes");

            // Intermediates
            modelBuilder.Entity<PropertyFavoriteEntity>().ToTable("PropertyFavorites");
            modelBuilder.Entity<PropertyImageEntity>().ToTable("PropertyImages");
            modelBuilder.Entity<PropertyImprovementEntity>().ToTable("PropertyImprovements");

            #endregion

            #region Relationships

            //PropertyEntity y TypePropertyEntity
            modelBuilder.Entity<PropertyEntity>()
                .HasOne(p => p.TypeProperty)
                .WithMany(tp => tp.Properties)
                .HasForeignKey(p => p.TypePropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            //PropertyEntity y TypeSaleEntity
            modelBuilder.Entity<PropertyEntity>()
                .HasOne(p => p.TypeSale)
                .WithMany(ts => ts.Properties)
                .HasForeignKey(p => p.TypeSaleId)
                .OnDelete(DeleteBehavior.Cascade);

            //PropertyEntity y ImagePropertyEntity
            modelBuilder.Entity<PropertyEntity>()
                .HasMany(p => p.Images)
                .WithOne(i => i.Property)
                .HasForeignKey(i => i.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            //PropertyEntity y FavoritePropertyEntity
            modelBuilder.Entity<PropertyEntity>()
                .HasMany(p => p.Favorites)
                .WithOne(f => f.Property)
                .HasForeignKey(f => f.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            //PropertyEntity y ImprovementPropertyEntity
            modelBuilder.Entity<PropertyEntity>()
                .HasMany(p => p.ImprovementProperties)
                .WithOne(ip => ip.Property)
                .HasForeignKey(ip => ip.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            //ImprovementEntity y ImprovementPropertyEntity
            modelBuilder.Entity<ImprovementEntity>()
                .HasMany(i => i.ImprovementProperties)
                .WithOne(ip => ip.Improvement)
                .HasForeignKey(ip => ip.ImprovementId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
