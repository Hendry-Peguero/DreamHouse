using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Domain.Entities;
using DreamHouse.Infrastructure.Persistence.Contexts;

namespace DreamHouse.Infrastructure.Persistence.Repositories
{
    public class PropertyRepository : GenericRepository<PropertyEntity>, IPropertyRepository
    {
        private readonly ApplicationContext context;

        public PropertyRepository(ApplicationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
