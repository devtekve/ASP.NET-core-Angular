using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Core;
using Vega.Core.Models;
using Vega.Persistence;
using Vega.Models;
using Vega.Models.Resources;

namespace Vega.Controllers.API
{
    [Route("api/Vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public VehiclesController(IMapper mapper, IVehicleRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id);
            if (vehicle == null)
                return NotFound(id);

            var VehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(VehicleResource);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource VehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(VehicleResource);
            vehicle.LastUpdated = DateTime.Now;


            repository.Add(vehicle);
            await unitOfWork.CompleteAsync();

            vehicle = await repository.GetVehicle(vehicle.Id);
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource VehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await repository.GetVehicle(id);
            if (vehicle == null)
                return NotFound(id);

            mapper.Map<SaveVehicleResource, Vehicle>(VehicleResource, vehicle);
            vehicle.LastUpdated = DateTime.Now;
            await unitOfWork.CompleteAsync();

            vehicle = await repository.GetVehicle(vehicle.Id);
            var result = mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id, includeRelated: false);
            if (vehicle == null)
                return NotFound(id);


            repository.Remove(vehicle);
            await unitOfWork.CompleteAsync();

            var result = mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(id);
        }

    }
}
