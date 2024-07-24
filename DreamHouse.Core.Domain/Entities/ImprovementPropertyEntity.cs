namespace DreamHouse.Core.Domain.Entities
{
    public class ImprovementPropertyEntity
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int ImprovementId { get; set; }

        //NAV
        public PropertyEntity Property { get; set; }
        public ImprovementEntity Improvement { get; set; }
    }
}
