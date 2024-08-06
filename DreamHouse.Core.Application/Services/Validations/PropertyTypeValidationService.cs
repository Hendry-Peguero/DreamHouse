using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.Interfaces.Services.Validations;
using DreamHouse.Core.Application.Services.User;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Application.ViewModels.User;

namespace DreamHouse.Core.Application.Services.Validations
{
    public class PropertyTypeValidationService : IDuplicateNameValidationService
    {
        private readonly IPropertyTypeService propertyTypeService;

        public PropertyTypeValidationService(IPropertyTypeService propertyTypeService)
        {
            this.propertyTypeService = propertyTypeService;
        }

        public async Task<Dictionary<string, string>> DuplicateName(PropertyTypeSaveViewModel propertyTypeVm)
        {
            var errors = new Dictionary<string, string>();

            var duplicateName = (await propertyTypeService.GetAllAsync())
                .FirstOrDefault(propertyType=> propertyType.Name == propertyTypeVm.Name);

            if (duplicateName.Name != null) errors.Add("DuplicateName", "Name already in use");

            return errors;
        }

        public async Task<Dictionary<string, string>> UpdateDuplicateName(PropertyTypeSaveViewModel propertyTypeVm)
        {
            var errors = new Dictionary<string, string>();
            var propertyTypeToUpdate = await propertyTypeService.GetByIdAsync(propertyTypeVm.Id.Value);
            var sameName = propertyTypeToUpdate.Name == propertyTypeVm.Name;

            if (!sameName)
            {
                var duplicateName = (await propertyTypeService.GetAllAsync())
                    .FirstOrDefault(propertyType => propertyType.Name == propertyTypeVm.Name);
                
                if (duplicateName != null) errors.Add("DuplicateName", "Email already in use");
            }
            
            return errors;
        }
    }
}
