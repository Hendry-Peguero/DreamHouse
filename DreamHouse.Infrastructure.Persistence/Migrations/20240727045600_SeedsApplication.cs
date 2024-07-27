using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DreamHouse.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedsApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AgentId",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Improvements",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Renovación completa de la cocina incluyendo electrodomésticos", "Renovación de Cocina" },
                    { 2, "Renovación completa del baño incluyendo nuevos accesorios", "Renovación de Baño" },
                    { 3, "Ampliación de la sala con nuevo mobiliario", "Ampliación de Sala" },
                    { 4, "Rediseño y renovación del jardín incluyendo nuevas plantas", "Renovación de Jardín" },
                    { 5, "Pintura de la fachada exterior de la casa", "Pintura Exterior" },
                    { 6, "Renovación completa de la cocina incluyendo electrodomésticos2", "Renovación de Cocina2" },
                    { 7, "Renovación completa del baño incluyendo nuevos accesorios2", "Renovación de Baño2" },
                    { 8, "Ampliación de la sala con nuevo mobiliario2", "Ampliación de Sala2" },
                    { 9, "Rediseño y renovación del jardín incluyendo nuevas plantas2", "Renovación de Jardín2" },
                    { 10, "Pintura de la fachada exterior de la casa2", "Pintura Exterior2" }
                });

            migrationBuilder.InsertData(
                table: "PropertyTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Unidad de vivienda en un edificio multifamiliar", "Apartamento" },
                    { 2, "Unidad de vivienda independiente", "Casa" },
                    { 3, "Unidad de vivienda de dos pisos", "Dúplex" },
                    { 4, "Parcela de terreno sin construcciones", "Terreno" },
                    { 5, "Espacio destinado para actividades comerciales", "Local Comercial" },
                    { 6, "Espacio destinado para actividades de oficina", "Oficina" },
                    { 7, "Espacio utilizado para almacenamiento", "Bodega" },
                    { 8, "Espacio de vivienda con diseño abierto y amplio", "Loft" },
                    { 9, "Unidad de vivienda de una sola habitación", "Estudio" },
                    { 10, "Unidad de vivienda en el último piso de un edificio, con características de lujo", "Penthouse" }
                });

            migrationBuilder.InsertData(
                table: "SaleTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Transacción de propiedad mediante venta directa", "Venta" },
                    { 2, "Transacción de propiedad mediante alquiler o arrendamiento", "Alquiler" },
                    { 3, "Transacción de propiedad con opción de compra en el futuro", "Venta con opción a compra" },
                    { 4, "Transacción de propiedad mediante intercambio con otra propiedad", "Intercambio" },
                    { 5, "Transacción de propiedad mediante subasta pública", "Subasta" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "AgentId", "Bathrooms", "Bedrooms", "Code", "Description", "Price", "SquareMeter", "TypePropertyId", "TypeSaleId" },
                values: new object[,]
                {
                    { 1, "AAAAA-vxztp-yub64-qm7fr-1298z", 1, 2, "P001", "Apartamento moderno en el centro", 150000.0, 70, 1, 1 },
                    { 2, "EEEEE-vxztp-yub64-qm7fr-1298z", 3, 4, "P002", "Casa espaciosa en las afueras", 250000.0, 200, 2, 2 },
                    { 3, "AAAAA-vxztp-yub64-qm7fr-1298z", 2, 3, "P003", "Dúplex con vista al mar", 300000.0, 150, 3, 3 },
                    { 4, "EEEEE-vxztp-yub64-qm7fr-1298z", 0, 0, "P004", "Terreno amplio en zona rural", 80000.0, 1000, 4, 4 },
                    { 5, "AAAAA-vxztp-yub64-qm7fr-1298z", 1, 0, "P005", "Local comercial en área céntrica", 200000.0, 120, 5, 5 },
                    { 6, "EEEEE-vxztp-yub64-qm7fr-1298z", 2, 3, "P006", "Apartamento de lujo con piscina", 500000.0, 100, 1, 2 },
                    { 7, "AAAAA-vxztp-yub64-qm7fr-1298z", 3, 5, "P007", "Casa antigua con jardín grande", 180000.0, 250, 2, 3 },
                    { 8, "EEEEE-vxztp-yub64-qm7fr-1298z", 2, 3, "P008", "Dúplex moderno en barrio tranquilo", 220000.0, 130, 3, 4 },
                    { 9, "AAAAA-vxztp-yub64-qm7fr-1298z", 0, 0, "P009", "Terreno urbanizable cerca del lago", 120000.0, 1500, 4, 5 },
                    { 10, "EEEEE-vxztp-yub64-qm7fr-1298z", 2, 0, "P010", "Local comercial con amplio aparcamiento", 350000.0, 200, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "PropertyFavorites",
                columns: new[] { "Id", "PropertyId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "CCCCC-vxztp-yub64-qm7fr-1298z" },
                    { 2, 2, "CCCCC-vxztp-yub64-qm7fr-1298z" },
                    { 3, 3, "CCCCC-vxztp-yub64-qm7fr-1298z" },
                    { 4, 4, "DDDDD-vxztp-yub64-qm7fr-1298z" },
                    { 5, 5, "DDDDD-vxztp-yub64-qm7fr-1298z" },
                    { 6, 6, "DDDDD-vxztp-yub64-qm7fr-1298z" },
                    { 7, 7, "FFFFF-vxztp-yub64-qm7fr-1298z" },
                    { 8, 8, "FFFFF-vxztp-yub64-qm7fr-1298z" },
                    { 9, 9, "FFFFF-vxztp-yub64-qm7fr-1298z" }
                });

            migrationBuilder.InsertData(
                table: "PropertyImages",
                columns: new[] { "Id", "ImageUrl", "PropertyId" },
                values: new object[,]
                {
                    { 1, "https://gpvivienda.com/blog/wp-content/uploads/2023/03/ralph-ravi-kayden-mR1CIDduGLc-unsplash-1-1-1024x680.jpg", 1 },
                    { 2, "https://gpvivienda.com/blog/wp-content/uploads/2023/03/ralph-ravi-kayden-mR1CIDduGLc-unsplash-1-1-1024x680.jpg", 1 },
                    { 3, "https://gpvivienda.com/blog/wp-content/uploads/2023/03/ralph-ravi-kayden-mR1CIDduGLc-unsplash-1-1-1024x680.jpg", 2 },
                    { 4, "https://gpvivienda.com/blog/wp-content/uploads/2023/03/ralph-ravi-kayden-mR1CIDduGLc-unsplash-1-1-1024x680.jpg", 2 },
                    { 5, "https://gpvivienda.com/blog/wp-content/uploads/2023/03/ralph-ravi-kayden-mR1CIDduGLc-unsplash-1-1-1024x680.jpg", 3 },
                    { 6, "https://i.blogs.es/c68014/casa-3d/450_1000.jpeg", 4 },
                    { 7, "https://i.blogs.es/c68014/casa-3d/450_1000.jpeg", 5 },
                    { 8, "https://i.blogs.es/c68014/casa-3d/450_1000.jpeg", 6 },
                    { 9, "https://i.blogs.es/c68014/casa-3d/450_1000.jpeg", 7 },
                    { 10, "https://i.blogs.es/c68014/casa-3d/450_1000.jpeg", 8 }
                });

            migrationBuilder.InsertData(
                table: "PropertyImprovements",
                columns: new[] { "Id", "ImprovementId", "PropertyId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 2 },
                    { 4, 4, 2 },
                    { 5, 5, 3 },
                    { 6, 1, 4 },
                    { 7, 2, 5 },
                    { 8, 3, 6 },
                    { 9, 4, 7 },
                    { 10, 5, 8 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Improvements",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Improvements",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Improvements",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Improvements",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Improvements",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PropertyFavorites",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PropertyFavorites",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PropertyFavorites",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PropertyFavorites",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PropertyFavorites",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PropertyFavorites",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PropertyFavorites",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PropertyFavorites",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PropertyFavorites",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PropertyImages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PropertyImages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PropertyImages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PropertyImages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PropertyImages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PropertyImages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PropertyImages",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PropertyImages",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PropertyImages",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PropertyImages",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PropertyImprovements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PropertyImprovements",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PropertyImprovements",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PropertyImprovements",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PropertyImprovements",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PropertyImprovements",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PropertyImprovements",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PropertyImprovements",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PropertyImprovements",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PropertyImprovements",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Improvements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Improvements",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Improvements",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Improvements",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Improvements",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PropertyTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SaleTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<int>(
                name: "AgentId",
                table: "Properties",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
