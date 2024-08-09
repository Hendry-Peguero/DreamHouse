using AutoMapper;
using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.Services;
using DreamHouse.Core.Application.ViewModels.Agent;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using MediatR;

namespace DreamHouse.Core.Application.Features.PropertyType.Queries.GetByIdQuery
{
    public class GetAgentByIdQuery : IRequest<AgentViewModel>
    {
        public string Id { get; set; }
    }

    public class GetAgentByIdQueryHandler : IRequestHandler<GetAgentByIdQuery, AgentViewModel>
    {
        private readonly IAccountService accountService;
        private readonly IPropertyService propertyService;
        private readonly IMapper mapper;

        public GetAgentByIdQueryHandler(IAccountService accountService,
            IPropertyService propertyService,
            IMapper mapper)
        {
            this.accountService = accountService;
            this.propertyService = propertyService;
            this.mapper = mapper;
        }

        public async Task<AgentViewModel> Handle(GetAgentByIdQuery query, CancellationToken cancellationToken)
        {
            var agentsVm = (await accountService.GetAllAsync()).Where(user => user.Roles[0] == ERoles.AGENT.ToString());
            var agentEntity = agentsVm.FirstOrDefault(agent => agent.Id == query.Id);

            if (agentEntity == null) throw new Exception("There are not agents");
            var agentVm = new AgentViewModel
            {
                Id = agentEntity.Id,
                FirstName = agentEntity.FirstName,
                LastName = agentEntity.LastName,
                Email = agentEntity.Email,
                IdCard = agentEntity.IdCard,
                UserName = agentEntity.UserName,
                ImageUrl = agentEntity.ImageUrl,
                Roles = agentEntity.Roles,
                Status = agentEntity.Status,
                NumberPropertiesAssigned = propertyService.GetAllFromAgentAsync(agentEntity.Id).GetAwaiter().GetResult()
            };

            return agentVm;
        }
    }
}
