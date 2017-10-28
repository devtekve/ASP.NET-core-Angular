﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Data;
using Vega.Models;
using Vega.Models.Resources;

namespace Vega.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Features")]
    public class FeaturesController : Controller
    {
        private readonly VegaDbContext _context;

        public FeaturesController(VegaDbContext context)
        {
            _context = context;
        }


        // GET: api/Models
        [HttpGet]
        public IEnumerable<FeatureResource> GetFeatures()
        {
            var x = _context.Features.ToList();
            return Mapper.Map<List<Feature>,List<FeatureResource>>(x);
        }
    }
}
