using AutoMapper;
using DreamHouse.Core.Application.Dtos.Account;
using DreamHouse.Core.Application.Dtos.Filters;
using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Interfaces.Helpers;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.ViewModels.Agent;
using DreamHouse.Core.Application.ViewModels.Auth;
using DreamHouse.Core.Application.ViewModels.User;

namespace DreamHouse.Core.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IImageHelper _imageHelper;
        private readonly IMapper _mapper;

        public UserService(
            IAccountService _accountService,
            IImageHelper _imageHelper,
            IMapper _mapper

        )
        {
            this._accountService = _accountService;
            this._imageHelper = _imageHelper;
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

        public async Task<List<AgentViewModel>> GetAgents()
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
            // Register
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterUserAsync(registerRequest);
        }

        public async Task<UserSaveViewModel> UpdateUserAsync(UserSaveViewModel saveUserViewModel)
        {
            AuthenticationResponse response = _mapper.Map<AuthenticationResponse>(saveUserViewModel);
            response = await _accountService.UpdateUserAsync(response);
            UserSaveViewModel userUpdated = _mapper.Map<UserSaveViewModel>(response);
            return userUpdated;
        }

        public async Task<UserSaveViewModel> UpdateAgentAsync(UserSaveViewModel saveUserViewModel)
        {
            // Update Image
            saveUserViewModel.ImageUrl = _imageHelper.UpdateImage(saveUserViewModel.File, saveUserViewModel.ImageUrl);

            return await UpdateUserAsync(saveUserViewModel);
        }

        public async Task<UserSaveViewModel> ChangeUserState(string id)
        {
            var user = await FindyByIdAsync(id);
            user.Status = (user.Status != (int)EUserStatus.ACTIVE) ? (int)EUserStatus.ACTIVE : (int)EUserStatus.INACTIVE;
            user = await UpdateUserAsync(user);
            user.UserType = user.Roles[0];
            return user;
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

        public async Task DeleteAsync(string id)
        {
            await _accountService.DeleteUserAsync(id);
        }

        public async Task<RegisterResponse> RegisterClienAndAgentAsync(UserSaveViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            RegisterResponse registerResponse = await _accountService.RegisterUserAndagentAsync(registerRequest, origin);

            // Create Image
            vm.ImageUrl = _imageHelper.SaveImage(vm.File, registerResponse.Id, EGroupImage.USERS);

            // Update User
            vm.Id = registerResponse.Id;
            await UpdateUserAsync(vm);

            return registerResponse;
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<List<AgentViewModel>> GetActiveAgents()
        {
            var allUsers = await GetAllAsync();
            var activeAgents = allUsers
                .Where(user => user.Roles.Contains(ERoles.AGENT.ToString()) && user.Status == 1)
                .ToList();

            return _mapper.Map<List<AgentViewModel>>(activeAgents);
        }

    }
}
