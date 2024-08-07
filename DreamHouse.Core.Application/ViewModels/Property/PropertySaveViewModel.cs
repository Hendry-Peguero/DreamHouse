using DreamHouse.Core.Application.ViewModels.Improvement;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Application.ViewModels.SaleType;
using Microsoft.AspNetCore.Http;

namespace DreamHouse.Core.Application.ViewModels.Property
{
    public class PropertySaveViewModel
    {
        public int Id { get; set; }
        public int SelectedPropertyTypeId { get; set; }
        public int SelectedSaleTypeId { get; set; }
        public List<int> IdSelectedImprovements { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int SquareMeter { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public List<IFormFile> Images { get; set; }

        // Properties for hide
        public string Code { get; set; }


        // Lists
        public List<PropertyTypeViewModel>? PropertyTypes { get; set; }
        public List<SaleTypeViewModel>? SaleTypes { get; set; }
        public List<ImprovementViewModel>? Improvements { get; set; }
    }
}
