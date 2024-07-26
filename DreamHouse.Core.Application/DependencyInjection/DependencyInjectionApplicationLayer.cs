﻿using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Helpers;
using DreamHouse.Core.Application.Interfaces.Services.Commons;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.Services.Commons;
using DreamHouse.Core.Application.Services.User;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DreamHouse.Core.Application.DependencyInjection
{
    public static class DependencyInjectionApplicationLayer
    {
        public static void AddApplicationDependency(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IJsonHelper, JsonHelper>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserValidationService, UserValidationService>();
        }
    }
}
