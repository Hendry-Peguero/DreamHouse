//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using DreamHouse.Core.Application.Interfaces.Services.Facilities;
//using DreamHouse.Core.Domain.Settings;
//using DreamHouse.Infraestructure.Shared.Services;

//namespace DreamHouse.Infrastructure.Shared.DependencyInjection
//{
//    public static class DependencyInjectionSharedLayer
//    {
//        public static void AddSharedDependency(this IServiceCollection services, IConfiguration configuration)
//        {
//            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
//            services.AddTransient<IEmailService, EmailService>();
//        }
//    }
//}
