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
    public class ChangeAgentStatusCommand : IRequest<string>
    {
        public string Id { get; set; }
    }

    public class ChangeAgentStatusCommandHandler : IRequestHandler<ChangeAgentStatusCommand, string>
    {
        private readonly IAccountService accountService;
        private readonly IMapper mapper;

        public ChangeAgentStatusCommandHandler( IAccountService accountService
            , IMapper mapper)
        {
            this.accountService = accountService;
            this.mapper = mapper;
        }

        public async Task<string> Handle(ChangeAgentStatusCommand command, CancellationToken cancellationToken)
        {

            var user = await accountService.FindByIdAsync(command.Id);
            if (user == null) throw new Exception("dont exist and agent with that id");
            
            user.Status = (user.Status != (int)EUserStatus.ACTIVE) ? (int)EUserStatus.ACTIVE : (int)EUserStatus.INACTIVE;
            user = await accountService.UpdateUserAsync(user);
            
            return user.Id;
        }
    }

}
