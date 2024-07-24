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
        public int AgentId { get; set; }

        //NAV 
        public TypePropertyEntity TypeProperty { get; set; }
        public TypeSaleEntity TypeSale { get; set; }
        public ICollection<ImprovementEntity> Improvements { get; set; }
        public ICollection<FavoritePropertyEntity> Favorites { get; set; }
        public ICollection<ImagePropertyEntity> Images { get; set; }
        public ICollection<ImprovementPropertyEntity> ImprovementProperties { get; set; }
    }
}
