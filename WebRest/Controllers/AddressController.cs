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
    [Route("api/Address")]
    [ApiController]
    public class AddressController : ControllerBase, iController<Address>
    {
        private readonly WebRestOracleContext _context;

        public AddressController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/Address
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> Get()
        {
            return await _context.Addresses.ToListAsync();
        }

        // GET: api/Address/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> Get(string id)
        {
            var address = await _context.Addresses.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Address/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Address address)
        {
            if (id != address.AddressId)
            {
                return BadRequest();
            }
            _context.Addresses.Update(address);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Address
        [HttpPost]
        public async Task<ActionResult<Address>> Post(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = address.AddressId }, address);
        }

        // DELETE: api/Address/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(string id)
        {
            return _context.Addresses.Any(e => e.AddressId == id);
        }
    }
}
