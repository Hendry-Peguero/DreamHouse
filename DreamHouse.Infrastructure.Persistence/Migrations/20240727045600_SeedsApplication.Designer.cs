﻿// <auto-generated />
using DreamHouse.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DreamHouse.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240727045600_SeedsApplication")]
    partial class SeedsApplication
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DreamHouse.Core.Domain.Entities.ImprovementEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Improvements", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Renovación completa de la cocina incluyendo electrodomésticos",
                            Name = "Renovación de Cocina"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Renovación completa del baño incluyendo nuevos accesorios",
                            Name = "Renovación de Baño"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Ampliación de la sala con nuevo mobiliario",
                            Name = "Ampliación de Sala"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Rediseño y renovación del jardín incluyendo nuevas plantas",
                            Name = "Renovación de Jardín"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Pintura de la fachada exterior de la casa",
                            Name = "Pintura Exterior"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Renovación completa de la cocina incluyendo electrodomésticos2",
                            Name = "Renovación de Cocina2"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Renovación completa del baño incluyendo nuevos accesorios2",
                            Name = "Renovación de Baño2"
                        },
                        new
                        {
                            Id = 8,
                            Description = "Ampliación de la sala con nuevo mobiliario2",
                            Name = "Ampliación de Sala2"
                        },
                        new
                        {
                            Id = 9,
                            Description = "Rediseño y renovación del jardín incluyendo nuevas plantas2",
                            Name = "Renovación de Jardín2"
                        },
                        new
                        {
                            Id = 10,
                            Description = "Pintura de la fachada exterior de la casa2",
                            Name = "Pintura Exterior2"
                        });
                });

            modelBuilder.Entity("DreamHouse.Core.Domain.Entities.PropertyEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AgentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Bathrooms")
                        .HasColumnType("int");

                    b.Property<int>("Bedrooms")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("SquareMeter")
                        .HasColumnType("int");

                    b.Property<int>("TypePropertyId")
                        .HasColumnType("int");

                    b.Property<int>("TypeSaleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypePropertyId");

                    b.HasIndex("TypeSaleId");

                    b.ToTable("Properties", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AgentId = "AAAAA-vxztp-yub64-qm7fr-1298z",
                            Bathrooms = 1,
                            Bedrooms = 2,
                            Code = "P001",
                            Description = "Apartamento moderno en el centro",
                            Price = 150000.0,
                            SquareMeter = 70,
                            TypePropertyId = 1,
                            TypeSaleId = 1
                        },
                        new
                        {
                            Id = 2,
                            AgentId = "EEEEE-vxztp-yub64-qm7fr-1298z",
                            Bathrooms = 3,
                            Bedrooms = 4,
                            Code = "P002",
                            Description = "Casa espaciosa en las afueras",
                            Price = 250000.0,
                            SquareMeter = 200,
                            TypePropertyId = 2,
                            TypeSaleId = 2
                        },
                        new
                        {
                            Id = 3,
                            AgentId = "AAAAA-vxztp-yub64-qm7fr-1298z",
                            Bathrooms = 2,
                            Bedrooms = 3,
                            Code = "P003",
                            Description = "Dúplex con vista al mar",
                            Price = 300000.0,
                            SquareMeter = 150,
                            TypePropertyId = 3,
                            TypeSaleId = 3
                        },
                        new
                        {
                            Id = 4,
                            AgentId = "EEEEE-vxztp-yub64-qm7fr-1298z",
                            Bathrooms = 0,
                            Bedrooms = 0,
                            Code = "P004",
                            Description = "Terreno amplio en zona rural",
                            Price = 80000.0,
                            SquareMeter = 1000,
                            TypePropertyId = 4,
                            TypeSaleId = 4
                        },
                        new
                        {
                            Id = 5,
                            AgentId = "AAAAA-vxztp-yub64-qm7fr-1298z",
                            Bathrooms = 1,
                            Bedrooms = 0,
                            Code = "P005",
                            Description = "Local comercial en área céntrica",
                            Price = 200000.0,
                            SquareMeter = 120,
                            TypePropertyId = 5,
                            TypeSaleId = 5
                        },
                        new
                        {
                            Id = 6,
                            AgentId = "EEEEE-vxztp-yub64-qm7fr-1298z",
                            Bathrooms = 2,
                            Bedrooms = 3,
                            Code = "P006",
                            Description = "Apartamento de lujo con piscina",
                            Price = 500000.0,
                            SquareMeter = 100,
                            TypePropertyId = 1,
                            TypeSaleId = 2
                        },
                        new
                        {
                            Id = 7,
                            AgentId = "AAAAA-vxztp-yub64-qm7fr-1298z",
                            Bathrooms = 3,
                            Bedrooms = 5,
                            Code = "P007",
                            Description = "Casa antigua con jardín grande",
                            Price = 180000.0,
                            SquareMeter = 250,
                            TypePropertyId = 2,
                            TypeSaleId = 3
                        },
                        new
                        {
                            Id = 8,
                            AgentId = "EEEEE-vxztp-yub64-qm7fr-1298z",
                            Bathrooms = 2,
                            Bedrooms = 3,
                            Code = "P008",
                            Description = "Dúplex moderno en barrio tranquilo",
                            Price = 220000.0,
                            SquareMeter = 130,
                            TypePropertyId = 3,
                            TypeSaleId = 4
                        },
                        new
                        {
                            Id = 9,
                            AgentId = "AAAAA-vxztp-yub64-qm7fr-1298z",
                            Bathrooms = 0,
                            Bedrooms = 0,
                            Code = "P009",
                            Description = "Terreno urbanizable cerca del lago",
                            Price = 120000.0,
                            SquareMeter = 1500,
                            TypePropertyId = 4,
                            TypeSaleId = 5
                        },
                        new
                        {
                            Id = 10,
                            AgentId = "EEEEE-vxztp-yub64-qm7fr-1298z",
                            Bathrooms = 2,
                            Bedrooms = 0,
                            Code = "P010",
                            Description = "Local comercial con amplio aparcamiento",
                            Price = 350000.0,
                            SquareMeter = 200,
                            TypePropertyId = 5,
                            TypeSaleId = 1
                        });
                });

            modelBuilder.Entity("DreamHouse.Core.Domain.Entities.PropertyFavoriteEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyFavorites", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PropertyId = 1,
                            UserId = "CCCCC-vxztp-yub64-qm7fr-1298z"
                        },
                        new
                        {
                            Id = 2,
                            PropertyId = 2,
                            UserId = "CCCCC-vxztp-yub64-qm7fr-1298z"
                        },
                        new
                        {
                            Id = 3,
                            PropertyId = 3,
                            UserId = "CCCCC-vxztp-yub64-qm7fr-1298z"
                        },
                        new
                        {
                            Id = 4,
                            PropertyId = 4,
                            UserId = "DDDDD-vxztp-yub64-qm7fr-1298z"
                        },
                        new
                        {
                            Id = 5,
                            PropertyId = 5,
                            UserId = "DDDDD-vxztp-yub64-qm7fr-1298z"
                        },
                        new
                        {
                            Id = 6,
                            PropertyId = 6,
                            UserId = "DDDDD-vxztp-yub64-qm7fr-1298z"
                        },
                        new
                        {
                            Id = 7,
                            PropertyId = 7,
                            UserId = "FFFFF-vxztp-yub64-qm7fr-1298z"
                        },
                        new
                        {
                            Id = 8,
                            PropertyId = 8,
                            UserId = "FFFFF-vxztp-yub64-qm7fr-1298z"
                        },
                        new
                        {
                            Id = 9,
                            PropertyId = 9,
                            UserId = "FFFFF-vxztp-yub64-qm7fr-1298z"
                        });
                });

            modelBuilder.Entity("DreamHouse.Core.Domain.Entities.PropertyImageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyImages", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImageUrl = "https://gpvivienda.com/blog/wp-content/uploads/2023/03/ralph-ravi-kayden-mR1CIDduGLc-unsplash-1-1-1024x680.jpg",
                            PropertyId = 1
                        },
                        new
                        {
                            Id = 2,
                            ImageUrl = "https://gpvivienda.com/blog/wp-content/uploads/2023/03/ralph-ravi-kayden-mR1CIDduGLc-unsplash-1-1-1024x680.jpg",
                            PropertyId = 1
                        },
                        new
                        {
                            Id = 3,
                            ImageUrl = "https://gpvivienda.com/blog/wp-content/uploads/2023/03/ralph-ravi-kayden-mR1CIDduGLc-unsplash-1-1-1024x680.jpg",
                            PropertyId = 2
                        },
                        new
                        {
                            Id = 4,
                            ImageUrl = "https://gpvivienda.com/blog/wp-content/uploads/2023/03/ralph-ravi-kayden-mR1CIDduGLc-unsplash-1-1-1024x680.jpg",
                            PropertyId = 2
                        },
                        new
                        {
                            Id = 5,
                            ImageUrl = "https://gpvivienda.com/blog/wp-content/uploads/2023/03/ralph-ravi-kayden-mR1CIDduGLc-unsplash-1-1-1024x680.jpg",
                            PropertyId = 3
                        },
                        new
                        {
                            Id = 6,
                            ImageUrl = "https://i.blogs.es/c68014/casa-3d/450_1000.jpeg",
                            PropertyId = 4
                        },
                        new
                        {
                            Id = 7,
                            ImageUrl = "https://i.blogs.es/c68014/casa-3d/450_1000.jpeg",
                            PropertyId = 5
                        },
                        new
                        {
                            Id = 8,
                            ImageUrl = "https://i.blogs.es/c68014/casa-3d/450_1000.jpeg",
                            PropertyId = 6
                        },
                        new
                        {
                            Id = 9,
                            ImageUrl = "https://i.blogs.es/c68014/casa-3d/450_1000.jpeg",
                            PropertyId = 7
                        },
                        new
                        {
                            Id = 10,
                            ImageUrl = "https://i.blogs.es/c68014/casa-3d/450_1000.jpeg",
                            PropertyId = 8
                        });
                });

            modelBuilder.Entity("DreamHouse.Core.Domain.Entities.PropertyImprovementEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ImprovementId")
                        .HasColumnType("int");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ImprovementId");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyImprovements", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ImprovementId = 1,
                            PropertyId = 1
                        },
                        new
                        {
                            Id = 2,
                            ImprovementId = 2,
                            PropertyId = 1
                        },
                        new
                        {
                            Id = 3,
                            ImprovementId = 3,
                            PropertyId = 2
                        },
                        new
                        {
                            Id = 4,
                            ImprovementId = 4,
                            PropertyId = 2
                        },
                        new
                        {
                            Id = 5,
                            ImprovementId = 5,
                            PropertyId = 3
                        },
                        new
                        {
                            Id = 6,
                            ImprovementId = 1,
                            PropertyId = 4
                        },
                        new
                        {
                            Id = 7,
                            ImprovementId = 2,
                            PropertyId = 5
                        },
                        new
                        {
                            Id = 8,
                            ImprovementId = 3,
                            PropertyId = 6
                        },
                        new
                        {
                            Id = 9,
                            ImprovementId = 4,
                            PropertyId = 7
                        },
                        new
                        {
                            Id = 10,
                            ImprovementId = 5,
                            PropertyId = 8
                        });
                });

            modelBuilder.Entity("DreamHouse.Core.Domain.Entities.PropertyTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PropertyTypes", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Unidad de vivienda en un edificio multifamiliar",
                            Name = "Apartamento"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Unidad de vivienda independiente",
                            Name = "Casa"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Unidad de vivienda de dos pisos",
                            Name = "Dúplex"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Parcela de terreno sin construcciones",
                            Name = "Terreno"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Espacio destinado para actividades comerciales",
                            Name = "Local Comercial"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Espacio destinado para actividades de oficina",
                            Name = "Oficina"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Espacio utilizado para almacenamiento",
                            Name = "Bodega"
                        },
                        new
                        {
                            Id = 8,
                            Description = "Espacio de vivienda con diseño abierto y amplio",
                            Name = "Loft"
                        },
                        new
                        {
                            Id = 9,
                            Description = "Unidad de vivienda de una sola habitación",
                            Name = "Estudio"
                        },
                        new
                        {
                            Id = 10,
                            Description = "Unidad de vivienda en el último piso de un edificio, con características de lujo",
                            Name = "Penthouse"
                        });
                });

            modelBuilder.Entity("DreamHouse.Core.Domain.Entities.SaleTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SaleTypes", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Transacción de propiedad mediante venta directa",
                            Name = "Venta"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Transacción de propiedad mediante alquiler o arrendamiento",
                            Name = "Alquiler"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Transacción de propiedad con opción de compra en el futuro",
                            Name = "Venta con opción a compra"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Transacción de propiedad mediante intercambio con otra propiedad",
                            Name = "Intercambio"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Transacción de propiedad mediante subasta pública",
                            Name = "Subasta"
                        });
                });

            modelBuilder.Entity("DreamHouse.Core.Domain.Entities.PropertyEntity", b =>
                {
                    b.HasOne("DreamHouse.Core.Domain.Entities.PropertyTypeEntity", "TypeProperty")
                        .WithMany("Properties")
                        .HasForeignKey("TypePropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamHouse.Core.Domain.Entities.SaleTypeEntity", "TypeSale")
                        .WithMany("Properties")
                        .HasForeignKey("TypeSaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeProperty");

                    b.Navigation("TypeSale");
                });

            modelBuilder.Entity("DreamHouse.Core.Domain.Entities.PropertyFavoriteEntity", b =>
                {
                    b.HasOne("DreamHouse.Core.Domain.Entities.PropertyEntity", "Property")
                        .WithMany("Favorites")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("DreamHouse.Core.Domain.Entities.PropertyImageEntity", b =>
                {
                    b.HasOne("DreamHouse.Core.Domain.Entities.PropertyEntity", "Property")
                        .WithMany("Images")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");
                });

            modelBuilder.Entity("DreamHouse.Core.Domain.Entities.PropertyImprovementEntity", b =>
                {
                    b.HasOne("DreamHouse.Core.Domain.Entities.ImprovementEntity", "Improvement")
                        .WithMany("ImprovementProperties")
                        .HasForeignKey("ImprovementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DreamHouse.Core.Domain.Entities.PropertyEntity", "Property")
                        .WithMany("ImprovementProperties")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Improvement");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("DreamHouse.Core.Domain.Entities.ImprovementEntity", b =>
                {
                    b.Navigation("ImprovementProperties");
                });

            modelBuilder.Entity("DreamHouse.Core.Domain.Entities.PropertyEntity", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("Images");

                    b.Navigation("ImprovementProperties");
                });

            modelBuilder.Entity("DreamHouse.Core.Domain.Entities.PropertyTypeEntity", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("DreamHouse.Core.Domain.Entities.SaleTypeEntity", b =>
                {
                    b.Navigation("Properties");
                });
#pragma warning restore 612, 618
        }
    }
}
