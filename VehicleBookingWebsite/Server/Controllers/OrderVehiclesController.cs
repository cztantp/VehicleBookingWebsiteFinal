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
    public class OrderVehicleController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public OrderVehicleController(ApplicationDbContext context)
        public OrderVehicleController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //context = context
            _unitOfWork = unitOfWork;
        }

        // GET: api/OrderVehicle
        [HttpGet]

        //Refactored
        //public async Task<ActionResult<IEnumerable<OrderVehicle>>> GetOrderVehicle()
        public async Task<IActionResult> GetOrderVehicle()
        {
            //Refactored
            //return await _context.OrderVehicle.ToListAsync();
            var OrderVehicle = await _unitOfWork.OrderVehicle.GetAll(includes: q => q.Include(x => x.Order).Include(x => x.Vehicle).Include(x => x.VehicleType));
            return Ok(OrderVehicle);
        }

        // GET: api/OrderVehicle/5
        [HttpGet("{id}")]

        //Refactored
        //public async Task<ActionResult<OrderVehicle>> GetOrderVehicle(int id)
        public async Task<ActionResult> GetOrderVehicle(int id)
        {
            //Refactored
            //var ordervehicle = await _context.OrderVehicle.FindAsync(id);
            var OrderVehicle = await _unitOfWork.OrderVehicle.Get(q => q.Id == id);

            if (OrderVehicle == null)
            {
                return NotFound();
            }

            return Ok(OrderVehicle);
        }

        // PUT: api/OrderVehicle/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderVehicle(int id, OrderVehicle ordervehicle)
        {
            if (id != ordervehicle.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(ordervehicle).State = EntityState.Modified;
            _unitOfWork.OrderVehicle.Update(ordervehicle);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!OrderVehicleExists(id))
                if (!await OrderVehicleExists(id))
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

        // POST: api/OrderVehicle
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderVehicle>> PostOrderVehicle(OrderVehicle ordervehicle)
        {
            //Refactored
            //_context.OrderVehicle.Add(ordervehicle);
            //await _context.SaveChangesAsync();
            await _unitOfWork.OrderVehicle.Insert(ordervehicle);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetOrderVehicle", new { id = ordervehicle.Id }, ordervehicle);
        }

        // DELETE: api/OrderVehicle/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderVehicle(int id)
        {
            //Refactored
            //var ordervehicle = await _context.OrderVehicle.FindAsync(id);
            var OrderVehicle = await _unitOfWork.OrderVehicle.Get(q => q.Id == id);
            if (OrderVehicle == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.OrderVehicle.Remove(ordervehicle);
            //await _context.SaveChangesAsync();
            await _unitOfWork.OrderVehicle.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool OrderVehicleExists(int id)
        private async Task<bool> OrderVehicleExists(int id)
        {
            //Refactored
            //return _context.OrderVehicle.Any(e => e.Id == id);
            var OrderVehicle = await _unitOfWork.OrderVehicle.Get(q => q.Id == id);
            return OrderVehicle != null;
        }
    }
}
