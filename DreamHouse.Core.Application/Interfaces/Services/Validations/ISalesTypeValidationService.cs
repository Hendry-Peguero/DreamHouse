using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Application.ViewModels.SaleType;

namespace DreamHouse.Core.Application.Interfaces.Services.Validations
{
    public interface ISalesTypeValidationService
    {
        Task<Dictionary<string, string>> DuplicateName(SaleTypeSaveViewModel propertyTypeVm);
        Task<Dictionary<string, string>> UpdateDuplicateName(SaleTypeSaveViewModel propertyTypeVm);

    }
}
