using AutoMapper;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Domain.Entities;
using MediatR;

namespace DreamHouse.Core.Application.Features.Improvements.Commands.Create
{
    public class CreateImprovementCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CreateImprovementCommandHandler : IRequestHandler<CreateImprovementCommand, int>
    {
        private readonly IImprovementRepository improvementRepository;
        private readonly IMapper mapper;

        public CreateImprovementCommandHandler(IImprovementRepository improvementRepository,
            IMapper mapper)
        {
            this.improvementRepository = improvementRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateImprovementCommand command, CancellationToken cancellationToken)
        {
            var commandEntity = mapper.Map<ImprovementEntity>(command);
            commandEntity = await improvementRepository.AddAsync(commandEntity);
            return commandEntity.Id;
        }
    }
}
