using AutoMapper;
using DreamHouse.Core.Application.Dtos.Filters;
using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Interfaces.Helpers;
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
        private readonly IPropertyFavoriteRepository propertyFavoriteRepository;
        private readonly IUserHelper userHelper;
        private readonly IMapper mapper;

        public PropertyService(
            IPropertyRepository propertyRepository,
            IPropertyFavoriteRepository propertyFavoriteRepository,
            IUserHelper userHelper,
            IMapper mapper
        )
        : base(propertyRepository, mapper)
        {
            this.propertyRepository = propertyRepository;
            this.propertyFavoriteRepository = propertyFavoriteRepository;
            this.userHelper = userHelper;
            this.mapper = mapper;
        }


        public async Task<List<PropertyViewModel>> GetAllWithIncludeAsync(List<string> includs)
        {
            var properties = await propertyRepository.GetAllWithIncludeAsync(includs);
            return mapper.Map<List<PropertyViewModel>>(properties);
        }

        public async Task<List<PropertyViewModel>> GetFilteredPropertiesAsync(PropertiesFilter filter)
        {
            var properties = await GetAllWithIncludeAsync(new List<string> { "TypeProperty", "TypeSale", "Favorites" });

            if (!string.IsNullOrEmpty(filter.Code)) properties = properties.Where(p => p.Code == filter.Code).ToList();
            if (!string.IsNullOrEmpty(filter.Type)) properties = properties.Where(p => p.TypeProperty!.Name == filter.Type).ToList();
            if (filter.MinPrice.HasValue) properties = properties.Where(p => p.Price >= filter.MinPrice.Value).ToList();
            if (filter.MaxPrice.HasValue) properties = properties.Where(p => p.Price <= filter.MaxPrice.Value).ToList();
            if (filter.Bedrooms.HasValue) properties = properties.Where(p => p.Bedrooms == filter.Bedrooms.Value).ToList();
            if (filter.Bathrooms.HasValue) properties = properties.Where(p => p.Bathrooms == filter.Bathrooms.Value).ToList();
            
            return properties;
        }

        public async Task<List<PropertyViewModel>> GetFilteredPropertiesByRoleAsync(PropertiesFilter filter)
        {
            var user = userHelper.GetUser();
            string? userPrincipalRole = user?.Roles![^1];

            // Filter and return the properties in home by the role of the current user logged
            if (user == null)
            {
                // No user logged 
                return await GetFilteredPropertiesAsync(filter);
            }
            else
            {
                // User logged 
                switch (userPrincipalRole!.ToUpper())
                {
                    case nameof(ERoles.CLIENT):

                        // Get the properties and mark as favorites if it have them
                        var properties = await GetFilteredPropertiesAsync(filter);
                        foreach (var property in properties) {
                            bool isMarkedAsFavorite = property.Favorites?.Any(p => p.UserId == user.Id) ?? false;
                            if (isMarkedAsFavorite) property.MarkedAsFavorite = true;
                        }
                        return properties;

                    case nameof(ERoles.AGENT): return (await GetFilteredPropertiesAsync(filter)).Where(p => p.AgentId == user!.Id).ToList();
                    default: return await GetFilteredPropertiesAsync(filter);
                }
            }
        }

        public async Task<List<PropertyViewModel>> GetFilteredPropertiesByFavoriteAsync(PropertiesFilter filter)
        {
            var properties = await GetFilteredPropertiesByRoleAsync(filter);
            return properties.Where(p => p.MarkedAsFavorite).ToList();
        }

        public async Task<PropertyViewModel?> GetPropertyDetailsAsync(int porpertyId)
        {
            var properties = await GetAllWithIncludeAsync(new List<string> { "TypeProperty", "TypeSale", "ImprovementProperties.Improvement" });
            return properties.FirstOrDefault(p => p.Id == porpertyId);
        }

        public async Task<int> GetAllFromAgentAsync(string agentId)
        {
            return (await base.GetAllAsync()).Where(property => property.AgentId == agentId).Count();
        }

        public async Task ConfigFavorite(int propertyId) 
        {
            //Check if the property has favorite
            var currentUser = userHelper.GetUser()!;
            var propertyHasFavorite = (await propertyFavoriteRepository.GetAllAsync()).FirstOrDefault(f => 
                f.UserId == currentUser.Id && f.PropertyId == propertyId
            );

            if (propertyHasFavorite == null)
            {
                // Add the favorite
                PropertyFavoriteEntity entity = new()
                {
                    UserId = currentUser.Id,
                    PropertyId = propertyId
                };
                await propertyFavoriteRepository.AddAsync(entity);
            }
            else
            {
                // Remove the favorite
                await propertyFavoriteRepository.DeleteAsync(propertyHasFavorite);
            }
        }

    }
}
