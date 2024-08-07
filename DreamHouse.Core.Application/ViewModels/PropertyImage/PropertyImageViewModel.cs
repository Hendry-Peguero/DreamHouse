using DreamHouse.Core.Application.ViewModels.Property;

namespace DreamHouse.Core.Application.ViewModels.PropertyImage
{
    public class PropertyImageViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int PropertyId { get; set; }

        //NAV
        public PropertyViewModel Property { get; set; }
    }
}
