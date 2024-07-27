using DreamHouse.Core.Application.Enums;
using DreamHouse.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace DreamHouse.Infrastructure.Identity.Seeds.Users
{
    public class DefaultDeveloperUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser defaultUser = new()
            {
                Id = "DDDDD-vxztp-yub64-qm7fr-1298z",
                UserName = "developer",
                Email = "developer1@email.com",
                FirstName = "developer",
                LastName = "developer",
                IdCard = "402-402-4002",
                Status = (int)EUserStatus.ACTIVE,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var developer = await userManager.FindByEmailAsync(defaultUser.Email);

            if (developer == null)
            {
                // Si el usuario no existe, créalo
                await userManager.CreateAsync(defaultUser, "123Pa$$Word!");
                await userManager.AddToRoleAsync(defaultUser, ERoles.DEVELOPER.ToString());
            }
        }
    }
}
