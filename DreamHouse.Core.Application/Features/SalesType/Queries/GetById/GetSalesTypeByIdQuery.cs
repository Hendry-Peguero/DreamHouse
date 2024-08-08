using AutoMapper;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using DreamHouse.Core.Application.ViewModels.SaleType;
using MediatR;

namespace DreamHouse.Core.Application.Features.PropertyType.Queries.GetByIdQuery
{
    public class GetSalesTypeByIdQuery : IRequest<SaleTypeViewModel>
    {
        public int Id { get; set; }
    }

    public class GetSalesTypeByIdQueryHandler : IRequestHandler<GetSalesTypeByIdQuery, SaleTypeViewModel>
    {
        private readonly ISaleTypeRepository saleTypeRepository;
        private readonly IMapper mapper;

        public GetSalesTypeByIdQueryHandler(ISaleTypeRepository saleTypeRepository,
            IMapper mapper)
        {
            this.saleTypeRepository = saleTypeRepository;
            this.mapper = mapper;
        }

        public async Task<SaleTypeViewModel> Handle(GetSalesTypeByIdQuery query, CancellationToken cancellationToken)
        {
            var saleTypelist = await saleTypeRepository.GetAllWithIncludeAsync(new List<string> { "Properties" });
            var saleType = saleTypelist.FirstOrDefault(saleType => saleType.Id == query.Id);

            if (saleType == null) throw new Exception("saleType not found");

            var saleTypeVm = mapper.Map<SaleTypeViewModel>(saleType);
            saleTypeVm.CuantitySalesAssigned = saleType.Properties.Count;
            return saleTypeVm;
        }
    }
}
