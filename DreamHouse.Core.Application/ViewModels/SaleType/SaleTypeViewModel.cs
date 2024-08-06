using DreamHouse.Core.Application.ViewModels.Property;
using DreamHouse.Core.Domain.Entities;

namespace DreamHouse.Core.Application.ViewModels.SaleType
{
    public class SaleTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CuantitySalesAssigned { get; set; }
        public ICollection<PropertyViewModel>? Properties { get; set; }

    }
}
