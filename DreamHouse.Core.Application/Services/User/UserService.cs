using AutoMapper;
using DreamHouse.Core.Application.Dtos.Account;
using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.ViewModels.Agent;
using DreamHouse.Core.Application.ViewModels.Auth;
using DreamHouse.Core.Application.ViewModels.User;

namespace DreamHouse.Core.Application.Services.User
{
    public class UserService : IUserService
    {
        protected readonly IAccountService _accountService;
        protected readonly IMapper _mapper;
        public UserService(IAccountService _accountService, IMapper _mapper)
        {
            this._accountService = _accountService;
            this._mapper = _mapper;
        }

        public async Task<bool> DuplicateUserName(string userName)
        {
            return await _accountService.DuplicateUserName(userName);
        }

        public async Task<bool> DuplicateEmail(string email)
        {
            return await _accountService.DuplicateEmail(email);
        }

        public async Task<List<UserViewModel>> GetAdmins()
        {
            return (await GetAllAsync()).Where(user => user.Roles[0] == ERoles.ADMIN.ToString()).ToList();
        }

        public async Task<List<UserViewModel>> GetDevelopers()
        {
            return (await GetAllAsync()).Where(user => user.Roles[0] == ERoles.DEVELOPER.ToString()).ToList();
        }
        public async Task<List<AgentViewModel>> GetAgentsWithInlude()
        {
            return _mapper.Map<List<AgentViewModel>>((await GetAllAsync()).Where(user => user.Roles[0] == ERoles.AGENT.ToString()).ToList());
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var usersResponse = await _accountService.GetAllAsync();
            var usersReturn = _mapper.Map<IEnumerable<UserViewModel>>(usersResponse);
            return usersReturn;
        }
        public async Task<UserSaveViewModel> FindyByIdAsync(string id)
        {
            AuthenticationResponse response = await _accountService.FindByIdAsync(id);
            UserSaveViewModel user = _mapper.Map<UserSaveViewModel>(response);
            return user;
        }

        public async Task<RegisterResponse> RegisterAsync(UserSaveViewModel vm)
        {

            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            RegisterResponse registerResponse = await _accountService.RegisterUserAsync(registerRequest);
            return registerResponse;
        }

        public async Task<UserSaveViewModel> UpdateUserAsync(UserSaveViewModel saveUserViewModel)
        {
            AuthenticationResponse response = _mapper.Map<AuthenticationResponse>(saveUserViewModel);
            response = await _accountService.UpdateUserAsync(response);
            UserSaveViewModel userUpdated = _mapper.Map<UserSaveViewModel>(response);
            return userUpdated;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest authenticationRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse authenticationResponse = await _accountService.AuthenticateAsync(authenticationRequest);
            return authenticationResponse;
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }


    }
}
