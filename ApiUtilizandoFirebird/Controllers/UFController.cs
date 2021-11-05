using ApiUtilizandoFirebird.Persistencia.Contexto;
using ApiUtilizandoFirebird.Persistencia.Database.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiUtilizandoFirebird.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UFController : ControllerBase
    {
        private readonly Contexto _context;
        public UFController(Contexto context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<UF> Get()
        {
            return _context.Set<UF>().ToList();
        }
    }
}
