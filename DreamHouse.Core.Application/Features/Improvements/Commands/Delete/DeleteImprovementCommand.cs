using AutoMapper;
using DreamHouse.Core.Application.Features.PropertyType.Commands.Delete;
using DreamHouse.Core.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.Core.Application.Features.Improvements.Commands.Delete
{
    public class DeleteImprovementCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteImprovementHandler : IRequestHandler<DeleteImprovementCommand, int>
    {
        private readonly IImprovementRepository improvementRepository;
        private readonly IMapper mapper;

        public DeleteImprovementHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            this.improvementRepository = improvementRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(DeleteImprovementCommand command, CancellationToken cancellationToken)
        {
            var category = await improvementRepository.GetByIdAsync(command.Id);
            if (category == null) throw new Exception("That Improvement doesnt exists");
            await improvementRepository.DeleteAsync(category);
            return category.Id;
        }
    }
}
