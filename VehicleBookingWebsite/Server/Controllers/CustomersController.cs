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
    public class CustomersController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public CustomersController(ApplicationDbContext context)
        public CustomersController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //context = context
            _unitOfWork = unitOfWork;
        }

        // GET: api/Customers
        [HttpGet]

        //Refactored
        //public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        public async Task<IActionResult> GetCustomers()
        {
            //To be deleted or comment after testing the Global Error Handling
            //return NotFound();

            //Refactored
            //return await _context.Customers.ToListAsync();
            var Customers = await _unitOfWork.Customers.GetAll();
            return Ok(Customers);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]

        //Refactored
        //public async Task<ActionResult<Customer>> GetCustomer(int id)
        public async Task<ActionResult> GetCustomer(int id)
        {
            //Refactored
            //var customer = await _context.Customers.FindAsync(id);
            var Customer = await _unitOfWork.Customers.Get(q => q.Id == id);

            if (Customer == null)
            {
                return NotFound();
            }

            return Ok(Customer);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(customer).State = EntityState.Modified;
            _unitOfWork.Customers.Update(customer);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!CustomerExists(id))
                if (!await CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            //Refactored
            //_context.Customers.Add(customer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Customers.Insert(customer);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            //Refactored
            //var customer = await _context.Customers.FindAsync(id);
            var Customer = await _unitOfWork.Customers.Get(q => q.Id == id);
            if (Customer == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Customers.Remove(customer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Customers.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool CustomerExists(int id)
        private async Task<bool> CustomerExists(int id)
        {
            //Refactored
            //return _context.Customers.Any(e => e.Id == id);
            var Customer = await _unitOfWork.Customers.Get(q => q.Id == id);
            return Customer != null;
        }
    }
}
