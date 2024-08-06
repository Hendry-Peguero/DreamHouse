using DreamHouse.Core.Application.Dtos.Filters;
using DreamHouse.Core.Application.ViewModels.Property;

namespace DreamHouse.Core.Application.ViewModels.Home
{
    public class HomeBasicViewModel
    {
        public PropertiesFilter? filter {  get; set; } 
        public List<PropertyViewModel>? Properties { get; set; }
    }
}
