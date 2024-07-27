using DreamHouse.Core.Application.Helpers.FOR_DELETE;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Infrastructure.Identity.Context;
using DreamHouse.Infrastructure.Identity.Entities;
using DreamHouse.Infrastructure.Identity.Seeds;
using DreamHouse.Infrastructure.Identity.Seeds.Users;
using DreamHouse.Infrastructure.Identity.Services;
using DreamHouse.Infrastructure.Persistence.Seeds.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace DreamHouse.Infrastructure.Identity.DependencyInjection
{
    public static class DependencyInjectionIdentityLayer
    {
        public static void AddIdentityDependency(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.UseInMemoryDatabase("IdentityContextInMemory");
                });
            }
            else
            {
                var connectionString = configuration.GetConnection("IdentityConnection");

                services.AddDbContext<IdentityContext>(options =>
                {
                    options.UseSqlServer(connectionString, a => a.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                });
            }
            #endregion

            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                //falta el de acceso denegado

                options.AccessDeniedPath = "/Auth/AccessDenied";
            });

            services.AddAuthentication();
            #endregion

            #region Services
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IAccountService, AccountService>();
            #endregion
        }

        public static async Task AddIdentitySeeds(this IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    await DefaultRoles.SeedAsync(roleManager);

                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    await DefaultAdminUsers.SeedAsync(userManager);
                    await DefaultAgentUsers.SeedAsync(userManager);
                    await DefaultClientUsers.SeedAsync(userManager);
                    await DefaultDeveloperUsers.SeedAsync(userManager);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
