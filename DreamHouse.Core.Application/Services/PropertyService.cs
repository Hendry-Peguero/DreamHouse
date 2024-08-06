using AutoMapper;
using DreamHouse.Core.Application.Dtos.Filters;
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

        public async Task<List<PropertyViewModel>> GetFilteredPropertiesAsync(PropertiesFilter filter)
        {
            var properties = await GetAllWithTypePropertyAndSaleAsync();

            if (!string.IsNullOrEmpty(filter.Code))
            {
                properties = properties.Where(p => p.Code == filter.Code).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Type))
            {
                properties = properties.Where(p => p.TypePropertyName == filter.Type).ToList();
            }

            if (filter.MinPrice.HasValue)
            {
                properties = properties.Where(p => p.Price <= filter.MinPrice.Value).ToList();
            }

            if (filter.MaxPrice.HasValue)
            {
                properties = properties.Where(p => p.Price <= filter.MaxPrice.Value).ToList();
            }

            if (filter.Bedrooms.HasValue)
            {
                properties = properties.Where(p => p.Bedrooms == filter.Bedrooms.Value).ToList();
            }

            if (filter.Bathrooms.HasValue)
            {
                properties = properties.Where(p => p.Bathrooms == filter.Bathrooms.Value).ToList();
            }

            return properties;
        }

        public async Task<PropertyViewModel?> GetPropertyDetailsAsync(int id)
        {
            var properties = await propertyRepository.GetAllWithIncludeAsync(new List<string> { "TypeProperty", "TypeSale", "ImprovementProperties.Improvement" });
            var property = properties.FirstOrDefault(p => p.Id == id);
            if (property == null)
            {
                return null;
            }

            var propertyViewModel = mapper.Map<PropertyViewModel>(property);
            propertyViewModel.TypePropertyName = property.TypeProperty?.Name;
            propertyViewModel.TypeSaleName = property.TypeSale?.Name;
            propertyViewModel.Improvements = property.ImprovementProperties?.Select(ip => ip.Improvement.Name).ToList() ?? new List<string>();

            return propertyViewModel;
        }

        public async Task<int> GetAllFromAgentAsync(string agentId)
        {
            return (await base.GetAllAsync()).Where(property => property.AgentId == agentId).Count();
        }

    }
}
