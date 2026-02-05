using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
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
        private readonly IRepository<Supplier> _repository;

        public SuppliersController(IRepository<Supplier> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Supplier>> Get()
        {
            var suppliers = _repository.Get();

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
            var supplier = _repository.GetByID(S => S.SupplierID == id);

            if (supplier is null)
            {
                return BadRequest("Unable to locate the requested Supplier.");
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

            var SupplierNew = _repository.Create(supplier);

            return new CreatedAtRouteResult("GetID", new { id = SupplierNew.SupplierID }, SupplierNew);
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Put(int id, Supplier supplier)
        {
            if (id != supplier.SupplierID)
            {
                return BadRequest("Please provide a valid ID.");
            }

            var SuppliderUpdate = _repository.Update(supplier);

            return Ok(SuppliderUpdate);
        }

        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Delete(int id)
        {
            var supplier = _repository.GetByID(S => S.SupplierID == id);

            if (supplier is null)
            {
                return BadRequest($"Unable to find category by id: {id}");
            }

            var SupplierRemoved = _repository.Delete(supplier);

            return Ok(supplier);
        }
    }
}
