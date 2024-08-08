using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.ViewModels.User;
using DreamHouse.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using QuickBank.Core.Application.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

            if (string.IsNullOrEmpty(usvm.Email))
            {
                errors.Add("InvalidEmail", "Email cannot be empty");
            }
            else if (await IsEmailRegisteredAsync(usvm.Email))
            {
                errors.Add("EmailExists", "Email is already registered");
            }

            #endregion

            #region UserName Validation

            if (string.IsNullOrEmpty(usvm.UserName))
            {
                errors.Add("InvalidUserName", "Username cannot be empty");
            }
            else if (await IsUserNameRegisteredAsync(usvm.UserName))
            {
                errors.Add("UserNameExists", "Username is already registered");
            }

            #endregion

            #region Password Validation

            var lowerCasePattern = @"[a-z]";
            var upperCasePattern = @"[A-Z]";
            var hasNumberPattern = @"\d";
            var nonAlphanumericPattern = @"\W";

            if (string.IsNullOrEmpty(usvm.Password))
            {
                errors.Add("PasswordRequired", "Password is required");
            }
            else
            {
                // Check if password meets the minimum length requirement
                if (usvm.Password.Length < BusinessLogicConstantsHelper.MinPasswordLength)
                {
                    errors.Add("MinPasswordLength", $"The minimum length is {BusinessLogicConstantsHelper.MinPasswordLength}");
                }

                // Check if password contains at least one lowercase letter
                if (!Regex.IsMatch(usvm.Password, lowerCasePattern))
                {
                    errors.Add("LowerCase", "Password must contain at least one lowercase letter");
                }

                // Check if password contains at least one uppercase letter
                if (!Regex.IsMatch(usvm.Password, upperCasePattern))
                {
                    errors.Add("UpperCase", "Password must contain at least one uppercase letter");
                }

                // Check if password contains at least one digit
                if (!Regex.IsMatch(usvm.Password, hasNumberPattern))
                {
                    errors.Add("RequireDigit", "Password needs digits [1234567890]");
                }

                // Check if password contains at least one non-alphanumeric character
                if (!Regex.IsMatch(usvm.Password, nonAlphanumericPattern))
                {
                    errors.Add("RequireNonAlphanumeric", "Password must contain special characters [_,#@$]");
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

            #region Photo

            if (string.IsNullOrEmpty(usvm.ImageUrl))
                errors.Add("InvalidImageUrl", "Post a profile photo");

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
    }
}
