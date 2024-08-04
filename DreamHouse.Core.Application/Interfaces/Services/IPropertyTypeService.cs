using DreamHouse.Core.Application.Interfaces.Services.Commons;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Domain.Entities;

namespace DreamHouse.Core.Application.Interfaces.Services
{
    public interface IPropertyTypeService : IGenericService<PropertyTypeSaveViewModel, PropertyTypeViewModel, PropertyTypeEntity>
    {
        Task<List<PropertyTypeViewModel>> GetAllViewModelWithInclude();
    }
}
