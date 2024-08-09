using AutoMapper;
using DreamHouse.Core.Application.Features.PropertyType.Queries.GetAllQuery;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Application.ViewModels.Improvement;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.Core.Application.Features.Improvements.Queries.GetAllQuery
{
    public class GetAllImprovementQuery : IRequest<List<ImprovementViewModel>>
    {
        public int Id { get; set; }
    }

    public class GetAllImprovementQueryHandler : IRequestHandler<GetAllImprovementQuery, List<ImprovementViewModel>>
    {
        private readonly IImprovementRepository improvementRepository;
        private readonly IMapper mapper;

        public GetAllImprovementQueryHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            this.improvementRepository = improvementRepository;
            this.mapper = mapper;
        }

        public async Task<List<ImprovementViewModel>> Handle(GetAllImprovementQuery query, CancellationToken cancellationToken)
        {
            var improvementVm = await GetAllViewModelWithInclude();
            if (improvementVm == null) throw new Exception("Improvements not found");

            return improvementVm;
        }

        private async Task<List<ImprovementViewModel>> GetAllViewModelWithInclude()
        {

            var improvementList = await improvementRepository.GetAllAsync();

            if (improvementList == null || !improvementList.Any())
            {       
                throw new Exception("Improvements not found");
            }

            var improvementVm = mapper.Map<List<ImprovementViewModel>>(improvementList);
            return improvementVm;
        }
    }
}

