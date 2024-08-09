using AutoMapper;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Services.Commons;
using DreamHouse.Core.Application.ViewModels.SaleType;
using DreamHouse.Core.Domain.Entities;

namespace DreamHouse.Core.Application.Services
{
    public class SaleTypeService : GenericService<SaleTypeSaveViewModel, SaleTypeViewModel, SaleTypeEntity>, ISaleTypeService
    {
        private readonly ISaleTypeRepository saleTypeRepository;
        private readonly IPropertyService propertyService;
        private readonly IMapper mapper;

        public SaleTypeService(
            ISaleTypeRepository saleTypeRepository,
            IPropertyService propertyService,
            IMapper mapper
        )
        : base(saleTypeRepository, mapper)
        {
            this.saleTypeRepository = saleTypeRepository;
            this.propertyService = propertyService;
            this.mapper = mapper;
        }

        public async Task<List<SaleTypeViewModel>> GetAllViewModelWithInclude()
        {
            var propertyTypelist = await saleTypeRepository.GetAllWithIncludeAsync(new List<string> { "Properties" });
            return propertyTypelist.Select(s => new SaleTypeViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                CuantitySalesAssigned = s.Properties.Count()
            }).ToList();
        }

        public override async Task DeleteAsync(int id)
        {
            // Delete properties manually
            var properties = (await propertyService.GetAllAsync()).Where(p => p.TypeSaleId == id);
            foreach (var property in properties)
            {
                await propertyService.DeleteAsync(property.Id);
            }

            //Delete
            await base.DeleteAsync(id);
        }
    }
}
