using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vehicles.API.Data;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class VehicleTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public VehicleTypesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleType>>> GetVehicleTypes()
        {
            return await _context.VehicleTypes.OrderBy(x => x.Description).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleType>> GetVehicleType(int id)
        {
            VehicleType vehicleType = await _context.VehicleTypes.FindAsync(id);

            if (vehicleType == null)
            {
                return NotFound();
            }

            return vehicleType;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleType(int id, VehicleType vehicleType)
        {
            if (id != vehicleType.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicleType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe tipo de vehículo.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<VehicleType>> PostVehicleType(VehicleType vehicleType)
        {
            _context.VehicleTypes.Add(vehicleType);

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetVehicleType", new { id = vehicleType.Id }, vehicleType);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe tipo de vehículo.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleType(int id)
        {
            VehicleType vehicleType = await _context.VehicleTypes.FindAsync(id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            _context.VehicleTypes.Remove(vehicleType);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
//    [Authorize(Roles = "Admin")]
//    public class VehicleTypesController : Controller
//    {
//        private readonly DataContext _context;

//        public VehicleTypesController(DataContext context) // cada que alguna clase utilice el DataContext, significa que el inyector le va a enviar la inyección de la conexión a la DB
//        {
//            _context = context;
//        }

//        // GET: VehicleTypes
//        public async Task<IActionResult> Index() // la primera Action es el index, y es el que lista lo artuculos
//        {
//            return View(await _context.VehicleTypes.ToListAsync()); //le enviamos el index del contexto los tipos de vehiculos se los enviamos como una lista.
//        }


//        // GET: VehicleTypes/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: VehicleTypes/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(VehicleType vehicleType)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Add(vehicleType);
//                    await _context.SaveChangesAsync();
//                    return RedirectToAction(nameof(Index));
//                }
//                catch (DbUpdateException dbUpdateException)
//                {
//                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
//                    {
//                        ModelState.AddModelError(string.Empty, "Ya existe este tipo de vehículo.");
//                    }
//                    else
//                    {
//                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
//                    }
//                }
//                catch (Exception exception)
//                {
//                    ModelState.AddModelError(string.Empty, exception.Message);
//                }
//            }

//            return View(vehicleType);
//        }

//        // GET: VehicleTypes/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var vehicleType = await _context.VehicleTypes.FindAsync(id);
//            if (vehicleType == null)
//            {
//                return NotFound();
//            }
//            return View(vehicleType);
//        }

//        // POST: VehicleTypes/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, VehicleType vehicleType)
//        {
//            if (id != vehicleType.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(vehicleType);
//                    await _context.SaveChangesAsync();
//                    return RedirectToAction(nameof(Index));
//                }
//                catch (DbUpdateException dbUpdateException)
//                {
//                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
//                    {
//                        ModelState.AddModelError(string.Empty, "Ya existe este tipo de vehículo.");
//                    }
//                    else
//                    {
//                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
//                    }
//                }
//                catch (Exception exception)
//                {
//                    ModelState.AddModelError(string.Empty, exception.Message);
//                }
//            }
//            return View(vehicleType);
//        }

//        // GET: VehicleTypes/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var vehicleType = await _context.VehicleTypes
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (vehicleType == null)
//            {
//                return NotFound();
//            }

//            _context.VehicleTypes.Remove(vehicleType);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//    }
//}
