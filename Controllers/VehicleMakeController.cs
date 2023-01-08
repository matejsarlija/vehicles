using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vehicles.Data;
using Vehicles.Models;

namespace Vehicles.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly VehicleContext _context;

        public VehicleMakeController(VehicleContext context)
        {
            _context = context;
        }

        // GET: VehicleMake
        public async Task<IActionResult> Index(string sortOrder)
        {
            
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = sortOrder == "abrv" ? "abrv_desc" : "abrv";

            var vehicleMakes = from v in _context.VehicleMake select v;

            switch (sortOrder)
            {
                case "name_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(v => v.Name);
                    break;
                case "abrv":
                    vehicleMakes = vehicleMakes.OrderBy(v => v.Abrv);
                    break;
                case "abrv_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(v => v.Abrv);
                    break;
                default:
                    vehicleMakes = vehicleMakes.OrderBy(v => v.Name);
                    break;

            }
            

              return View(await vehicleMakes.AsNoTracking().ToListAsync());
        }

        // GET: VehicleMake/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMake
                .Include(v => v.VehicleModels)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        // GET: VehicleMake/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleMake/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleMake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMake/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMake.FindAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMake/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (id != vehicleMake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleMake);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleMakeExists(vehicleMake.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMake/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.VehicleMake
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        // POST: VehicleMake/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VehicleMake == null)
            {
                return Problem("Entity set 'VehicleContext.VehicleMake'  is null.");
            }
            var vehicleMake = await _context.VehicleMake.FindAsync(id);
            if (vehicleMake != null)
            {
                _context.VehicleMake.Remove(vehicleMake);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleMakeExists(int id)
        {
          return (_context.VehicleMake?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
