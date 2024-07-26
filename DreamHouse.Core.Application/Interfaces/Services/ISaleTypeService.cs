using DreamHouse.Core.Application.Interfaces.Services.Commons;
using DreamHouse.Core.Application.ViewModels.SaleType;
using DreamHouse.Core.Domain.Entities;

namespace DreamHouse.Core.Application.Interfaces.Services
{
    public interface ISaleTypeService : IGenericService<SaleTypeSaveViewModel, SaleTypeViewModel, SaleTypeEntity>
    {

    }
}
