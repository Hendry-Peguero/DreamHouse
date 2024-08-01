using DreamHouse.Core.Application.Enums;
using DreamHouse.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace DreamHouse.Infrastructure.Identity.Seeds.Users
{
    public class DefaultAgentUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            List<ApplicationUser> agents = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "AAAAA-vxztp-yub64-qm7fr-1298z",
                    UserName = "agent1",
                    Email = "agent1@email.com",
                    FirstName = "agent",
                    LastName = "agent",
                    IdCard = "402-402-4002",
                    Status = (int)EUserStatus.ACTIVE,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "EEEEE-vxztp-yub64-qm7fr-1298z",
                    UserName = "agent2",
                    Email = "agent2@email.com",
                    FirstName = "agent",
                    LastName = "agent",
                    IdCard = "402-402-4003",
                    Status = (int)EUserStatus.ACTIVE,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                }
            };

            foreach (var defaultUser in agents)
            {
                var agent = await userManager.FindByEmailAsync(defaultUser.Email);

                if (agent == null)
                {
                    // Si el usuario no existe, créalo
                    await userManager.CreateAsync(defaultUser, "123Pa$$Word!");
                    await userManager.AddToRoleAsync(defaultUser, ERoles.AGENT.ToString());
                }
            }

        }
    }
}
