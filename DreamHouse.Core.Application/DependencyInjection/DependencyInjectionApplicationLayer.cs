using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Helpers;
using DreamHouse.Core.Application.Interfaces.Services.Commons;
using DreamHouse.Core.Application.Interfaces.Services.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.Core.Application.DependencyInjection
{
    public static class DependencyInjectionApplicationLayer
    {
        public static void AddApplicationDependency(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IJsonHelper, JsonHelper>();
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            
            services.AddTransient<IUserService, UserService>();
            
            services.AddTransient<IUserValidationService, UserValidationService>();

        }
    }
}
