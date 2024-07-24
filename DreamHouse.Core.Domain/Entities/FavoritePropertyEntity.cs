using System.Diagnostics.Contracts;

namespace DreamHouse.Core.Domain.Entities
{
    public class FavoritePropertyEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PropertyId { get; set; }

        //NAV
        public PropertyEntity Property { get; set; }
    }
}
