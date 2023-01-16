using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vehicles.Data;
using Vehicles.Models;
using Vehicles.Service;

namespace Vehicles.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly VehicleContext _context;
        private readonly IVehicleMakeRepository _vehicleMakeRepository;


        public VehicleModelController(IVehicleMakeRepository vehicleMakeRepository, VehicleContext context)
        {
            _context = context;
            _vehicleMakeRepository = vehicleMakeRepository;
        }

        // GET: VehicleModel
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string vehicleModelMake, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = sortOrder == "abrv" ? "abrv_desc" : "abrv";
            ViewData["MakeSortParm"] = sortOrder == "make" ? "make_desc" : "make";

            if (vehicleModelMake != null)
            {
                pageNumber = 1;
            }
            else
            {
                vehicleModelMake = currentFilter;
            }

            ViewData["CurrentFilter"] = vehicleModelMake; 
            
            // section for filter by make
            var vehicleMakeQuery = await _vehicleMakeRepository.GetVehicleMakesForModelsAsync();

            var vehicleModels = from v in _context.VehicleModel.Include(v => v.VehicleMake) select v;

            if (!string.IsNullOrEmpty(vehicleModelMake))
            {
                vehicleModels = vehicleModels.Where(v => v.VehicleMake.Name == vehicleModelMake);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    vehicleModels = vehicleModels.OrderByDescending(v => v.Name);
                    break;
                case "abrv":
                    vehicleModels = vehicleModels.OrderBy(v => v.Abrv);
                    break;
                case "abrv_desc":
                    vehicleModels = vehicleModels.OrderByDescending(v => v.Abrv);
                    break;
                case "make":
                    vehicleModels = vehicleModels.OrderBy(v => v.VehicleMake.Name);
                    break;
                case "make_desc":
                    vehicleModels = vehicleModels.OrderByDescending(v => v.VehicleMake.Name);
                    break;
                default:
                    vehicleModels = vehicleModels.OrderBy(v => v.Name);
                    break;
            }

            IQueryable<VehicleModel> source = vehicleModels;
            int pageSize = 3;
            var paginatedList = await
                PaginatedList<VehicleModel>.CreateAsync(source, pageNumber ?? 1, pageSize);


            var vehicleModelVm = new VehicleModelViewModel
            {
                VehicleMakes = new SelectList(vehicleMakeQuery.Select(vM => vM.Name)),
                VehicleModels = paginatedList,
                VehicleModelMake = vehicleModelMake,
                SearchString = currentFilter
            };
            
            return View(vehicleModelVm);
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // GET: VehicleModel/Create
        public IActionResult Create()
        {
            ViewData["VehicleMakeId"] = new SelectList(_context.VehicleMake, "Id", "Name");
            return View();
        }

        // POST: VehicleModel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleMakeId,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleMakeId"] = new SelectList(_context.VehicleMake, "Id", "Id", vehicleModel.VehicleMakeId);
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
            ViewData["VehicleMakeId"] = new SelectList(_context.VehicleMake, "Id", "Id", vehicleModel.VehicleMakeId);
            return View(vehicleModel);
        }

        // POST: VehicleModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleMakeId,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (id != vehicleModel.Id)
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
                    if (!VehicleModelExists(vehicleModel.Id))
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
            ViewData["VehicleMakeId"] = new SelectList(_context.VehicleMake, "Id", "Id", vehicleModel.VehicleMakeId);
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
                .FirstOrDefaultAsync(m => m.Id == id);
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
          return (_context.VehicleModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
