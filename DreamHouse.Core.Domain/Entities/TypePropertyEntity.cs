namespace DreamHouse.Core.Domain.Entities
{
    public class TypePropertyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //NAV
        public ICollection<PropertyEntity> Properties { get; set; }
    }
}
