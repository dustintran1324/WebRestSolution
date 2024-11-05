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
    [Route("api/CustomerAddress")]
    [ApiController]
    public class CustomerAddressController : ControllerBase, iController<CustomerAddress>
    {
        private readonly WebRestOracleContext _context;

        public CustomerAddressController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/CustomerAddress
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerAddress>>> Get()
        {
            return await _context.CustomerAddresses.ToListAsync();
        }

        // GET: api/CustomerAddress/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerAddress>> Get(string id)
        {
            var customerAddress = await _context.CustomerAddresses.FindAsync(id);

            if (customerAddress == null)
            {
                return NotFound();
            }

            return customerAddress;
        }

        // PUT: api/CustomerAddress/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, CustomerAddress customerAddress)
        {
            if (id != customerAddress.CustomerAddressId)
            {
                return BadRequest();
            }
            _context.CustomerAddresses.Update(customerAddress);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerAddressExists(id))
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

        // POST: api/CustomerAddress
        [HttpPost]
        public async Task<ActionResult<CustomerAddress>> Post(CustomerAddress customerAddress)
        {
            _context.CustomerAddresses.Add(customerAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = customerAddress.CustomerAddressId }, customerAddress);
        }

        // DELETE: api/CustomerAddress/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customerAddress = await _context.CustomerAddresses.FindAsync(id);
            if (customerAddress == null)
            {
                return NotFound();
            }

            _context.CustomerAddresses.Remove(customerAddress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerAddressExists(string id)
        {
            return _context.CustomerAddresses.Any(e => e.CustomerAddressId == id);
        }
    }
}
