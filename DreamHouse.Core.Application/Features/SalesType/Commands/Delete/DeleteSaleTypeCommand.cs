using AutoMapper;
using DreamHouse.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.Core.Application.Features.PropertyType.Commands.Delete
{
    public class DeleteSaleTypeCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteSaleTypeCommandHandler : IRequestHandler<DeleteSaleTypeCommand, int>
    {
        private readonly ISaleTypeRepository saleTypeRepository;
        private readonly IMapper mapper;

        public DeleteSaleTypeCommandHandler(ISaleTypeRepository saleTypeRepository, IMapper mapper)
        {
            this.saleTypeRepository = saleTypeRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(DeleteSaleTypeCommand command, CancellationToken cancellationToken)
        {
            var category = await saleTypeRepository.GetByIdAsync(command.Id);
            if (category == null) throw new Exception("That propertyType doesnt exists");
            await saleTypeRepository.DeleteAsync(category);
            return category.Id;
        }
    }
}
