using System.Threading.Tasks;
using Vega.Models;

namespace Vega.Data
{
    public interface IVehicleRepository
    {
        VegaDbContext Context { get; }

        Task<Vehicle> GetVehicle(int id);
    }
}