using DreamHouse.Core.Application.ViewModels.Home;

namespace DreamHouse.Core.Application.Interfaces.Services.AdminHome
{
    public interface IAdminHomeService
    {
        Task<AdminHomeViewModel> DisplayValuesHome();
        Task<int> GetPropertyCuantityRegistered();
        Task<int> GetActiveDevelopers();
        Task<int> GetInactiveDevelopers();
        Task<int> GetActiveAgents();
        Task<int> GetInactiveAgents();
        Task<int> GetActiveClients();
        Task<int> GetInactiveClients();
    }
}
