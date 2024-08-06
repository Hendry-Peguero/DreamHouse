using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Interfaces.Services.Validations;
using DreamHouse.Core.Application.ViewModels.SaleType;

namespace DreamHouse.Core.Application.Services.Validations
{
    public class SalesTypeValidationService : ISalesTypeValidationService
    {
        private readonly ISaleTypeService saleTypeService;

        public SalesTypeValidationService(ISaleTypeService saleTypeService)
        {
            this.saleTypeService = saleTypeService;
        }

        public async Task<Dictionary<string, string>> DuplicateName(SaleTypeSaveViewModel saleTypeVm)
        {
            var errors = new Dictionary<string, string>();

            if (saleTypeVm.Name != null)
            {
                var duplicateName = (await saleTypeService.GetAllAsync())
                .FirstOrDefault(saleType => saleType.Name == saleTypeVm.Name);

                if (duplicateName.Name != null) errors.Add("DuplicateName", "Name already in use");
            }

            return errors;
        }

        public async Task<Dictionary<string, string>> UpdateDuplicateName(SaleTypeSaveViewModel saleTypeVm)
        {
            var errors = new Dictionary<string, string>();
            var saleTypeToUpdate = await saleTypeService.GetByIdAsync(saleTypeVm.Id.Value);
            var sameName = saleTypeToUpdate.Name == saleTypeVm.Name;

            if (!sameName)
            {
                var duplicateName = (await saleTypeService.GetAllAsync())
                    .FirstOrDefault(propertyType => propertyType.Name == saleTypeVm.Name);

                if (duplicateName != null) errors.Add("DuplicateName", "Email already in use");
            }

            return errors;
        }
    }
}
