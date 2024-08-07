using DreamHouse.Core.Application.ViewModels.Property;

namespace DreamHouse.Core.Application.ViewModels.PropertyFavorite
{
    public class PropertyFavoriteViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PropertyId { get; set; }

        //NAV
        public PropertyViewModel Property { get; set; }
    }
}
