using DreamHouse.Core.Application.Enums;
using DreamHouse.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace DreamHouse.Infrastructure.Identity.Seeds.Users
{
    public class DefaultClientUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            List<ApplicationUser> clients = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "CCCCC-vxztp-yub64-qm7fr-1298z",
                    UserName = "client1",
                    Email = "client1@email.com",
                    FirstName = "client",
                    LastName = "client",
                    IdCard = "402-402-4002",
                    Status = (int)EUserStatus.ACTIVE,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                },
                    new ApplicationUser
                    {
                    Id = "DDDDD-vxztp-yub64-qm7fr-1298z",
                    UserName = "client2",
                    Email = "client2@email.com",
                    FirstName = "client",
                    LastName = "client",
                    IdCard = "402-402-4002",
                    Status = (int)EUserStatus.ACTIVE,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                },
                    {
                    new ApplicationUser
                    {
                    Id = "FFFFF-vxztp-yub64-qm7fr-1298z",
                    UserName = "client3",
                    Email = "client3@email.com",
                    FirstName = "client",
                    LastName = "client",
                    IdCard = "402-402-4002",
                    Status = (int)EUserStatus.ACTIVE,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                }
                }
            };
            foreach (var defaultUser in clients)
            {
                var client = await userManager.FindByEmailAsync(defaultUser.Email);

                if (client == null)
                {
                    // Si el usuario no existe, créalo
                    await userManager.CreateAsync(defaultUser, "123Pa$$Word!");
                    await userManager.AddToRoleAsync(defaultUser, ERoles.CLIENT.ToString());
                }
            }
        }
    }
}
