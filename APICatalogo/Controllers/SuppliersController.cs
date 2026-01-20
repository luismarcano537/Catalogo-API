using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APICatalogo.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly APICatalogoContext _Context;

        public SuppliersController(APICatalogoContext context)
        {
            _Context = context;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Supplier>> Get()
        {
            var suppliers = _Context.Suppliers.AsNoTracking().ToList();

            if (suppliers is null)
            {
                return BadRequest("The Suppliers is empty");
            }

            return Ok(suppliers);
        }

        [HttpGet("id:int", Name = "GetID")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult GetID(int id)
        {
            var supplier = _Context.Suppliers.AsNoTracking().FirstOrDefault(S => S.SupplierID == id);

            if (supplier is null)
            {
                return BadRequest("Unable to locate the requested Supplier.");
            }

            return Ok(supplier);
        }

        [HttpGet("GetInclude")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Supplier>> GetInclude()
        {
            var supplier = _Context.Suppliers.Include(S => S.Products).Where(S => S.SupplierID < 5).AsNoTracking().ToList();

            if (supplier is null)
            {
                return BadRequest("The Supplier is empty");
            }

            return Ok(supplier);
        }

        [HttpPost]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Post(Supplier supplier)
        {
            if (supplier is null)
            {
                return BadRequest("It is impossible to add an empty supplier.");
            }

            _Context.Suppliers.Add(supplier);
            _Context.SaveChanges();

            return Ok();
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Put(int id, Supplier supplier)
        {
            if (id != supplier.SupplierID)
            {
                return BadRequest("Please provide a valid ID.");
            }

            _Context.Entry(supplier).State = EntityState.Modified;
            _Context.SaveChanges();

            return Ok(supplier);
        }

        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Delete(int id)
        {
            var supplier = _Context.Suppliers.FirstOrDefault(S => S.SupplierID == id);

            if (supplier is null)
            {
                return BadRequest("Please provide a valid ID.");
            }

            _Context.Suppliers.Remove(supplier);
            _Context.SaveChanges();

            return Ok(supplier);
        }
    }
}
