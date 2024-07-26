namespace DreamHouse.Core.Domain.Entities
{
    public class ImprovementEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //NAV
        public ICollection<PropertyImprovementEntity> ImprovementProperties { get; set; }
    }
} 
