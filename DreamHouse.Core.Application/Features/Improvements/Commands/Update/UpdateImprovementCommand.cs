using AutoMapper;
using DreamHouse.Core.Application.Features.PropertyType.Commands.Create;
using DreamHouse.Core.Application.Features.PropertyType.Commands.Update;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.Core.Application.Features.Improvements.Commands.Update
{
    public class UpdateImprovementCommand : IRequest<UpdateImprovementResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class UpdateImprovementCommandHandler : IRequestHandler<UpdateImprovementCommand, UpdateImprovementResponse>
    {
        private readonly IImprovementRepository improvementRepository;
        private readonly IMapper mapper;

        public UpdateImprovementCommandHandler(IImprovementRepository improvementRepository,
            IMapper mapper)
        {
            this.improvementRepository = improvementRepository;
            this.mapper = mapper;
        }

        public async Task<UpdateImprovementResponse> Handle(UpdateImprovementCommand command, CancellationToken cancellationToken)
        {
            var improvementEntity = mapper.Map<ImprovementEntity>(command);
            var improvementResponse = mapper.Map<UpdateImprovementResponse>(await improvementRepository.UpdateAsync(improvementEntity, improvementEntity.Id));
            return improvementResponse;
        }
    }
}
