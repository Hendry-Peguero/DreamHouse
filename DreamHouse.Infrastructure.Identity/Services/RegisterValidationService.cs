using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.ViewModels.User;
using DreamHouse.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamHouse.Infrastructure.Identity.Services
{
    public class RegisterValidationService : IRegisterValidationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterValidationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Dictionary<string, string>> ValidateUserRegistrationAsync(UserSaveViewModel usvm)
        {
            var errors = new Dictionary<string, string>();

            #region Email Validation

            bool emailIsNullOrEmpty = string.IsNullOrEmpty(usvm.Email);
            bool emailIsValid = !emailIsNullOrEmpty && new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(usvm.Email);

            if (emailIsNullOrEmpty) errors.Add("InvalidEmail", "Email cannot be empty");
            else if (!emailIsValid) errors.Add("InvalidEmailFormat", "Invalid email format");
            else if (await IsEmailRegisteredAsync(usvm.Email)) errors.Add("EmailExists", "Email is already registered");

            #endregion

            #region UserName Validation

            bool userNameIsNullOrEmpty = string.IsNullOrEmpty(usvm.UserName);
            if (userNameIsNullOrEmpty) errors.Add("InvalidUserName", "Username cannot be empty");
            else if (await IsUserNameRegisteredAsync(usvm.UserName)) errors.Add("UserNameExists", "Username is already registered");

            #endregion

            #region Password Validation

            bool passwordIsNullOrEmpty = string.IsNullOrEmpty(usvm.Password);
            if (passwordIsNullOrEmpty) errors.Add("InvalidPassword", "Password cannot be empty");
            else
            {
                var passwordErrors = await ValidatePasswordAsync(usvm.Password);
                foreach (var error in passwordErrors)
                {
                    errors.Add($"Password_{error}", error);
                }

                if (usvm.Password != usvm.ConfirmPassword)
                {
                    errors.Add("PasswordMismatch", "Passwords do not match");
                }
            }

            #endregion

            #region FirstName Validation

            if (string.IsNullOrEmpty(usvm.FirstName))
                errors.Add("InvalidFirstName", "First name cannot be empty");

            #endregion

            #region LastName Validation

            if (string.IsNullOrEmpty(usvm.LastName))
                errors.Add("InvalidLastName", "Last name cannot be empty");

            #endregion

            #region PhoneNumber Validation

            if (string.IsNullOrEmpty(usvm.PhoneNumber))
                errors.Add("InvalidPhoneNumber", "Phone number cannot be empty");

            #endregion

            #region IdCard Validation

            if (string.IsNullOrEmpty(usvm.IdCard))
                errors.Add("InvalidIdCard", "ID Card cannot be empty");

            #endregion

            #region UserType Validation

            if (string.IsNullOrEmpty(usvm.UserType))
                errors.Add("InvalidUserType", "User type must be selected");

            #endregion

            return errors;
        }

        private async Task<bool> IsEmailRegisteredAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        private async Task<bool> IsUserNameRegisteredAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName) != null;
        }

        private async Task<IEnumerable<string>> ValidatePasswordAsync(string password)
        {
            var passwordValidator = new PasswordValidator<ApplicationUser>();
            var result = await passwordValidator.ValidateAsync(_userManager, null, password);

            return result.Succeeded ? Enumerable.Empty<string>() : result.Errors.Select(e => e.Description);
        }
    }
}
