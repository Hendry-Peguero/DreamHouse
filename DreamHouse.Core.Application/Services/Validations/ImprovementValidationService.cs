using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.Interfaces.Services.Validations;
using DreamHouse.Core.Application.Services.User;
using DreamHouse.Core.Application.ViewModels.Improvement;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Application.ViewModels.User;

namespace DreamHouse.Core.Application.Services.Validations
{
    public class ImprovementValidationService : IImprovementValidationService
    {
        private readonly IImprovementService improvementService;

        public ImprovementValidationService(IImprovementService improvementService)
        {
            this.improvementService = improvementService;
        }

        public async Task<Dictionary<string, string>> DuplicateName(ImprovementSaveViewModel improvementSVm)
        {
            var errors = new Dictionary<string, string>();
            if (improvementSVm.Name != null)
            {
                var duplicateName = (await improvementService.GetAllAsync())
                    .FirstOrDefault(propertyType => propertyType.Name == improvementSVm.Name);

                if (duplicateName.Name != null) errors.Add("DuplicateName", "Name already in use");
            }

            return errors;
        }

        public async Task<Dictionary<string, string>> UpdateDuplicateName(ImprovementSaveViewModel improvementSVm)
        {
            var errors = new Dictionary<string, string>();
            var improvementToUpdate = await improvementService.GetByIdAsync(improvementSVm.Id.Value);
            var sameName = improvementToUpdate.Name == improvementSVm.Name;

            if (!sameName)
            {
                var duplicateName = (await improvementService.GetAllAsync())
                    .FirstOrDefault(improvement => improvement.Name == improvementSVm.Name);
                
                if (duplicateName != null) errors.Add("DuplicateName", "Email already in use");
            }
            
            return errors;
        }
    }
}
