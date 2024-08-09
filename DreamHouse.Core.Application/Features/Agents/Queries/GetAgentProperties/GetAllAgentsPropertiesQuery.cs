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
    public class GetAllAgentsPropertiesQuery : IRequest<List<PropertyViewModel>>
    {
        public string Id { get; set; }
    }

    public class GetAllAgentsPropertiesQueryHandler : IRequestHandler<GetAllAgentsPropertiesQuery, List<PropertyViewModel>>
    {
        private readonly IAccountService accountService;
        private readonly IMapper mapper;
        private readonly IPropertyRepository propertyRepository;

        public GetAllAgentsPropertiesQueryHandler( IAccountService accountService
            , IMapper mapper,
            IPropertyRepository propertyRepository)
        {
            this.accountService = accountService;
            this.mapper = mapper;
            this.propertyRepository = propertyRepository;
        }

        public async Task<List<PropertyViewModel>> Handle(GetAllAgentsPropertiesQuery query, CancellationToken cancellationToken)
        {

            var agentsVm = (await accountService.GetAllAsync()).Where(user => user.Roles[0] == ERoles.AGENT.ToString());
            var agentEntity = agentsVm.FirstOrDefault(agent => agent.Id == query.Id);

            var propertiesVm = mapper.Map<List<PropertyViewModel>>(await propertyRepository.GetAllAsync());

            var propertiesFromAgent = propertiesVm.Where(property => property.AgentId == agentEntity.Id);
            
            if (propertiesFromAgent == null) throw new Exception("There are not properties related to this agent");
            
            return propertiesFromAgent.ToList();

        }
    }

}
