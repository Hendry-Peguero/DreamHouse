using DreamHouse.Core.Application.ViewModels.Agent;
using DreamHouse.Core.Application.ViewModels.PropertyFavorite;
using DreamHouse.Core.Application.ViewModels.PropertyImage;
using DreamHouse.Core.Application.ViewModels.PropertyImprovement;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Application.ViewModels.SaleType;
using DreamHouse.Core.Domain.Entities;

namespace DreamHouse.Core.Application.ViewModels.Property
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int SquareMeter { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int TypePropertyId { get; set; }
        public int TypeSaleId { get; set; }
        public string AgentId { get; set; }

        // Extras
        public bool MarkedAsFavorite { get; set; }


        public AgentViewModel? Agent { get; set; }
        public PropertyTypeViewModel? TypeProperty { get; set; }
        public SaleTypeViewModel? TypeSale { get; set; }
        public ICollection<PropertyFavoriteViewModel>? Favorites { get; set; }
        public ICollection<PropertyImageViewModel>? Images { get; set; }
        public ICollection<PropertyImprovementViewModel>? ImprovementProperties { get; set; }
    }
}
