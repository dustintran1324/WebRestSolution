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
    [Route("api/OrderState")]
    [ApiController]
    public class OrderStateController : ControllerBase, iController<OrderState>
    {
        private readonly WebRestOracleContext _context;

        public OrderStateController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrderState
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderState>>> Get()
        {
            return await _context.OrderStates.ToListAsync();
        }

        // GET: api/OrderState/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderState>> Get(string id)
        {
            var orderState = await _context.OrderStates.FindAsync(id);

            if (orderState == null)
            {
                return NotFound();
            }

            return orderState;
        }

        // PUT: api/OrderState/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, OrderState orderState)
        {
            if (id != orderState.OrderStateId)
            {
                return BadRequest();
            }
            _context.OrderStates.Update(orderState);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStateExists(id))
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

        // POST: api/OrderState
        [HttpPost]
        public async Task<ActionResult<OrderState>> Post(OrderState orderState)
        {
            _context.OrderStates.Add(orderState);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = orderState.OrderStateId }, orderState);
        }

        // DELETE: api/OrderState/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var orderState = await _context.OrderStates.FindAsync(id);
            if (orderState == null)
            {
                return NotFound();
            }

            _context.OrderStates.Remove(orderState);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderStateExists(string id)
        {
            return _context.OrderStates.Any(e => e.OrderStateId == id);
        }
    }
}
