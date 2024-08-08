using AutoMapper;
using DreamHouse.Core.Application.Features.PropertyType.Commands.Update;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Domain.Entities;
using MediatR;

namespace DreamHouse.Core.Application.Features.PropertyType.Commands.Create
{
    public class UpdateSaleTypeCommand : IRequest<UpdateSaleTypeResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateSaleTypeCommandHandler : IRequestHandler<UpdateSaleTypeCommand, UpdateSaleTypeResponse>
    {
        private readonly ISaleTypeRepository saleTypeRepository;
        private readonly IMapper mapper;

        public UpdateSaleTypeCommandHandler(ISaleTypeRepository saleTypeRepository,
            IMapper mapper)
        {
            this.saleTypeRepository = saleTypeRepository;
            this.mapper = mapper;
        }

        public async Task<UpdateSaleTypeResponse> Handle(UpdateSaleTypeCommand command, CancellationToken cancellationToken)
        {
            var saleTypeEntity = mapper.Map<SaleTypeEntity>(command);
            var saleTypeResponse = mapper.Map<UpdateSaleTypeResponse>(await saleTypeRepository.UpdateAsync(saleTypeEntity, saleTypeEntity.Id));
            return saleTypeResponse;
        }
    }
}
