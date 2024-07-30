using AutoMapper;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Services.Commons;
using DreamHouse.Core.Application.ViewModels.Property;
using DreamHouse.Core.Domain.Entities;

namespace DreamHouse.Core.Application.Services
{
    public class PropertyService : GenericService<PropertySaveViewModel, PropertyViewModel, PropertyEntity>, IPropertyService
    {
        private readonly IPropertyRepository propertyRepository;
        private readonly IMapper mapper;

        public PropertyService(
            IPropertyRepository propertyRepository,
            IMapper mapper
        )
        : base(propertyRepository, mapper)
        {
            this.propertyRepository = propertyRepository;
            this.mapper = mapper;
        }

        public async Task<List<PropertyViewModel>> GetAllWithTypePropertyAndSaleAsync()
        {
            var properties = await propertyRepository.GetAllWithIncludeAsync(new List<string> { "TypeProperty", "TypeSale" });
            var propertyViewModels = mapper.Map<List<PropertyViewModel>>(properties);

            foreach (var propertyViewModel in propertyViewModels)
            {
                var property = properties.FirstOrDefault(p => p.Id == propertyViewModel.Id);
                if (property != null)
                {
                    propertyViewModel.TypePropertyName = property.TypeProperty.Name;
                    propertyViewModel.TypeSaleName = property.TypeSale.Name;
                }
            }

            return propertyViewModels;
        }
    }
}
