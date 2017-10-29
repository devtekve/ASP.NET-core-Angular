using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Data;
using Vega.Models;
using Vega.Models.Resources;

namespace Vega.Controllers.API
{
    [Route("api/Vehicles")]
    public class VehiclesController : Controller
    {
        public VegaDbContext Context { get; }
        public IVehicleRepository Repository { get; }

        public VehiclesController(VegaDbContext context, IVehicleRepository repository)
        {
            Context = context;
            Repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await Repository.GetVehicle(id);
            if (vehicle == null)
                return NotFound(id);

            var VehicleResource = Mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(VehicleResource);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource VehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = Mapper.Map<SaveVehicleResource, Vehicle>(VehicleResource);
            vehicle.LastUpdated = DateTime.Now;
            Context.Vehicles.Add(vehicle);
            await Context.SaveChangesAsync();

            vehicle = await Repository.GetVehicle(vehicle.Id);
            var result = Mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource VehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await Repository.GetVehicle(id);
            if (vehicle == null)
                return NotFound(id);

            Mapper.Map<SaveVehicleResource, Vehicle>(VehicleResource, vehicle);
            vehicle.LastUpdated = DateTime.Now;
            await Context.SaveChangesAsync();

            var result = Mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await Context.Vehicles.FindAsync(id);
            if (vehicle == null)
                return NotFound(id);

            Context.Remove(vehicle);
            await Context.SaveChangesAsync();

            var result = Mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(id);
        }

    }
}
