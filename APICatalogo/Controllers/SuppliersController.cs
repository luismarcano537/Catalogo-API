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
        private readonly ISupplierRepository _Repository;

        public SuppliersController(ISupplierRepository repository)
        {
            _Repository = repository;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Supplier>> Get()
        {
            var suppliers = _Repository.Get();

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
            var supplier = _Repository.GetByID(id);

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
            var supplier = _Repository.GetInclude();

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

            var SupplierNew = _Repository.Create(supplier);

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

            var SuppliderDeleted = _Repository.Update(supplier);

            return Ok(SuppliderDeleted);
        }

        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult Delete(int id)
        {
            var supplier = _Repository.GetByID(id);

            if (supplier is null)
            {
                return BadRequest($"Unable to find category by id: {id}");
            }

            var SupplierRemoved = _Repository.Delete(id);

            return Ok(supplier);
        }
    }
}
