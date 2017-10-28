using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vega.Data;
using Vega.Models;
using Microsoft.EntityFrameworkCore;
using Vega.Models.Resources;

namespace Vega.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Models")]
    public class ModelsController : Controller
    {
        private readonly VegaDbContext _context;

        public ModelsController(VegaDbContext context)
        {
            _context = context;
        }


        // GET: api/Models
        [HttpGet]
        public IEnumerable<ModelResource> GetModels()
        {
            var x = _context.Models.ToList();
            return AutoMapper.Mapper.Map<List<Model>,List<ModelResource>>(x);
        }
    }
}