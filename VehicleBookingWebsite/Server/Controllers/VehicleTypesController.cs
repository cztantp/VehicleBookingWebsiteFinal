using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleBookingWebsite.Server.IRepository;
using VehicleBookingWebsite.Shared.Domain;
using VehicleBookingWebsite.Server.Data;

namespace VehicleBookingWebsite.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypeController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public VehicleTypeController(ApplicationDbContext context)
        public VehicleTypeController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //context = context
            _unitOfWork = unitOfWork;
        }

        // GET: api/VehicleType
        [HttpGet]

        //Refactored
        //public async Task<ActionResult<IEnumerable<VehicleType>>> GetVehicleType()
        public async Task<IActionResult> GetVehicleType()
        {
            //Refactored
            //return await _context.VehicleType.ToListAsync();
            var VehicleType = await _unitOfWork.VehicleType.GetAll();
            return Ok(VehicleType);
        }

        // GET: api/VehicleType/5
        [HttpGet("{id}")]

        //Refactored
        //public async Task<ActionResult<VehicleType>> GetVehicleType(int id)
        public async Task<ActionResult> GetVehicleType(int id)
        {
            //Refactored
            //var vehicletype = await _context.VehicleType.FindAsync(id);
            var VehicleType = await _unitOfWork.VehicleType.Get(q => q.Id == id);

            if (VehicleType == null)
            {
                return NotFound();
            }

            return Ok(VehicleType);
        }

        // PUT: api/VehicleType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleType(int id, VehicleType vehicletype)
        {
            if (id != vehicletype.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(vehicletype).State = EntityState.Modified;
            _unitOfWork.VehicleType.Update(vehicletype);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!VehicleTypeExists(id))
                if (!await VehicleTypeExists(id))
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

        // POST: api/VehicleType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehicleType>> PostVehicleType(VehicleType vehicletype)
        {
            //Refactored
            //_context.VehicleType.Add(vehicletype);
            //await _context.SaveChangesAsync();
            await _unitOfWork.VehicleType.Insert(vehicletype);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetVehicleType", new { id = vehicletype.Id }, vehicletype);
        }

        // DELETE: api/VehicleType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleType(int id)
        {
            //Refactored
            //var vehicletype = await _context.VehicleType.FindAsync(id);
            var VehicleType = await _unitOfWork.VehicleType.Get(q => q.Id == id);
            if (VehicleType == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.VehicleType.Remove(vehicletype);
            //await _context.SaveChangesAsync();
            await _unitOfWork.VehicleType.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool VehicleTypeExists(int id)
        private async Task<bool> VehicleTypeExists(int id)
        {
            //Refactored
            //return _context.VehicleType.Any(e => e.Id == id);
            var VehicleType = await _unitOfWork.VehicleType.Get(q => q.Id == id);
            return VehicleType != null;
        }
    }
}
