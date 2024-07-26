using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Domain.Entities;
using DreamHouse.Infrastructure.Persistence.Contexts;

namespace DreamHouse.Infrastructure.Persistence.Repositories
{
    public class PropertyFavoriteRepository :GenericRepository<PropertyFavoriteEntity>,IPropertyFavoriteRepository
    {
        private readonly ApplicationContext context;

        public PropertyFavoriteRepository(ApplicationContext context) :base(context)
        {
            this.context = context;
        }
    }
}
