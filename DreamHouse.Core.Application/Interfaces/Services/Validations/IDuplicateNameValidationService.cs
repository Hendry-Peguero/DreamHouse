using DreamHouse.Core.Application.ViewModels.PropertyType;

namespace DreamHouse.Core.Application.Interfaces.Services.Validations
{
    public interface IDuplicateNameValidationService
    {
        Task<Dictionary<string, string>> DuplicateName(PropertyTypeSaveViewModel propertyTypeVm);
        Task<Dictionary<string, string>> UpdateDuplicateName(PropertyTypeSaveViewModel propertyTypeVm);

    }
}
