using AutoMapper;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Services.Commons;
using DreamHouse.Core.Application.ViewModels.Improvement;
using DreamHouse.Core.Domain.Entities;

namespace DreamHouse.Core.Application.Services
{
    public class ImprovementService : GenericService<ImprovementSaveViewModel, ImprovementViewModel, ImprovementEntity>, IImprovementService
    {
        private readonly IImprovementRepository improvementRepository;
        private readonly IMapper mapper;

        public ImprovementService(
            IImprovementRepository improvementRepository,
            IMapper mapper
        )
        : base(improvementRepository, mapper)
        {
            this.improvementRepository = improvementRepository;
            this.mapper = mapper;
        }
    }
}
