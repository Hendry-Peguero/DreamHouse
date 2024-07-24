using DreamHouse.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DreamHouse.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<PropertyEntity> Properties { get; set; }
        public DbSet<FavoritePropertyEntity> FavoriteProperties { get; set; }
        public DbSet<ImagePropertyEntity> ImageProperties { get; set; }
        public DbSet<ImprovementEntity> Improvements { get; set; }
        public DbSet<ImprovementPropertyEntity> ImprovementProperties { get; set; }
        public DbSet<TypePropertyEntity> PropertyTypes { get; set; }
        public DbSet<TypeSaleEntity> TypeSales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            #region Primary Keys
            modelBuilder.Entity<PropertyEntity>().HasKey(p => p.Id);
            modelBuilder.Entity<FavoritePropertyEntity>().HasKey(fp => fp.Id);
            modelBuilder.Entity<ImagePropertyEntity>().HasKey(ip => ip.Id);
            modelBuilder.Entity<ImprovementEntity>().HasKey(i => i.Id);
            modelBuilder.Entity<ImprovementPropertyEntity>().HasKey(ip => ip.Id);
            modelBuilder.Entity<TypePropertyEntity>().HasKey(pt => pt.Id);
            modelBuilder.Entity<TypeSaleEntity>().HasKey(ts => ts.Id);
            #endregion

            #region Tables
            modelBuilder.Entity<PropertyEntity>().ToTable("Properties");
            modelBuilder.Entity<FavoritePropertyEntity>().ToTable("FavoriteProperties");
            modelBuilder.Entity<ImagePropertyEntity>().ToTable("ImageProperties");
            modelBuilder.Entity<ImprovementEntity>().ToTable("Improvements");
            modelBuilder.Entity<ImprovementPropertyEntity>().ToTable("ImprovementProperties");
            modelBuilder.Entity<TypePropertyEntity>().ToTable("PropertyTypes");
            modelBuilder.Entity<TypeSaleEntity>().ToTable("TypeSales");
            #endregion

            #region Relationships

            //PropertyEntity y TypePropertyEntity
            modelBuilder.Entity<PropertyEntity>()
                .HasOne(p => p.TypeProperty)
                .WithMany(tp => tp.Properties)
                .HasForeignKey(p => p.TypePropertyId);

            //PropertyEntity y TypeSaleEntity
            modelBuilder.Entity<PropertyEntity>()
                .HasOne(p => p.TypeSale)
                .WithMany(ts => ts.Properties)
                .HasForeignKey(p => p.TypeSaleId);

            //PropertyEntity y ImagePropertyEntity
            modelBuilder.Entity<PropertyEntity>()
                .HasMany(p => p.Images)
                .WithOne(i => i.Property)
                .HasForeignKey(i => i.PropertyId);

            //PropertyEntity y FavoritePropertyEntity
            modelBuilder.Entity<PropertyEntity>()
                .HasMany(p => p.Favorites)
                .WithOne(f => f.Property)
                .HasForeignKey(f => f.PropertyId);

            //PropertyEntity y ImprovementPropertyEntity
            modelBuilder.Entity<PropertyEntity>()
                .HasMany(p => p.ImprovementProperties)
                .WithOne(ip => ip.Property)
                .HasForeignKey(ip => ip.PropertyId);

            //ImprovementEntity y ImprovementPropertyEntity
            modelBuilder.Entity<ImprovementEntity>()
                .HasMany(i => i.ImprovementProperties)
                .WithOne(ip => ip.Improvement)
                .HasForeignKey(ip => ip.ImprovementId);

            //FavoritePropertyEntity y PropertyEntity
            modelBuilder.Entity<FavoritePropertyEntity>()
                .HasOne(fp => fp.Property)
                .WithMany(p => p.Favorites)
                .HasForeignKey(fp => fp.PropertyId);

            //ImagePropertyEntity y PropertyEntity
            modelBuilder.Entity<ImagePropertyEntity>()
                .HasOne(ip => ip.Property)
                .WithMany(p => p.Images)
                .HasForeignKey(ip => ip.PropertyId);

            //ImprovementPropertyEntity y PropertyEntity
            modelBuilder.Entity<ImprovementPropertyEntity>()
                .HasOne(ip => ip.Property)
                .WithMany(p => p.ImprovementProperties)
                .HasForeignKey(ip => ip.PropertyId);

            //ImprovementPropertyEntity y ImprovementEntity
            modelBuilder.Entity<ImprovementPropertyEntity>()
                .HasOne(ip => ip.Improvement)
                .WithMany(i => i.ImprovementProperties)
                .HasForeignKey(ip => ip.ImprovementId);
            #endregion
        }
    }
}
