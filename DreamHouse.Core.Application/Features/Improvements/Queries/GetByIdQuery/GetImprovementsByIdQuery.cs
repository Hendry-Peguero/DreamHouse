using AutoMapper;
using DreamHouse.Core.Application.Features.PropertyType.Queries.GetByIdQuery;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Application.ViewModels.Improvement;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.Core.Application.Features.Improvements.Queries.GetByIdQuery
{
    public class GetImprovementsByIdQuery : IRequest<ImprovementViewModel>
    {
        public int Id { get; set; }
    }
    public class GetImprovementsByIQueryHandler : IRequestHandler<GetImprovementsByIdQuery, ImprovementViewModel>
    {
        private readonly IImprovementRepository improvementRepository;
        private readonly IMapper mapper;

        public GetImprovementsByIQueryHandler(IImprovementRepository improvementRepository,
            IMapper mapper)
        {
            this.improvementRepository = improvementRepository;
            this.mapper = mapper;
        }

        public async Task<ImprovementViewModel> Handle(GetImprovementsByIdQuery query, CancellationToken cancellationToken)
        {
            var improvement = await improvementRepository.GetByIdAsync(query.Id);

            if (improvement == null) throw new Exception("Improvement not found");

            var improvementVm = mapper.Map<ImprovementViewModel>(improvement);
            return improvementVm;
        }
    }
}
