using AutoMapper;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using MediatR;

namespace DreamHouse.Core.Application.Features.PropertyType.Queries.GetByIdQuery
{
    public class GetPropertyTypeByIdQuery : IRequest<PropertyTypeViewModel>
    {
        public int Id { get; set; }
    }

    public class GetPropertyTypeByIdQueryHandler : IRequestHandler<GetPropertyTypeByIdQuery, PropertyTypeViewModel>
    {
        private readonly IPropertyTypeRepository propertyTypeRepository;
        private readonly IMapper mapper;

        public GetPropertyTypeByIdQueryHandler(IPropertyTypeRepository propertyTypeRepository,
            IMapper mapper)
        {
            this.propertyTypeRepository = propertyTypeRepository;
            this.mapper = mapper;
        }

        public async Task<PropertyTypeViewModel> Handle(GetPropertyTypeByIdQuery query, CancellationToken cancellationToken)
        {
            var propertyTypelist = await propertyTypeRepository.GetAllWithIncludeAsync(new List<string> { "Properties" });
            var propertyType = propertyTypelist.FirstOrDefault(property => property.Id == query.Id);

            if (propertyType == null) throw new Exception("propertyType not found");

            var categoryVm = mapper.Map<PropertyTypeViewModel>(propertyType);
            categoryVm.CuantityPropertiesAssigned = propertyType.Properties.Count;
            return categoryVm;
        }
    }
}
