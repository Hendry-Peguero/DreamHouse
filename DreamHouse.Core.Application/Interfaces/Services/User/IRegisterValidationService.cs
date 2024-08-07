using DreamHouse.Core.Application.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DreamHouse.Core.Application.Interfaces.Services
{
    public interface IRegisterValidationService
    {
        Task<Dictionary<string, string>> ValidateUserRegistrationAsync(UserSaveViewModel usvm);
    }
}