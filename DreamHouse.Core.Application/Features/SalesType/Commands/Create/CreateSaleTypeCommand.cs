using AutoMapper;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Domain.Entities;
using MediatR;

namespace DreamHouse.Core.Application.Features.PropertyType.Commands.Create
{
    public class CreateSaleTypeCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateSaleTypeCommandHandler : IRequestHandler<CreateSaleTypeCommand, int>
    {
        private readonly ISaleTypeRepository saleTypeRepository;
        private readonly IMapper mapper;

        public CreateSaleTypeCommandHandler(ISaleTypeRepository saleTypeRepository,
            IMapper mapper)
        {
            this.saleTypeRepository = saleTypeRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateSaleTypeCommand command, CancellationToken cancellationToken)
        {
            var saleTypeEntity = mapper.Map<SaleTypeEntity>(command);
            saleTypeEntity = await saleTypeRepository.AddAsync(saleTypeEntity);
            return saleTypeEntity.Id;
        }
    }
}
