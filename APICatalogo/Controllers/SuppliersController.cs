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

        [HttpGet("id:int", Name = "GetID")]
        public ActionResult GetID(int id)
        {
            var supplier = _Context.Suppliers.FirstOrDefault(S => S.SupplierID == id);

            if (supplier is null)
            {
                return BadRequest("Unable to locate the requested Supplier.");
            }

            return Ok(supplier);
        }
    }
}
