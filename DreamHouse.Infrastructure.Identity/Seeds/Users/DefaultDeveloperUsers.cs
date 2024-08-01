using DreamHouse.Core.Application.Enums;
using DreamHouse.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace DreamHouse.Infrastructure.Identity.Seeds.Users
{
    public class DefaultDeveloperUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser defaultDeveloper = new()
            {
                Id = "HHHHH-vxztp-yub64-qm7fr-1298z",
                UserName = "developer1",
                Email = "developer1@email.com",
                FirstName = "developer",
                LastName = "developer",
                IdCard = "402-402-4008",
                Status = (int)EUserStatus.ACTIVE,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var developer = await userManager.FindByEmailAsync(defaultDeveloper.Email);

            if (developer == null)
            {
                // Si el usuario no existe, créalo
                await userManager.CreateAsync(defaultDeveloper, "123Pa$$Word!");
                await userManager.AddToRoleAsync(defaultDeveloper, ERoles.DEVELOPER.ToString());
            }
        }
    }
}
