using APICatalogo.Context;
using APICatalogo.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly APICatalogoContext _Context;

        public SuppliersController(APICatalogoContext context)
        {
            _Context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Supplier>> Get()
        {
            var suppliers = _Context.Suppliers.ToList();

            if (suppliers is null)
            {
                return BadRequest("The Suppliers is empty");
            }

            return Ok(suppliers);
        }
    }
}
