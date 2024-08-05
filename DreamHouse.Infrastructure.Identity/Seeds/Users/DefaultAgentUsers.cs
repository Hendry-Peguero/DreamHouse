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
                    ImageUrl = "https://www.shutterstock.com/image-photo/portrait-confident-man-arms-crossed-260nw-2333089669.jpg",
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
                    ImageUrl ="https://c0.klipartz.com/pngpicture/40/822/gratis-png-persona-con-camisa-de-vestir-gris-hombre-mostrando.png",
                    Status = (int)EUserStatus.ACTIVE,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "sa12E-vxztp-yub64-qm7fr-1298z",
                    UserName = "agent3",
                    Email = "agent3@email.com",
                    FirstName = "agent",
                    LastName = "agent",
                    IdCard = "402-402-4023",
                    ImageUrl ="https://c0.klipartz.com/pngpicture/40/822/gratis-png-persona-con-camisa-de-vestir-gris-hombre-mostrando.png",
                    Status = (int)EUserStatus.ACTIVE,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "sasa-vxztp-yub64-qm7fr-1298z",
                    UserName = "agent4",
                    Email = "agent4@email.com",
                    FirstName = "agent",
                    LastName = "agent",
                    IdCard = "402-402-4026",
                    ImageUrl ="https://c0.klipartz.com/pngpicture/40/822/gratis-png-persona-con-camisa-de-vestir-gris-hombre-mostrando.png",
                    Status = (int)EUserStatus.ACTIVE,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "sas2a-vxztp-yub64-qm7fr-1298z",
                    UserName = "agent5",
                    Email = "agent5@email.com",
                    FirstName = "agent5",
                    LastName = "agent",
                    IdCard = "402-402-4926",
                    ImageUrl ="https://c0.klipartz.com/pngpicture/40/822/gratis-png-persona-con-camisa-de-vestir-gris-hombre-mostrando.png",
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
