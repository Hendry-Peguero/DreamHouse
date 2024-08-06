using DreamHouse.Core.Application.Helpers;
using DreamHouse.Core.Application.Interfaces.Helpers;
using DreamHouse.Core.Application.Interfaces.Services;
using DreamHouse.Core.Application.Interfaces.Services.Commons;
using DreamHouse.Core.Application.Interfaces.Services.User;
using DreamHouse.Core.Application.Interfaces.Services.Validations;
using DreamHouse.Core.Application.Services;
using DreamHouse.Core.Application.Services.Commons;
using DreamHouse.Core.Application.Services.User;
using DreamHouse.Core.Application.Services.Validations;
using DreamHouse.Core.Application.ViewModels.SaleType;
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
            services.AddTransient<IUserHelper, UserHelper>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IUserValidationService, UserValidationService>();
            services.AddTransient<IPropertyTypeService, PropertyTypeService>();
            services.AddTransient<ISaleTypeService, SaleTypeService>();
            services.AddTransient<IImprovementService, ImprovementService>();
            services.AddTransient<IPropertyTypeValidationService, PropertyTypeValidationService>();
            services.AddTransient<ISalesTypeValidationService, SalesTypeValidationService>();
            services.AddTransient<IImprovementValidationService, ImprovementValidationService>();
            
        }
    }
}
