using AutoMapper;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Services.Commons;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Domain.Entities;

namespace DreamHouse.Core.Application.Services
{
    public class PropertyTypeService : GenericService<PropertyTypeSaveViewModel, PropertyTypeViewModel, PropertyTypeEntity>, IPropertyTypeService
    {
        private readonly IPropertyTypeRepository propertyTypeRepository;
        private readonly IMapper mapper;

        public PropertyTypeService(
            IPropertyTypeRepository propertyTypeRepository,
            IMapper mapper
        )
        : base(propertyTypeRepository, mapper)
        {
            this.propertyTypeRepository = propertyTypeRepository;
            this.mapper = mapper;
        }
    }
}
