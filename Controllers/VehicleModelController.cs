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
    public class VehicleModelController : Controller
    {
        private readonly VehicleContext _context;

        public VehicleModelController(VehicleContext context)
        {
            _context = context;
        }

        // GET: VehicleModel
        public async Task<IActionResult> Index()
        {
            var vehicleContext = _context.VehicleModel.Include(v => v.VehicleMake);
            return View(await vehicleContext.ToListAsync());
        }

        // GET: VehicleModel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VehicleModel == null)
            {
                return NotFound();
            }

            var vehicleModel = await _context.VehicleModel
                .Include(v => v.VehicleMake)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // GET: VehicleModel/Create
        public IActionResult Create()
        {
            ViewData["VehicleMakeID"] = new SelectList(_context.VehicleMake, "ID", "ID");
            return View();
        }

        // POST: VehicleModel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,VehicleMakeID,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleMakeID"] = new SelectList(_context.VehicleMake, "ID", "ID", vehicleModel.VehicleMakeID);
            return View(vehicleModel);
        }

        // GET: VehicleModel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VehicleModel == null)
            {
                return NotFound();
            }

            var vehicleModel = await _context.VehicleModel.FindAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            ViewData["VehicleMakeID"] = new SelectList(_context.VehicleMake, "ID", "ID", vehicleModel.VehicleMakeID);
            return View(vehicleModel);
        }

        // POST: VehicleModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,VehicleMakeID,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (id != vehicleModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleModelExists(vehicleModel.ID))
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
            ViewData["VehicleMakeID"] = new SelectList(_context.VehicleMake, "ID", "ID", vehicleModel.VehicleMakeID);
            return View(vehicleModel);
        }

        // GET: VehicleModel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VehicleModel == null)
            {
                return NotFound();
            }

            var vehicleModel = await _context.VehicleModel
                .Include(v => v.VehicleMake)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // POST: VehicleModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VehicleModel == null)
            {
                return Problem("Entity set 'VehicleContext.VehicleModel'  is null.");
            }
            var vehicleModel = await _context.VehicleModel.FindAsync(id);
            if (vehicleModel != null)
            {
                _context.VehicleModel.Remove(vehicleModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleModelExists(int id)
        {
          return (_context.VehicleModel?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
