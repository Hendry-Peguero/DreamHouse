 namespace DreamHouse.Core.Domain.Entities
{
    public class ImagePropertyEntity
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int PropertyId { get; set; }

        //NAV
        public PropertyEntity Property { get; set; }
    }
}
