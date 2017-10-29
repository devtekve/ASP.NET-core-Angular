using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Models;

namespace Vega.Data
{
    public class VehicleRepository : IVehicleRepository
    {
        public VegaDbContext Context { get; }
        public VehicleRepository(VegaDbContext context)
        {
            Context = context;
        }        

        public async Task<Vehicle> GetVehicle(int id)
        {
            return await Context.Vehicles
               .Include(v => v.Features)
                   .ThenInclude(vf => vf.Feature)
               .Include(v => v.Model)
                   .ThenInclude(m => m.Make)
               .SingleOrDefaultAsync(v => v.Id == id);
        }
    }
}
