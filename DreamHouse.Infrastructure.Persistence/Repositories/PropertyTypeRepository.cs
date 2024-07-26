using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Domain.Entities;
using DreamHouse.Infrastructure.Persistence.Contexts;

namespace DreamHouse.Infrastructure.Persistence.Repositories
{
    public class PropertyTypeRepository : GenericRepository<PropertyTypeEntity>, IPropertyTypeRepository
    {
        private readonly ApplicationContext context;

        public PropertyTypeRepository(ApplicationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
