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
    [Route("api/OrdersLine")]
    [ApiController]
    public class OrdersLineController : ControllerBase, iController<OrdersLine>
    {
        private readonly WebRestOracleContext _context;

        public OrdersLineController(WebRestOracleContext context)
        {
            _context = context;
        }

        // GET: api/OrdersLine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdersLine>>> Get()
        {
            return await _context.OrdersLines.ToListAsync();
        }

        // GET: api/OrdersLine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdersLine>> Get(string id)
        {
            var ordersLine = await _context.OrdersLines.FindAsync(id);

            if (ordersLine == null)
            {
                return NotFound();
            }

            return ordersLine;
        }

        // PUT: api/OrdersLine/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, OrdersLine ordersLine)
        {
            if (id != ordersLine.OrdersLineId)
            {
                return BadRequest();
            }
            _context.OrdersLines.Update(ordersLine);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersLineExists(id))
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

        // POST: api/OrdersLine
        [HttpPost]
        public async Task<ActionResult<OrdersLine>> Post(OrdersLine ordersLine)
        {
            _context.OrdersLines.Add(ordersLine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = ordersLine.OrdersLineId }, ordersLine);
        }

        // DELETE: api/OrdersLine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ordersLine = await _context.OrdersLines.FindAsync(id);
            if (ordersLine == null)
            {
                return NotFound();
            }

            _context.OrdersLines.Remove(ordersLine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdersLineExists(string id)
        {
            return _context.OrdersLines.Any(e => e.OrdersLineId == id);
        }
    }
}
