using Microsoft.AspNetCore.Identity;
//using DreamHouse.Core.Application.Enums;
using DreamHouse.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.Infrastructure.Persistence.Seeds.Users
{
    public class DefaultAdminUsers
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            //ApplicationUser defaultUser = new()
            //{
            //    Id = "g5k2l-vxztp-yub64-qm7fr-1298z",
            //    UserName = "admin",
            //    Email = "admin@email.com",
            //    FirstName = "admin",
            //    LastName = "admin",
            //    IdCard ="402-402-4002",
            //    Status = (int)EUserStatus.ACTIVE,
            //    EmailConfirmed = true,
            //    PhoneNumberConfirmed = true
            //};

            //var user = await userManager.FindByEmailAsync(defaultUser.Email);

            //if (user == null)
            //{
            //    // Si el usuario no existe, créalo
            //    await userManager.CreateAsync(defaultUser, "123Pa$$Word!");
            //    await userManager.AddToRoleAsync(defaultUser, ERoles.ADMIN.ToString());
            //}
        }
    }
}
