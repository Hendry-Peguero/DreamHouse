using AutoMapper;
using DreamHouse.Core.Application.Features.PropertyType.Commands.Update;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Domain.Entities;
using MediatR;

namespace DreamHouse.Core.Application.Features.PropertyType.Commands.Create
{
    public class UpdatePropertyTypeCommand : IRequest<UpdatePropertyTypeResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdatePropertyTypeCommandHandler : IRequestHandler<UpdatePropertyTypeCommand, UpdatePropertyTypeResponse>
    {
        private readonly IPropertyTypeRepository propertyTypeRepository;
        private readonly IMapper mapper;

        public UpdatePropertyTypeCommandHandler(IPropertyTypeRepository propertyTypeRepository,
            IMapper mapper)
        {
            this.propertyTypeRepository = propertyTypeRepository;
            this.mapper = mapper;
        }

        public async Task<UpdatePropertyTypeResponse> Handle(UpdatePropertyTypeCommand command, CancellationToken cancellationToken)
        {
            var propertyTypeEntity = mapper.Map<PropertyTypeEntity>(command);
            var propertyTypeResponse = mapper.Map<UpdatePropertyTypeResponse>(await propertyTypeRepository.UpdateAsync(propertyTypeEntity, propertyTypeEntity.Id));
            return propertyTypeResponse;
        }
    }
}
