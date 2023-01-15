using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vehicles.Data;
using Vehicles.Models;
using Vehicles.Service;

namespace Vehicles.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly IVehicleMakeRepository _vehicleMakeRepository;
        private readonly IMapper _mapper;


        public VehicleMakeController(IVehicleMakeRepository vehicleMakeRepository, IMapper mapper)
        {
            _vehicleMakeRepository = vehicleMakeRepository;
            _mapper = mapper;
        }

        // GET: VehicleMake
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = sortOrder == "abrv" ? "abrv_desc" : "abrv";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var vehicleMakes =
                await _vehicleMakeRepository.GetVehicleMakesAsync(sortOrder, currentFilter, searchString, pageNumber);

            var vehicleMakesVm = _mapper.Map<List<VehicleMakeViewModel>>(vehicleMakes);

            var paginatedVehicleMakesVm = new PaginatedList<VehicleMakeViewModel>(vehicleMakesVm, vehicleMakes.Count(), pageNumber ?? 1, 3);

            return View(paginatedVehicleMakesVm);
        }

        // GET: VehicleMake/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _vehicleMakeRepository.GetVehicleMakeByIdAsync(id.Value);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMakeVm = _mapper.Map<VehicleMakeViewModel>(vehicleMake);

            return View(vehicleMakeVm);
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
                await _vehicleMakeRepository.CreateVehicleMakeAsync(vehicleMake);
                return RedirectToAction(nameof(Index));
            }

            var vehicleMakeVm = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(vehicleMakeVm);
        }

        // GET: VehicleMake/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _vehicleMakeRepository.GetVehicleMakeByIdAsync(id.Value);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMakeVm = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(vehicleMakeVm);
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
                    await _vehicleMakeRepository.UpdateVehicleMakeAsync(vehicleMake);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _vehicleMakeRepository.VehicleMakeExistsAsync(vehicleMake.Id))
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

            var vehicleMakeVm = _mapper.Map<VehicleMakeViewModel>(vehicleMake);
            return View(vehicleMakeVm);
        }

        // GET: VehicleMake/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            var vehicleMake = await _vehicleMakeRepository.GetVehicleMakeByIdAsync(id.Value);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            var vehicleMakeVm = _mapper.Map<VehicleMakeViewModel>(vehicleMake);

            return View(vehicleMakeVm);
        }

        // POST: VehicleMake/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleMakeRepository.DeleteVehicleMakeAsync(id);
            return RedirectToAction(nameof(Index));
        }
        
        
        // marked for deletion
        //private bool VehicleMakeExists(int id)
        //{
        //  return (_context.VehicleMake?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
