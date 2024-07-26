using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Domain.Entities;
using DreamHouse.Infrastructure.Persistence.Contexts;

namespace DreamHouse.Infrastructure.Persistence.Repositories
{
    public class ImprovementRepository : GenericRepository<ImprovementEntity>, IImprovementRepository
    {
        private readonly ApplicationContext context;

        public ImprovementRepository(ApplicationContext context) :base(context)
        {
            this.context = context;
        }
    }
}
