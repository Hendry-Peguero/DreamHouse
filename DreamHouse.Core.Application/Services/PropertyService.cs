using AutoMapper;
using DreamHouse.Core.Application.Dtos.Filters;
using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Helpers;
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
        private readonly IPropertyImageRepository propertyImageRepository;
        private readonly IPropertyImprovementRepository propertyImprovementRepository;
        private readonly IUserHelper userHelper;
        private readonly IImageHelper imageHelper;
        private readonly IMapper mapper;

        public PropertyService(
            IPropertyRepository propertyRepository,
            IPropertyFavoriteRepository propertyFavoriteRepository,
            IPropertyImageRepository propertyImageRepository,
            IPropertyImprovementRepository propertyImprovementRepository,
            IUserHelper userHelper,
            IImageHelper imageHelper,
            IMapper mapper
        )
        : base(propertyRepository, mapper)
        {
            this.propertyRepository = propertyRepository;
            this.propertyFavoriteRepository = propertyFavoriteRepository;
            this.propertyImageRepository = propertyImageRepository;
            this.propertyImprovementRepository = propertyImprovementRepository;
            this.userHelper = userHelper;
            this.imageHelper = imageHelper;
            this.mapper = mapper;
        }


        public async Task<List<PropertyViewModel>> GetAllWithIncludeAsync(List<string> includs)
        {
            var properties = await propertyRepository.GetAllWithIncludeAsync(includs);
            var propertiesMapped = mapper.Map<List<PropertyViewModel>>(properties);
            propertiesMapped.Reverse();
            return propertiesMapped;
        }

        public async Task<List<PropertyViewModel>> GetFilteredPropertiesAsync(PropertiesFilter filter)
        {
            var properties = await GetAllWithIncludeAsync(new List<string> { "TypeProperty", "TypeSale", "Favorites", "Images" });

            if (!string.IsNullOrEmpty(filter.Code)) properties = properties.Where(p => p.Code == filter.Code).ToList();
            if (!string.IsNullOrEmpty(filter.Type)) properties = properties.Where(p => p.TypeProperty!.Name == filter.Type).ToList();
            if (filter.MinPrice.HasValue) properties = properties.Where(p => p.Price >= filter.MinPrice.Value).ToList();
            if (filter.MaxPrice.HasValue) properties = properties.Where(p => p.Price <= filter.MaxPrice.Value).ToList();
            if (filter.Bedrooms.HasValue) properties = properties.Where(p => p.Bedrooms == filter.Bedrooms.Value).ToList();
            if (filter.Bathrooms.HasValue) properties = properties.Where(p => p.Bathrooms == filter.Bathrooms.Value).ToList();
            
            return properties;
        }


        public async Task<List<PropertyViewModel>> GetFilteredPropertiesForAgentAsync(PropertiesFilter filter)
        {
            var user = userHelper.GetUser();
            return await GetFilteredPropertiesByAgentIdAsync(filter, user.Id);
        }

        public async Task<List<PropertyViewModel>> GetFilteredPropertiesForClientAsync(PropertiesFilter filter)
        {
            var user = userHelper.GetUser();
            var properties = await GetFilteredPropertiesAsync(filter);
            foreach (var property in properties)
            {
                bool isMarkedAsFavorite = property.Favorites?.Any(p => p.UserId == user.Id) ?? false;
                if (isMarkedAsFavorite) property.MarkedAsFavorite = true;
            }
            return properties;
        }

        public async Task<List<PropertyViewModel>> GetFilteredPropertiesByFavoriteAsync(PropertiesFilter filter)
        {
            var properties = await GetFilteredPropertiesForClientAsync(filter);
            return properties.Where(p => p.MarkedAsFavorite).ToList();
        }

        public async Task<List<PropertyViewModel>> GetFilteredPropertiesByAgentIdAsync(PropertiesFilter filter, string agentId)
        {
            return (await GetFilteredPropertiesAsync(filter)).Where(p => p.AgentId == agentId).ToList();
        }

        public async Task<PropertyViewModel?> GetPropertyDetailsAsync(int porpertyId)
        {
            var properties = await GetAllWithIncludeAsync(new List<string> { "TypeProperty", "TypeSale", "ImprovementProperties.Improvement", "Images" });
            return properties.FirstOrDefault(p => p.Id == porpertyId);
        }

        public async Task<int> GetAllFromAgentAsync(string agentId)
        {
            return (await base.GetAllAsync()).Where(property => property.AgentId == agentId).Count();
        }


        public override async Task<PropertySaveViewModel?> AddAsync(PropertySaveViewModel propertySaveViewModel)
        {
            // Generate Code
            var properties = await propertyRepository.GetAllAsync();
            string code;
            while (true)
            {
                code = CodeStingGenerator.GenerateRandomLetters(
                    BusinessLogicConstansHelper.MaximumLettersPropertyCode,
                    CodeStingGenerator.Uppercase +
                    CodeStingGenerator.Lowercase +
                    CodeStingGenerator.Numbers
                );

                if (!properties.Any(p => p.Code == code)) break;
            }
            propertySaveViewModel.Code = code;

            // Set agent
            propertySaveViewModel.AgentId = userHelper.GetUser()!.Id;

            // Create the property
            var propertyCreated = await base.AddAsync(propertySaveViewModel);

            // Create the improvements
            foreach (var improvementId in propertySaveViewModel.IdSelectedImprovements)
            {
                await propertyImprovementRepository.AddAsync(new PropertyImprovementEntity()
                {
                    PropertyId = propertyCreated.Id,
                    ImprovementId = improvementId
                });
            }

            // Create the images 
            foreach (var image in propertySaveViewModel.Images)
            {
                string relativePath = imageHelper.SaveImage(image, propertyCreated.Id.ToString(), EGroupImage.PROPERTIES);
                await propertyImageRepository.AddAsync(new PropertyImageEntity()
                {
                    ImageUrl = relativePath,
                    PropertyId = propertyCreated.Id
                });
            }

            return propertyCreated;
        }

        public override async Task<PropertySaveViewModel?> UpdateAsync(PropertySaveViewModel propertySaveViewModel, int propertyId)
        {
            // Update the property
            var propertyUpdated = await base.UpdateAsync(propertySaveViewModel, propertyId);

            // Remove the old improvements and add the new ones
            var oldImprovements = (await propertyImprovementRepository.GetAllAsync()).Where(i => i.PropertyId == propertyId);
            foreach (var improvement in oldImprovements)
            {
                await propertyImprovementRepository.DeleteAsync(improvement);
            }
            foreach (var improvementId in propertySaveViewModel.IdSelectedImprovements)
            {
                await propertyImprovementRepository.AddAsync(new PropertyImprovementEntity()
                {
                    PropertyId = propertyId,
                    ImprovementId = improvementId
                });
            }

            // Remove the old images and add the new ones
            if (propertySaveViewModel.Images != null && propertySaveViewModel.Images.Count != 0)
            {
                var oldImages = (await propertyImageRepository.GetAllAsync()).Where(i => i.PropertyId == propertyId);
                foreach (var image in oldImages)
                {
                    await propertyImageRepository.DeleteAsync(image);
                }
                imageHelper.RemoveImage(propertyId.ToString(), EGroupImage.PROPERTIES);
                foreach (var image in propertySaveViewModel.Images)
                {
                    string relativePath = imageHelper.SaveImage(image, propertyId.ToString(), EGroupImage.PROPERTIES);
                    await propertyImageRepository.AddAsync(new PropertyImageEntity()
                    {
                        ImageUrl = relativePath,
                        PropertyId = propertyId
                    });
                }
            }

            return propertyUpdated;
        }


        public override async Task DeleteAsync(int propertyId)
        {
            // Delete the local images
            imageHelper.RemoveImage(propertyId.ToString(), EGroupImage.PROPERTIES);

            // Delete the property
            await base.DeleteAsync(propertyId);
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
