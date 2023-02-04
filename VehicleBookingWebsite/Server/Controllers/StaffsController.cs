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
    public class StaffController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public StaffController(ApplicationDbContext context)
        public StaffController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //context = context
            _unitOfWork = unitOfWork;
        }

        // GET: api/Staff
        [HttpGet]

        //Refactored
        //public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
        public async Task<IActionResult> GetStaff()
        {
            //Refactored
            //return await _context.Staff.ToListAsync();
            var Staff = await _unitOfWork.Staff.GetAll();
            return Ok(Staff);
        }

        // GET: api/Staff/5
        [HttpGet("{id}")]

        //Refactored
        //public async Task<ActionResult<Staff>> GetStaff(int id)
        public async Task<ActionResult> GetStaff(int id)
        {
            //Refactored
            //var staff = await _context.Staff.FindAsync(id);
            var Staff = await _unitOfWork.Staff.Get(q => q.Id == id);

            if (Staff == null)
            {
                return NotFound();
            }

            return Ok(Staff);
        }

        // PUT: api/Staff/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaff(int id, Staff staff)
        {
            if (id != staff.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(staff).State = EntityState.Modified;
            _unitOfWork.Staff.Update(staff);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!StaffExists(id))
                if (!await StaffExists(id))
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

        // POST: api/Staff
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Staff>> PostStaff(Staff staff)
        {
            //Refactored
            //_context.Staff.Add(staff);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Staff.Insert(staff);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetStaff", new { id = staff.Id }, staff);
        }

        // DELETE: api/Staff/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            //Refactored
            //var staff = await _context.Staff.FindAsync(id);
            var Staff = await _unitOfWork.Staff.Get(q => q.Id == id);
            if (Staff == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Staff.Remove(staff);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Staff.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool StaffExists(int id)
        private async Task<bool> StaffExists(int id)
        {
            //Refactored
            //return _context.Staff.Any(e => e.Id == id);
            var Staff = await _unitOfWork.Staff.Get(q => q.Id == id);
            return Staff != null;
        }
    }
}
