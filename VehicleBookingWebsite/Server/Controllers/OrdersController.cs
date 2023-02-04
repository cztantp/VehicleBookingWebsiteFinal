using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleBookingWebsite.Server.Data;
using VehicleBookingWebsite.Server.IRepository;
using VehicleBookingWebsite.Shared.Domain;

namespace VehicleBookingWebsite.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public OrdersController(ApplicationDbContext context)
        public OrdersController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //context = context
            _unitOfWork = unitOfWork;
        }

        // GET: api/Orders
        [HttpGet]

        //Refactored
        //public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        public async Task<IActionResult> GetOrders()
        {
            //Refactored
            //return await _context.Orders.ToListAsync();
            var Orders = await _unitOfWork.Orders.GetAll(includes: q => q.Include(x => x.Customer).Include(x => x.Staff));
            return Ok(Orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]

        //Refactored
        //public async Task<ActionResult<Order>> GetOrder(int id)
        public async Task<ActionResult> GetOrder(int id)
        {
            //Refactored
            //var order = await _context.Orders.FindAsync(id);
            var Order = await _unitOfWork.Orders.Get(q => q.Id == id);

            if (Order == null)
            {
                return NotFound();
            }

            return Ok(Order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(order).State = EntityState.Modified;
            _unitOfWork.Orders.Update(order);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!OrderExists(id))
                if (!await OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            //Refactored
            //_context.Orders.Add(order);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Orders.Insert(order);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            //Refactored
            //var order = await _context.Orders.FindAsync(id);
            var Order = await _unitOfWork.Orders.Get(q => q.Id == id);
            if (Order == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Orders.Remove(order);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Orders.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool OrderExists(int id)
        private async Task<bool> OrderExists(int id)
        {
            //Refactored
            //return _context.Orders.Any(e => e.Id == id);
            var Order = await _unitOfWork.Orders.Get(q => q.Id == id);
            return Order != null;
        }
    }
}
