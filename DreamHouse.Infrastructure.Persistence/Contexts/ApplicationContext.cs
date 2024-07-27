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

            #region Seedings

            //ImprovementEntity
            modelBuilder.Entity<ImprovementEntity>().HasData(
                new ImprovementEntity { Id = 1, Name = "Renovación de Cocina", Description = "Renovación completa de la cocina incluyendo electrodomésticos" },
                new ImprovementEntity { Id = 2, Name = "Renovación de Baño", Description = "Renovación completa del baño incluyendo nuevos accesorios" },
                new ImprovementEntity { Id = 3, Name = "Ampliación de Sala", Description = "Ampliación de la sala con nuevo mobiliario" },
                new ImprovementEntity { Id = 4, Name = "Renovación de Jardín", Description = "Rediseño y renovación del jardín incluyendo nuevas plantas" },
                new ImprovementEntity { Id = 5, Name = "Pintura Exterior", Description = "Pintura de la fachada exterior de la casa" },
                new ImprovementEntity { Id = 6, Name = "Renovación de Cocina2", Description = "Renovación completa de la cocina incluyendo electrodomésticos2" },
                new ImprovementEntity { Id = 7, Name = "Renovación de Baño2", Description = "Renovación completa del baño incluyendo nuevos accesorios2" },
                new ImprovementEntity { Id = 8, Name = "Ampliación de Sala2", Description = "Ampliación de la sala con nuevo mobiliario2" },
                new ImprovementEntity { Id = 9, Name = "Renovación de Jardín2", Description = "Rediseño y renovación del jardín incluyendo nuevas plantas2" },
                new ImprovementEntity { Id = 10, Name = "Pintura Exterior2", Description = "Pintura de la fachada exterior de la casa2" }
            );

            //PropertyTypeEntity
            modelBuilder.Entity<PropertyTypeEntity>().HasData(
                new PropertyTypeEntity { Id = 1, Name = "Apartamento", Description = "Unidad de vivienda en un edificio multifamiliar" },
                new PropertyTypeEntity { Id = 2, Name = "Casa", Description = "Unidad de vivienda independiente" },
                new PropertyTypeEntity { Id = 3, Name = "Dúplex", Description = "Unidad de vivienda de dos pisos" },
                new PropertyTypeEntity { Id = 4, Name = "Terreno", Description = "Parcela de terreno sin construcciones" },
                new PropertyTypeEntity { Id = 5, Name = "Local Comercial", Description = "Espacio destinado para actividades comerciales" },
                new PropertyTypeEntity { Id = 6, Name = "Oficina", Description = "Espacio destinado para actividades de oficina" },
                new PropertyTypeEntity { Id = 7, Name = "Bodega", Description = "Espacio utilizado para almacenamiento" },
                new PropertyTypeEntity { Id = 8, Name = "Loft", Description = "Espacio de vivienda con diseño abierto y amplio" },
                new PropertyTypeEntity { Id = 9, Name = "Estudio", Description = "Unidad de vivienda de una sola habitación" },
                new PropertyTypeEntity { Id = 10, Name = "Penthouse", Description = "Unidad de vivienda en el último piso de un edificio, con características de lujo" }
            );

            //PropertyEntity
            modelBuilder.Entity<PropertyEntity>().HasData(
                new PropertyEntity { Id = 1, Code = "P001", Description = "Apartamento moderno en el centro", Price = 150000.00, SquareMeter = 70, Bedrooms = 2, Bathrooms = 1, TypePropertyId = 1, TypeSaleId = 1, AgentId = "AAAAA-vxztp-yub64-qm7fr-1298z" },
                new PropertyEntity { Id = 2, Code = "P002", Description = "Casa espaciosa en las afueras", Price = 250000.00, SquareMeter = 200, Bedrooms = 4, Bathrooms = 3, TypePropertyId = 2, TypeSaleId = 2, AgentId = "EEEEE-vxztp-yub64-qm7fr-1298z" },
                new PropertyEntity { Id = 3, Code = "P003", Description = "Dúplex con vista al mar", Price = 300000.00, SquareMeter = 150, Bedrooms = 3, Bathrooms = 2, TypePropertyId = 3, TypeSaleId = 3, AgentId = "AAAAA-vxztp-yub64-qm7fr-1298z" },
                new PropertyEntity { Id = 4, Code = "P004", Description = "Terreno amplio en zona rural", Price = 80000.00, SquareMeter = 1000, Bedrooms = 0, Bathrooms = 0, TypePropertyId = 4, TypeSaleId = 4, AgentId = "EEEEE-vxztp-yub64-qm7fr-1298z" },
                new PropertyEntity { Id = 5, Code = "P005", Description = "Local comercial en área céntrica", Price = 200000.00, SquareMeter = 120, Bedrooms = 0, Bathrooms = 1, TypePropertyId = 5, TypeSaleId = 5, AgentId = "AAAAA-vxztp-yub64-qm7fr-1298z" },
                new PropertyEntity { Id = 6, Code = "P006", Description = "Apartamento de lujo con piscina", Price = 500000.00, SquareMeter = 100, Bedrooms = 3, Bathrooms = 2, TypePropertyId = 1, TypeSaleId = 2, AgentId = "EEEEE-vxztp-yub64-qm7fr-1298z" },
                new PropertyEntity { Id = 7, Code = "P007", Description = "Casa antigua con jardín grande", Price = 180000.00, SquareMeter = 250, Bedrooms = 5, Bathrooms = 3, TypePropertyId = 2, TypeSaleId = 3, AgentId = "AAAAA-vxztp-yub64-qm7fr-1298z" },
                new PropertyEntity { Id = 8, Code = "P008", Description = "Dúplex moderno en barrio tranquilo", Price = 220000.00, SquareMeter = 130, Bedrooms = 3, Bathrooms = 2, TypePropertyId = 3, TypeSaleId = 4, AgentId = "EEEEE-vxztp-yub64-qm7fr-1298z" },
                new PropertyEntity { Id = 9, Code = "P009", Description = "Terreno urbanizable cerca del lago", Price = 120000.00, SquareMeter = 1500, Bedrooms = 0, Bathrooms = 0, TypePropertyId = 4, TypeSaleId = 5, AgentId = "AAAAA-vxztp-yub64-qm7fr-1298z" },
                new PropertyEntity { Id = 10, Code = "P010", Description = "Local comercial con amplio aparcamiento", Price = 350000.00, SquareMeter = 200, Bedrooms = 0, Bathrooms = 2, TypePropertyId = 5, TypeSaleId = 1, AgentId = "EEEEE-vxztp-yub64-qm7fr-1298z" }
            );

            //hay que ver si el usuer id ira vacio
            modelBuilder.Entity<PropertyFavoriteEntity>().HasData(
            new PropertyFavoriteEntity { Id = 1, UserId = "CCCCC-vxztp-yub64-qm7fr-1298z", PropertyId = 1 },
            new PropertyFavoriteEntity { Id = 2, UserId = "CCCCC-vxztp-yub64-qm7fr-1298z", PropertyId = 2 },
            new PropertyFavoriteEntity { Id = 3, UserId = "CCCCC-vxztp-yub64-qm7fr-1298z", PropertyId = 3 },
            new PropertyFavoriteEntity { Id = 4, UserId = "DDDDD-vxztp-yub64-qm7fr-1298z", PropertyId = 4 },
            new PropertyFavoriteEntity { Id = 5, UserId = "DDDDD-vxztp-yub64-qm7fr-1298z", PropertyId = 5 },
            new PropertyFavoriteEntity { Id = 6, UserId = "DDDDD-vxztp-yub64-qm7fr-1298z", PropertyId = 6 },
            new PropertyFavoriteEntity { Id = 7, UserId = "FFFFF-vxztp-yub64-qm7fr-1298z", PropertyId = 7 },
            new PropertyFavoriteEntity { Id = 8, UserId = "FFFFF-vxztp-yub64-qm7fr-1298z", PropertyId = 8 },
            new PropertyFavoriteEntity { Id = 9, UserId = "FFFFF-vxztp-yub64-qm7fr-1298z", PropertyId = 9 }
            );

            //PropertyImageEntity
            modelBuilder.Entity<PropertyImageEntity>().HasData(
                new PropertyImageEntity { Id = 1, ImageUrl = "https://gpvivienda.com/blog/wp-content/uploads/2023/03/ralph-ravi-kayden-mR1CIDduGLc-unsplash-1-1-1024x680.jpg", PropertyId = 1 },
                new PropertyImageEntity { Id = 2, ImageUrl = "https://gpvivienda.com/blog/wp-content/uploads/2023/03/ralph-ravi-kayden-mR1CIDduGLc-unsplash-1-1-1024x680.jpg", PropertyId = 1 },
                new PropertyImageEntity { Id = 3, ImageUrl = "https://gpvivienda.com/blog/wp-content/uploads/2023/03/ralph-ravi-kayden-mR1CIDduGLc-unsplash-1-1-1024x680.jpg", PropertyId = 2 },
                new PropertyImageEntity { Id = 4, ImageUrl = "https://gpvivienda.com/blog/wp-content/uploads/2023/03/ralph-ravi-kayden-mR1CIDduGLc-unsplash-1-1-1024x680.jpg", PropertyId = 2 },
                new PropertyImageEntity { Id = 5, ImageUrl = "https://gpvivienda.com/blog/wp-content/uploads/2023/03/ralph-ravi-kayden-mR1CIDduGLc-unsplash-1-1-1024x680.jpg", PropertyId = 3 },
                new PropertyImageEntity { Id = 6, ImageUrl = "https://i.blogs.es/c68014/casa-3d/450_1000.jpeg", PropertyId = 4 },
                new PropertyImageEntity { Id = 7, ImageUrl = "https://i.blogs.es/c68014/casa-3d/450_1000.jpeg", PropertyId = 5 },
                new PropertyImageEntity { Id = 8, ImageUrl = "https://i.blogs.es/c68014/casa-3d/450_1000.jpeg", PropertyId = 6 },
                new PropertyImageEntity { Id = 9, ImageUrl = "https://i.blogs.es/c68014/casa-3d/450_1000.jpeg", PropertyId = 7 },
                new PropertyImageEntity { Id = 10, ImageUrl = "https://i.blogs.es/c68014/casa-3d/450_1000.jpeg", PropertyId = 8 }
            );

            //PropertyImprovementEntity
            modelBuilder.Entity<PropertyImprovementEntity>().HasData(
                new PropertyImprovementEntity { Id = 1, PropertyId = 1, ImprovementId = 1 },
                new PropertyImprovementEntity { Id = 2, PropertyId = 1, ImprovementId = 2 },
                new PropertyImprovementEntity { Id = 3, PropertyId = 2, ImprovementId = 3 },
                new PropertyImprovementEntity { Id = 4, PropertyId = 2, ImprovementId = 4 },
                new PropertyImprovementEntity { Id = 5, PropertyId = 3, ImprovementId = 5 },
                new PropertyImprovementEntity { Id = 6, PropertyId = 4, ImprovementId = 1 },
                new PropertyImprovementEntity { Id = 7, PropertyId = 5, ImprovementId = 2 },
                new PropertyImprovementEntity { Id = 8, PropertyId = 6, ImprovementId = 3 },
                new PropertyImprovementEntity { Id = 9, PropertyId = 7, ImprovementId = 4 },
                new PropertyImprovementEntity { Id = 10, PropertyId = 8, ImprovementId = 5 }
            );

            //SaleTypeEntity
            modelBuilder.Entity<SaleTypeEntity>().HasData(
                new SaleTypeEntity { Id = 1, Name = "Venta", Description = "Transacción de propiedad mediante venta directa" },
                new SaleTypeEntity { Id = 2, Name = "Alquiler", Description = "Transacción de propiedad mediante alquiler o arrendamiento" },
                new SaleTypeEntity { Id = 3, Name = "Venta con opción a compra", Description = "Transacción de propiedad con opción de compra en el futuro" },
                new SaleTypeEntity { Id = 4, Name = "Intercambio", Description = "Transacción de propiedad mediante intercambio con otra propiedad" },
                new SaleTypeEntity { Id = 5, Name = "Subasta", Description = "Transacción de propiedad mediante subasta pública" }
            );
            #endregion
        }
    }
}
