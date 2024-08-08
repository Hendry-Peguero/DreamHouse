using AutoMapper;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Application.ViewModels.Property;
using MediatR;

namespace DreamHouse.Core.Application.Features.Properties.Queries.GetPropertyById
{
    public class GetPropertyByIdQuery : IRequest<PropertyViewModel>
    {
        public int Id { get; set; }
    }

    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, PropertyViewModel>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IMapper mapper;

        public GetPropertyByIdQueryHandler(IPropertyRepository propertyRepository,
            IMapper mapper)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
        }

        public async Task<PropertyViewModel> Handle(GetPropertyByIdQuery query, CancellationToken cancellationToken)
        {
            var propertiesVm = mapper.Map<List<PropertyViewModel>>(await propertyRepository.GetAllAsync());
            var property = propertiesVm.FirstOrDefault(prop => prop.Id == query.Id);

            if (property == null) throw new Exception("property not found");

            return property;
        }
    }
}
