using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Interfaces.Services.AdminHome;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.ViewModels.Home;

namespace DreamHouse.Core.Application.Services.AdminHome
{
    public class AdminHomeService : IAdminHomeService
    {
        private readonly IUserService userService;
        private readonly IPropertyService propertyService;

        public AdminHomeService(IUserService userService,
            IPropertyService propertyService)
        {
            this.userService = userService;
            this.propertyService = propertyService;
        }

        public async Task<int> GetActiveAgents()
        {
            return (await userService.GetAllAsync()).Where(agent => agent.Roles[0] == ERoles.AGENT.ToString() && agent.Status == (int)EUserStatus.ACTIVE).Count();
        }

        public async Task<int> GetInactiveAgents()
        {
            return (await userService.GetAllAsync()).Where(agent => agent.Roles[0] == ERoles.AGENT.ToString() && agent.Status == (int)EUserStatus.INACTIVE).Count();
        }

        public async Task<int> GetActiveClients()
        {
            return (await userService.GetAllAsync()).Where(agent => agent.Roles[0] == ERoles.CLIENT.ToString() && agent.Status == (int)EUserStatus.ACTIVE).Count();
        }

        public async Task<int> GetInactiveClients()
        {
            return (await userService.GetAllAsync()).Where(agent => agent.Roles[0] == ERoles.CLIENT.ToString() && agent.Status == (int)EUserStatus.INACTIVE).Count();
        }

        public async Task<int> GetActiveDevelopers()
        {
            return (await userService.GetAllAsync()).Where(agent => agent.Roles[0] == ERoles.DEVELOPER.ToString() && agent.Status == (int)EUserStatus.ACTIVE).Count();
        }

        public async Task<int> GetInactiveDevelopers()
        {
            return (await userService.GetAllAsync()).Where(agent => agent.Roles[0] == ERoles.DEVELOPER.ToString() && agent.Status == (int)EUserStatus.INACTIVE).Count();
        }

        public async Task<int> GetPropertyCuantityRegistered()
        {
            return (await propertyService.GetAllAsync()).Count();
        }

        public async Task<AdminHomeViewModel> DisplayValuesHome()
        {
            var adminHomeVm = new AdminHomeViewModel()
            {
                CuantityPropertyResgistered = await GetPropertyCuantityRegistered(),
                ActiveAgents = await GetActiveAgents(),
                InactiveAgents = await GetInactiveAgents(),
                ActiveClients = await GetActiveClients(),
                InactiveClients = await GetInactiveClients(),
                ActiveDevelopers = await GetActiveDevelopers(),
                InactiveDevelopers = await GetInactiveDevelopers(),
            };
            
            return adminHomeVm;
        }
    }
}
