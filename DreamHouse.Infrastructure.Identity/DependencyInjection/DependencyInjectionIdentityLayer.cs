using DreamHouse.Core.Application.Dtos.Token;
using DreamHouse.Core.Application.Helpers.FOR_DELETE;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Domain.Settings;
using DreamHouse.Infrastructure.Identity.Context;
using DreamHouse.Infrastructure.Identity.Entities;
using DreamHouse.Infrastructure.Identity.Seeds;
using DreamHouse.Infrastructure.Identity.Seeds.Users;
using DreamHouse.Infrastructure.Identity.Services;
using DreamHouse.Infrastructure.Persistence.Seeds.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace DreamHouse.Infrastructure.Identity.DependencyInjection
{
    public static class DependencyInjectionIdentityLayer
    {
        public static void AddIdentityDependencyApi(this IServiceCollection services, IConfiguration configuration)
        {
            ContextConfiguration(services, configuration);

            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Authorization/Login";
                options.AccessDeniedPath = "/Authorization/AccessDenied";
            });

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, ErrorDescription = "Arent authorized" });
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse { HasError = true, ErrorDescription = "Arent authorized to access these resources" });
                        return c.Response.WriteAsync(result);
                    }

                };
            });
            #endregion

            ServiceConfiguration(services);
        }
        public static void AddIdentityDependencyWeb(this IServiceCollection services, IConfiguration configuration)
        {
            ContextConfiguration(services, configuration);

            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Home/HomeBasic";
                options.AccessDeniedPath = "/Authorization/AccessDenied";
            });

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            services.AddAuthentication();
            #endregion

            ServiceConfiguration(services);
        }

        #region "Private methods"

        private static void ContextConfiguration(IServiceCollection services, IConfiguration configuration)
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
        }

        private static void ServiceConfiguration(IServiceCollection services)
        {
            #region Services
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IRegisterValidationService, RegisterValidationService>();
            #endregion
        }

        #endregion


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
