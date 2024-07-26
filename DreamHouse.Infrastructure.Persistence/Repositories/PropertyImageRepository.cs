using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Domain.Entities;
using DreamHouse.Infrastructure.Persistence.Contexts;

namespace DreamHouse.Infrastructure.Persistence.Repositories
{
    public class PropertyImageRepository : GenericRepository<PropertyImageEntity>, IPropertyImageRepository
    {
        private readonly ApplicationContext context;

        public PropertyImageRepository( ApplicationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
