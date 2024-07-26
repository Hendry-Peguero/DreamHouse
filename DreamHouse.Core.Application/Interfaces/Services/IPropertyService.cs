using DreamHouse.Core.Application.Interfaces.Services.Commons;
using DreamHouse.Core.Application.ViewModels.Property;
using DreamHouse.Core.Domain.Entities;

namespace DreamHouse.Core.Application.Interfaces.Services
{
    public interface IPropertyService : IGenericService<PropertySaveViewModel, PropertyViewModel, PropertyEntity>
    {

    }
}
