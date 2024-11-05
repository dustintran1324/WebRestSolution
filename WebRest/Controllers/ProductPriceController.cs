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
    [Route("api/ProductPrice")]
    [ApiController]
    public class ProductPriceController : ControllerBase, iController<ProductPrice>
    {
        private readonly WebRestOracleContext _context;

        public ProductPriceController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/ProductPrice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductPrice>>> Get()
        {
            return await _context.ProductPrices.ToListAsync();
        }

        // GET: api/ProductPrice/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductPrice>> Get(string id)
        {
            var productPrice = await _context.ProductPrices.FindAsync(id);

            if (productPrice == null)
            {
                return NotFound();
            }

            return productPrice;
        }

        // PUT: api/ProductPrice/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, ProductPrice productPrice)
        {
            if (id != productPrice.ProductPriceId)
            {
                return BadRequest();
            }
            _context.ProductPrices.Update(productPrice);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductPriceExists(id))
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

        // POST: api/ProductPrice
        [HttpPost]
        public async Task<ActionResult<ProductPrice>> Post(ProductPrice productPrice)
        {
            _context.ProductPrices.Add(productPrice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = productPrice.ProductPriceId }, productPrice);
        }

        // DELETE: api/ProductPrice/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var productPrice = await _context.ProductPrices.FindAsync(id);
            if (productPrice == null)
            {
                return NotFound();
            }

            _context.ProductPrices.Remove(productPrice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductPriceExists(string id)
        {
            return _context.ProductPrices.Any(e => e.ProductPriceId == id);
        }
    }
}
