namespace DreamHouse.Core.Domain.Entities
{
    public class PropertyEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int SquareMeter { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int TypePropertyId { get; set; }
        public int TypeSaleId { get; set; } 
        public string AgentId { get; set; }

        //NAV 
        public PropertyTypeEntity? TypeProperty { get; set; }
        public SaleTypeEntity? TypeSale { get; set; }
        public ICollection<PropertyFavoriteEntity>? Favorites { get; set; }
        public ICollection<PropertyImageEntity>? Images { get; set; }
        public ICollection<PropertyImprovementEntity>? ImprovementProperties { get; set; }
    }
}
