using AutoMapper;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Services.Commons;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Application.ViewModels.User;
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

        public async Task<List<PropertyTypeViewModel>> GetAllViewModelWithInclude()
        {
            var propertyTypelist = await propertyTypeRepository.GetAllWithIncludeAsync(new List<string> { "Properties" });
            return propertyTypelist.Select(s => new PropertyTypeViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                CuantityPropertiesAssigned = s.Properties.Count() 
            }).ToList();
        }
    }
}
