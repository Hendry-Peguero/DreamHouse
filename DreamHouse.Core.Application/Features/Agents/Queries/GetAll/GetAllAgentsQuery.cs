using AutoMapper;
using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.ViewModels.Agent;
using DreamHouse.Core.Application.ViewModels.Property;
using DreamHouse.Core.Application.ViewModels.PropertyType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.Core.Application.Features.Properties.Queries.GetAllProperties
{
    public class GetAllAgentsQuery : IRequest<List<AgentViewModel>>
    {
    }

    public class GetAllAgentsQueryHandler : IRequestHandler<GetAllAgentsQuery, List<AgentViewModel>>
    {
        private readonly IAccountService accountService;
        private readonly IMapper mapper;
        private readonly IPropertyService propertyService;

        public GetAllAgentsQueryHandler( IAccountService accountService
            , IMapper mapper,
            IPropertyService propertyService)
        {
            this.accountService = accountService;
            this.mapper = mapper;
            this.propertyService = propertyService;
        }

        public async Task<List<AgentViewModel>> Handle(GetAllAgentsQuery query, CancellationToken cancellationToken)
        {

            var agentsVm = (await accountService.GetAllAsync()).Where(user => user.Roles[0] == ERoles.AGENT.ToString());
            if (agentsVm == null) throw new Exception("There are not agents");

            return agentsVm.Select(agent => new AgentViewModel
            {
                Id = agent.Id,
                FirstName = agent.FirstName,
                LastName = agent.LastName,
                Email = agent.Email,
                IdCard = agent.IdCard,
                UserName = agent.UserName,
                ImageUrl = agent.ImageUrl,
                Status = agent.Status,
                Roles = agent.Roles,
                PhoneNumber = agent.PhoneNumber,
                NumberPropertiesAssigned = propertyService.GetAllFromAgentAsync(agent.Id).Result
            }).ToList();
        }
    }

}
