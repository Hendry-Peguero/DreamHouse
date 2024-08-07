using DreamHouse.Core.Application.ViewModels.Improvement;

namespace DreamHouse.Core.Application.Interfaces.Services.Validations
{
    public interface IImprovementValidationService
    {
        Task<Dictionary<string, string>> DuplicateName(ImprovementSaveViewModel improvmenetSVm);
        Task<Dictionary<string, string>> UpdateDuplicateName(ImprovementSaveViewModel improvementSVm);

    }
}
