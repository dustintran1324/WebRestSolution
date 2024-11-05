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
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase, iController<Product>
    {
        private readonly WebRestOracleContext _context;

        public ProductController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            _context.Products.Update(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = product.ProductId }, product);
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
