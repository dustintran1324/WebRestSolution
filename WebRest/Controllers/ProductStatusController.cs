using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using WebRestEF.EF.Data;
using WebRestEF.EF.Models;
using WebRest.Interfaces;

namespace WebRest.Controllers
{
    [Route("api/ProductStatus")]
    [ApiController]
    public class ProductStatusController : ControllerBase, iController<ProductStatus>
    {
        private readonly WebRestOracleContext _context;

        public ProductStatusController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/ProductStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStatus>>> Get()
        {
            return await _context.ProductStatuses.ToListAsync();
        }

        // GET: api/ProductStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductStatus>> Get(string id)
        {
            var productStatus = await _context.ProductStatuses.FindAsync(id);

            if (productStatus == null)
            {
                return NotFound();
            }

            return productStatus;
        }

        // PUT: api/ProductStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, ProductStatus productStatus)
        {
            if (id != productStatus.ProductStatusId)
            {
                return BadRequest();
            }
            _context.ProductStatuses.Update(productStatus);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductStatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductStatus
        [HttpPost]
        public async Task<ActionResult<ProductStatus>> Post(ProductStatus productStatus)
        {
            _context.ProductStatuses.Add(productStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = productStatus.ProductStatusId }, productStatus);
        }

        // DELETE: api/ProductStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var productStatus = await _context.ProductStatuses.FindAsync(id);
            if (productStatus == null)
            {
                return NotFound();
            }

            _context.ProductStatuses.Remove(productStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductStatusExists(string id)
        {
            return _context.ProductStatuses.Any(e => e.ProductStatusId == id);
        }
    }
}
