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
        private readonly IPropertyService propertyService;
        private readonly IMapper mapper;

        public PropertyTypeService(
            IPropertyTypeRepository propertyTypeRepository,
            IPropertyService propertyService,
            IMapper mapper
        )
        : base(propertyTypeRepository, mapper)
        {
            this.propertyTypeRepository = propertyTypeRepository;
            this.propertyService = propertyService;
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

        public override async Task DeleteAsync(int id)
        {
            // Delete properties manually
            var properties = (await propertyService.GetAllAsync()).Where(p => p.TypePropertyId == id);
            foreach (var property in properties)
            {
                await propertyService.DeleteAsync(property.Id);
            }

            //Delete
            await base.DeleteAsync(id);
        }
    }
}
