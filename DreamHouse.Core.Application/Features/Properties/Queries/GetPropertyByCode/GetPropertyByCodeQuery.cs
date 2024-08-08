using AutoMapper;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Application.ViewModels.Property;
using MediatR;

namespace DreamHouse.Core.Application.Features.Properties.Queries.GetPropertyById
{
    public class GetPropertyByCodeQuery : IRequest<PropertyViewModel>
    {
        public string Code { get; set; }
    }

    public class GetPropertyByCodeQueryHandler : IRequestHandler<GetPropertyByCodeQuery, PropertyViewModel>
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IMapper mapper;

        public GetPropertyByCodeQueryHandler(IPropertyRepository propertyRepository,
            IMapper mapper)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
        }

        public async Task<PropertyViewModel> Handle(GetPropertyByCodeQuery query, CancellationToken cancellationToken)
        {
            var propertiesVm = mapper.Map<List<PropertyViewModel>>(await propertyRepository.GetAllAsync());
            var property = propertiesVm.FirstOrDefault(prop => prop.Code == query.Code);

            if (property == null) throw new Exception("property not found");

            return property;
        }
    }
}
