using DreamHouse.Core.Application.Dtos.Filters;
using DreamHouse.Core.Application.Interfaces.Services.Commons;
using DreamHouse.Core.Application.ViewModels.Property;
using DreamHouse.Core.Domain.Entities;

namespace DreamHouse.Core.Application.Interfaces.Services
{
    public interface IPropertyService : IGenericService<PropertySaveViewModel, PropertyViewModel, PropertyEntity>
    {
        Task<List<PropertyViewModel>> GetAllWithIncludeAsync(List<string> includs);
        Task<List<PropertyViewModel>> GetFilteredPropertiesAsync(PropertiesFilter filter);
        Task<List<PropertyViewModel>> GetFilteredPropertiesForAgentAsync(PropertiesFilter filter);
        Task<List<PropertyViewModel>> GetFilteredPropertiesForClientAsync(PropertiesFilter filter);
        Task<List<PropertyViewModel>> GetFilteredPropertiesByFavoriteAsync(PropertiesFilter filter);
        Task<List<PropertyViewModel>> GetFilteredPropertiesByAgentIdAsync(PropertiesFilter filter, string agentId);
        Task<PropertyViewModel?> GetPropertyDetailsAsync(int porpertyId);
        Task<int> GetAllFromAgentAsync(string agentId);
        new Task<PropertySaveViewModel?> AddAsync(PropertySaveViewModel propertySaveViewModel);
        new Task<PropertySaveViewModel?> UpdateAsync(PropertySaveViewModel propertySaveViewModel, int propertyId);
        new Task DeleteAsync(int propertyId);
        Task ConfigFavorite(int propertyId);
    }
}
