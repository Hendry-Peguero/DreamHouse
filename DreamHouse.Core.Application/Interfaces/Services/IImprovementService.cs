using DreamHouse.Core.Application.Interfaces.Services.Commons;
using DreamHouse.Core.Application.ViewModels.Improvement;
using DreamHouse.Core.Domain.Entities;

namespace DreamHouse.Core.Application.Interfaces.Services
{
    public interface IImprovementService : IGenericService<ImprovementSaveViewModel, ImprovementViewModel, ImprovementEntity>
    {

    }
}
