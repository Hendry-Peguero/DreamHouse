using DreamHouse.Core.Application.Interfaces.Repositories;
using DreamHouse.Core.Domain.Entities;
using DreamHouse.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamHouse.Infrastructure.Persistence.Repositories
{
    public class SaleTypeRepository : GenericRepository<SaleTypeEntity>, ISaleTypeRepository
    {
        private readonly ApplicationContext context;

        public SaleTypeRepository(ApplicationContext context) : base(context)
        {
            this.context = context;
        }
    }
}
