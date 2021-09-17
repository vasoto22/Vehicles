using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Vehicles.API.Data;
using Vehicles.API.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Vehicles.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class DocumentTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public DocumentTypesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentType>>> GetDocumentTypes()
        {
            return await _context.DocumentTypes.OrderBy(x => x.Description).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentType>> GetDocumentType(int id)
        {
            DocumentType documentType = await _context.DocumentTypes.FindAsync(id);

            if (documentType == null)
            {
                return NotFound();
            }

            return documentType;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentType(int id, DocumentType documentType)
        {
            if (id != documentType.Id)
            {
                return BadRequest();
            }

            _context.Entry(documentType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe tipo de documento.");
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
        public async Task<ActionResult<DocumentType>> PostDocumentType(DocumentType documentType)
        {
            _context.DocumentTypes.Add(documentType);

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetDocumentType", new { id = documentType.Id }, documentType);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe tipo de documento.");
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
        public async Task<IActionResult> DeleteDocumentType(int id)
        {
            DocumentType documentType = await _context.DocumentTypes.FindAsync(id);
            if (documentType == null)
            {
                return NotFound();
            }

            _context.DocumentTypes.Remove(documentType);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
//    [Authorize(Roles = "Admin")]
//    public class DocumentTypesController : Controller
//    {
//        private readonly DataContext _context;

//        public DocumentTypesController(DataContext context)
//        {
//            _context = context;
//        }

//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.DocumentTypes.ToListAsync());
//        }

//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(DocumentType documentType)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Add(documentType);
//                    await _context.SaveChangesAsync();
//                    return RedirectToAction(nameof(Index));
//                }
//                catch (DbUpdateException dbUpdateException)
//                {
//                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
//                    {
//                        ModelState.AddModelError(string.Empty, "Ya existe este tipo de documento.");
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

//            return View(documentType);
//        }

//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            DocumentType documentType = await _context.DocumentTypes.FindAsync(id);
//            if (documentType == null)
//            {
//                return NotFound();
//            }

//            return View(documentType);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, DocumentType documentType)
//        {
//            if (id != documentType.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(documentType);
//                    await _context.SaveChangesAsync();
//                    return RedirectToAction(nameof(Index));
//                }
//                catch (DbUpdateException dbUpdateException)
//                {
//                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
//                    {
//                        ModelState.AddModelError(string.Empty, "Ya existe este tipo de documento.");
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
//            return View(documentType);
//        }

//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            DocumentType documentType = await _context.DocumentTypes
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (documentType == null)
//            {
//                return NotFound();
//            }

//            _context.DocumentTypes.Remove(documentType);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }
//    }
//}
