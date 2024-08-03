using DreamHouse.Core.Application.Interfaces.Services.Commons;
using DreamHouse.Core.Application.ViewModels.Property;
using DreamHouse.Core.Domain.Entities;

namespace DreamHouse.Core.Application.Interfaces.Services
{
    public interface IPropertyService : IGenericService<PropertySaveViewModel, PropertyViewModel, PropertyEntity>
    {
        Task<List<PropertyViewModel>> GetAllWithTypePropertyAndSaleAsync();
        Task<List<PropertyViewModel>> GetFilteredPropertiesAsync(string code, string type, decimal? minPrice, decimal? maxPrice, int? bedrooms, int? bathrooms);
        Task<PropertyViewModel?> GetPropertyDetailsAsync(int id);
    }
}
