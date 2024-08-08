using AutoMapper;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Application.ViewModels.SaleType;
using MediatR;

namespace DreamHouse.Core.Application.Features.PropertyType.Queries.GetAllQuery
{
    public class GetAllSalesTypeQuery : IRequest<List<SaleTypeViewModel>>
    {
    }

    public class GetAllSalesTypeQueryHandler : IRequestHandler<GetAllSalesTypeQuery, List<SaleTypeViewModel>>
    {
        private readonly ISaleTypeRepository saleTypeRepository;
        private readonly IMapper mapper;

        public GetAllSalesTypeQueryHandler(ISaleTypeRepository saleTypeRepository,
            IMapper mapper)
        {
            this.saleTypeRepository = saleTypeRepository;
            this.mapper = mapper;
        }

        public async Task<List<SaleTypeViewModel>> Handle(GetAllSalesTypeQuery query, CancellationToken cancellationToken)
        {
            var propertiesVm = await GetAllViewModelWithInclude();
            if (propertiesVm == null) throw new Exception("saleType not found");

            return propertiesVm;
        }

        private async Task<List<SaleTypeViewModel>> GetAllViewModelWithInclude()
        {
            var salesTypelist = await saleTypeRepository.GetAllWithIncludeAsync(new List<string> { "Properties" });

            return salesTypelist.Select(propertyType => new SaleTypeViewModel
            {
                Name = propertyType.Name,
                Description = propertyType.Description,
                Id = propertyType.Id,
                CuantitySalesAssigned = propertyType.Properties.Count()
            }).ToList();
        }
    }
}
