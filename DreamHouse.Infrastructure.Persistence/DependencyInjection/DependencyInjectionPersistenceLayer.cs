using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DreamHouse.Infrastructure.Persistence.Contexts;
using DreamHouse.Infrastructure.Persistence.Repositories;


namespace DreamHouse.Infrastructure.Persistence.DependencyInjection
{
    public static class DependencyInjectionPersistenceLayer
    {
        public static void AddPersistenceDependency(this IServiceCollection services, IConfiguration configuration)
        {
            #region Database
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(
                    options => options.UseInMemoryDatabase("DbInMemory")
                );
            }
            else
            {
                string? connectionString = configuration.GetConnectionString("SqlServerConnection");

                services.AddDbContext<ApplicationContext>(
                    options => options.UseSqlServer(
                        connectionString,
                        m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)
                    )
                );
            }
            #endregion

            #region Repositories
            //services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion
        }
    }
}
