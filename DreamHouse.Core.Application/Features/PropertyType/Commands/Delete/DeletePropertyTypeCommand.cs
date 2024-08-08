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
    public class DeletePropertyTypeCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeletePropertyTypeCommandHandler : IRequestHandler<DeletePropertyTypeCommand, int>
    {
        private readonly IPropertyTypeRepository propertyTypeRepository;
        private readonly IMapper mapper;

        public DeletePropertyTypeCommandHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            this.propertyTypeRepository = propertyTypeRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(DeletePropertyTypeCommand command, CancellationToken cancellationToken)
        {
            var category = await propertyTypeRepository.GetByIdAsync(command.Id);
            if (category == null) throw new Exception("That propertyType doesnt exists");
            await propertyTypeRepository.DeleteAsync(category);
            return category.Id;
        }
    }
}
