using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Domain.Entities;
using DreamHouse.Infrastructure.Persistence.Contexts;

namespace DreamHouse.Infrastructure.Persistence.Repositories
{
    public class PropertyImprovementRepository : GenericRepository<PropertyImprovementEntity>, IPropertyImprovementRepository
    {
        private readonly ApplicationContext context;

        public PropertyImprovementRepository( ApplicationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
