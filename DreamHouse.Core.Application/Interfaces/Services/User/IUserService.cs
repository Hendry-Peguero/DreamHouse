using DreamHouse.Core.Application.Dtos.Account;
using DreamHouse.Core.Application.ViewModels.Agent;
using DreamHouse.Core.Application.ViewModels.Auth;
using DreamHouse.Core.Application.ViewModels.User;

namespace DreamHouse.Core.Application.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllAsync();
        Task<UserSaveViewModel> FindyByIdAsync(string id);
        Task<RegisterResponse> RegisterAsync(UserSaveViewModel vm);
        Task<UserSaveViewModel> UpdateUserAsync(UserSaveViewModel saveUserViewModel);
        Task<UserSaveViewModel> ChangeUserState(string id);
        Task SignOutAsync();
        Task DeleteAsync(string id);

        //Task<string> ConfirmEmailAsync(string userId, string token);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<bool> DuplicateUserName(string userName);
        Task<bool> DuplicateEmail(string email);
        Task<List<UserViewModel>> GetAdmins();
        Task<List<UserViewModel>> GetDevelopers();
        Task<List<AgentViewModel>> GetAgents();
    }
}
