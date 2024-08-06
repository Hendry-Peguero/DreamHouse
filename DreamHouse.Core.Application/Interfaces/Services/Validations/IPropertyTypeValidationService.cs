using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Application.ViewModels.SaleType;

namespace DreamHouse.Core.Application.Interfaces.Services.Validations
{
    public interface IPropertyTypeValidationService
    {
        Task<Dictionary<string, string>> DuplicateName(PropertyTypeSaveViewModel saleTypeVm);
        Task<Dictionary<string, string>> UpdateDuplicateName(PropertyTypeSaveViewModel saleTypeVm);

    }
}
