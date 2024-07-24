using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.Core.Application.Interfaces.Services.User
{
    public interface IUserValidationService
    {
        Task<Dictionary<string, string>> UserSaveValidation(UserSaveViewModel userSaveViewModel);
        Task<Dictionary<string, string>> PasswordValidation(UserSaveViewModel userSaveViewModel);
        Task<Dictionary<string, string>> UserUpdateValidation(UserSaveViewModel userSaveViewModel);
    }
}
