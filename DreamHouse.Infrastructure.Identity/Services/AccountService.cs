using AutoMapper;
using DreamHouse.Core.Application.Dtos.Account;
using DreamHouse.Core.Application.Dtos.Token;
using DreamHouse.Core.Application.Enums;
using DreamHouse.Core.Application.Interfaces.Services.Facilities;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Domain.Settings;
using DreamHouse.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DreamHouse.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly JWTSettings jWTSettings;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            IEmailService emailService,
            IOptions<JWTSettings> JWTSettings
        )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.emailService = emailService;
            jWTSettings = JWTSettings.Value;
        }

        public async Task<IEnumerable<AuthenticationResponse>> GetAllAsync()
        {
            var applicationUsers = userManager.Users.ToList();
            var authResponses = new List<AuthenticationResponse>();

            foreach (var user in applicationUsers)
            {
                var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);
                var response = mapper.Map<AuthenticationResponse>(user);
                response.Roles = rolesList.ToList();
                authResponses.Add(response);
            }
            return authResponses;
        }

        public async Task<AuthenticationResponse> FindByIdAsync(string userId)
        {
            var applicationUser = await userManager.FindByIdAsync(userId);
            var response = mapper.Map<AuthenticationResponse>(applicationUser);
            response.Roles = (await userManager.GetRolesAsync(applicationUser!).ConfigureAwait(false)).ToList();

            return response;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var responseWithErrors = new AuthenticationResponse();

            // Intentar buscar por Email
            var applicationUser = await userManager.FindByEmailAsync(request.Email);

            // Si no se encuentra por Email, intentar buscar por UserName
            if (applicationUser == null)
            {
                applicationUser = await userManager.FindByNameAsync(request.Email);
            }

            // Si aún no se encuentra, devolver error
            if (applicationUser == null)
            {
                responseWithErrors.HasError = true;
                responseWithErrors.ErrorDescription = $"No accounts with this {request.Email}";
                return responseWithErrors;
            }

            // Verificar credenciales
            var resultCredential = await signInManager.PasswordSignInAsync(applicationUser.UserName, request.Password, false, false);
            if (!resultCredential.Succeeded)
            {
                responseWithErrors.HasError = true;
                responseWithErrors.ErrorDescription = $"Invalid credentials for {applicationUser.UserName}";
                return responseWithErrors;
            }

            // Verificar si el correo electrónico está confirmado
            if (!applicationUser.EmailConfirmed)
            {
                responseWithErrors.HasError = true;
                responseWithErrors.ErrorDescription = $"Account not confirmed for {applicationUser.UserName}";
                return responseWithErrors;
            }

            // Verificar si la cuenta está activa
            if (applicationUser.Status == (int)EUserStatus.INACTIVE)
            {
                responseWithErrors.HasError = true;
                responseWithErrors.ErrorDescription = $"Account inactive for {applicationUser.UserName}, please communicate with the admin";
                return responseWithErrors;
            }

            // Si no hay errores, construir la respuesta con datos
            var responseWithData = mapper.Map<AuthenticationResponse>(applicationUser);
            responseWithData.Roles = (await userManager.GetRolesAsync(applicationUser).ConfigureAwait(false)).ToList();

            // Generar JWT
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(applicationUser);
            responseWithData.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            // Generar Refresh Token
            var refreshTokenObject = GenerateRefreshToken();
            responseWithData.RefreshToken = refreshTokenObject.Token;

            return responseWithData;
        }


        #region privates
        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();
            foreach (var role in userRoles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userId",user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmectricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTSettings.Key));

            var signinCredentials = new SigningCredentials(symmectricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jWTSettings.Issuer,
                audience: jWTSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jWTSettings.DurationInMinutes),
                signingCredentials: signinCredentials
                );

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken()
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
            };
        }
        #endregion

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new Byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return BitConverter.ToString(randomBytes).Replace("-", "");
        }


        public async Task<bool> DuplicateUserName(string userName)
        {
            return (await userManager.FindByNameAsync(userName)) != null;
        }

        public async Task<bool> DuplicateEmail(string email)
        {
            return (await userManager.FindByEmailAsync(email)) != null;
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request)
        {
            // Resources
            RegisterResponse response = new();
            ApplicationUser userToRegister = mapper.Map<ApplicationUser>(request);

            // Dafault values for user when is created
            userToRegister.EmailConfirmed = true;
            userToRegister.PhoneNumberConfirmed = true;
            userToRegister.Status = (int)EUserStatus.ACTIVE;

            // Try to create the user
            var resultCreation = await userManager.CreateAsync(userToRegister, request.Password);
            if (!resultCreation.Succeeded)
            {
                response.HasError = true;
                response.ErrorDescription = $"Has ocurred an error trying to save the user";
                return response;
            }

            // Set roles for created user
            await userManager.AddToRoleAsync(userToRegister, request.UserType.ToString());

            // Set id of user creatde to the response
            response.Id = userToRegister.Id;

            return response;
        }

        public async Task<AuthenticationResponse> UpdateUserAsync(AuthenticationResponse request)
        {
            // Resources
            var response = new AuthenticationResponse();
            ApplicationUser userToUpdate = await userManager.FindByIdAsync(request.Id);

            userToUpdate.Id = request.Id;
            userToUpdate.FirstName = request.FirstName;
            userToUpdate.LastName = request.LastName;
            userToUpdate.UserName = request.UserName;
            userToUpdate.IdCard = request.IdCard;
            userToUpdate.Status = request.Status;
            userToUpdate.Email = request.Email;
            userToUpdate.PhoneNumber = request.PhoneNumber;
            userToUpdate.ImageUrl = request.ImageUrl;

            // Try to update the user
            var resultUpdate = await userManager.UpdateAsync(userToUpdate);
            if (!resultUpdate.Succeeded)
            {
                response.HasError = true;
                response.ErrorDescription = "An ocurred an error updating the user try again";
                return response;
            }

            // Try to update the password
            if (request.Password != null)
            {
                var removePassword = await userManager.RemovePasswordAsync(userToUpdate);
                if (!removePassword.Succeeded)
                {
                    response.HasError = true;
                    response.ErrorDescription = "An ocurred an error removing the user password try again";
                    return response;
                }

                var addPassword = await userManager.AddPasswordAsync(userToUpdate, request.Password);
                if (!addPassword.Succeeded)
                {
                    response.HasError = true;
                    response.ErrorDescription = "An ocurred an adding removing the user password try again";
                    return response;
                }
            }

            // Fill the response with data
            response = mapper.Map<AuthenticationResponse>(userToUpdate);
            response.Roles = (await userManager.GetRolesAsync(userToUpdate).ConfigureAwait(false)).ToList();

            return response;
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task DeleteUserAsync(string id)
        {
            await userManager.DeleteAsync(await userManager.FindByIdAsync(id));
        }

        //metodo de prueba de registro de agente y usuario
        public async Task<RegisterResponse> RegisterUserAndagentAsync(RegisterRequest request, string origin)
        {
            // Resources
            RegisterResponse response = new()
            {
                HasError = false
            };

            ApplicationUser userToRegister = mapper.Map<ApplicationUser>(request);

            // Dafault values for user when is created
            userToRegister.EmailConfirmed = request.UserType!.ToString() == "AGENT" ? true : false;
            userToRegister.PhoneNumberConfirmed = true;
            userToRegister.Status = (int)EUserStatus.INACTIVE;

            // Try to create the user
            var resultCreation = await userManager.CreateAsync(userToRegister, request.Password);
            if (!resultCreation.Succeeded)
            {
                response.HasError = true;
                response.ErrorDescription = $"Has ocurred an error trying to save the user";
                return response;
            }

            // Set roles for created user
            await userManager.AddToRoleAsync(userToRegister, request.UserType.ToString());

            // Send a email if is user
            if (request.UserType.ToString() == "CLIENT")
            {
                var verificationUri = await SendVerificationEmailUri(userToRegister, origin);
                await emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
                {
                    To = userToRegister.Email,
                    Body = $@"<div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                        <h2 style='color: #2e6da4; text-align: center;'>Welcome to DreanHouse!</h2>
                        <p style='font-size: 16px; color: #333;'>Hi {userToRegister.FirstName} {userToRegister.LastName},</p>
                        <p style='font-size: 16px; color: #333;'>Thank you for registering at DreanHouse, the website where you can find the house of your dreams.</p>
                        <p style='font-size: 16px; color: #333;'>To complete your registration, please verify your email by clicking the button below:</p>
                        <div style='text-align: center; margin: 20px 0;'>
                            <a href='{verificationUri}' style='background-color: #2e6da4; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Verify Your Account</a>
                        </div>
                        <p style='font-size: 16px; color: #333;'>If you did not register for a DreanHouse account, please ignore this email.</p>
                        <p style='font-size: 16px; color: #333;'>Best regards,<br/>The DreanHouse Team</p>
                    </div>",
                    Subject = "Confirm Registration"
                });
            }

            // Set id of user creatde to the response
            response.Id = userToRegister.Id;

            return response;
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "Usuario no encontrado";
            }

            var result = await userManager.ConfirmEmailAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token)));
            if (result.Succeeded)
            {
                user.Status = (int)EUserStatus.ACTIVE;
                await userManager.UpdateAsync(user);
                return "Success";
            }

            return "Error al confirmar el correo electrónico";
        }

        private async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin)
        {
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "Authorization/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }
    }
}
