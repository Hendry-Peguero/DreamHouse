using AutoMapper;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Domain.Entities;
using MediatR;

namespace DreamHouse.Core.Application.Features.PropertyType.Commands.Create
{
    public class CreatePropertyTypeCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreatePropertyTypeCommandHandler : IRequestHandler<CreatePropertyTypeCommand, int>
    {
        private readonly IPropertyTypeRepository propertyTypeRepository;
        private readonly IMapper mapper;

        public CreatePropertyTypeCommandHandler(IPropertyTypeRepository propertyTypeRepository,
            IMapper mapper)
        {
            this.propertyTypeRepository = propertyTypeRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreatePropertyTypeCommand command, CancellationToken cancellationToken)
        {
            var commandEntity = mapper.Map<PropertyTypeEntity>(command);
            commandEntity = await propertyTypeRepository.AddAsync(commandEntity);
            return commandEntity.Id;
        }
    }
}
