using DreamHouse.Core.Application.ViewModels.Property;

namespace DreamHouse.Core.Application.ViewModels.PropertyType
{
    public class PropertyTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CuantityPropertiesAssigned { get; set; }
        public ICollection<PropertyViewModel>? Properties { get; set; }

    }
}
