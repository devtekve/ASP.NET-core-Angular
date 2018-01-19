using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Core;

namespace Vega.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDbContext context;

        public UnitOfWork(VegaDbContext context)
        {
            this.context = context;
        }
        public Task CompleteAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
