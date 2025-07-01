using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AssetTracker.Web.Data;
using AssetTracker.Web.Models.Entities;

namespace AssetTracker.Controllers
{
    public class AssetAssignmentsController : Controller
    {
        private readonly AssetTrackerContext _context;

        public AssetAssignmentsController(AssetTrackerContext context)
        {
            _context = context;
        }

        // GET: AssetAssignments
        public async Task<IActionResult> Index()
        {
            var assetTrackerContext = _context.AssetAssignments.Include(a => a.Asset).Include(a => a.Employee);
            return View(await assetTrackerContext.ToListAsync());
        }

        // GET: AssetAssignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetAssignment = await _context.AssetAssignments
                .Include(a => a.Asset)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetAssignment == null)
            {
                return NotFound();
            }

            return View(assetAssignment);
        }

        // GET: AssetAssignments/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name");
            ViewData["AssetId"] = new SelectList(_context.Assets.Where(a => a.Status == "Available"), "Id", "Model");
            return View();
        }


        // POST: AssetAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssetAssignment assetAssignment)
        {
            if (ModelState.IsValid)
            {
                assetAssignment.AssignedDate = DateTime.Now;
                _context.Add(assetAssignment);

                // Mark asset as assigned
                var asset = await _context.Assets.FindAsync(assetAssignment.AssetId);
                asset.Status = "Assigned";

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", assetAssignment.EmployeeId);
            ViewData["AssetId"] = new SelectList(_context.Assets.Where(a => a.Status == "Available"), "Id", "Model", assetAssignment.AssetId);
            return View(assetAssignment);
        }

        // GET: AssetAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetAssignment = await _context.AssetAssignments.FindAsync(id);
            if (assetAssignment == null)
            {
                return NotFound();
            }
            ViewData["AssetId"] = new SelectList(_context.Assets, "Id", "Type", assetAssignment.AssetId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", assetAssignment.EmployeeId);
            return View(assetAssignment);
        }

        // POST: AssetAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AssetId,EmployeeId,AssignedDate,ReturnDate,ConditionAtReturn")] AssetAssignment assetAssignment)
        {
            if (id != assetAssignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetAssignmentExists(assetAssignment.Id))
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
            ViewData["AssetId"] = new SelectList(_context.Assets, "Id", "Type", assetAssignment.AssetId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Name", assetAssignment.EmployeeId);
            return View(assetAssignment);
        }

        // GET: AssetAssignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetAssignment = await _context.AssetAssignments
                .Include(a => a.Asset)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetAssignment == null)
            {
                return NotFound();
            }

            return View(assetAssignment);
        }

        // POST: AssetAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetAssignment = await _context.AssetAssignments.FindAsync(id);
            if (assetAssignment != null)
            {
                _context.AssetAssignments.Remove(assetAssignment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetAssignmentExists(int id)
        {
            return _context.AssetAssignments.Any(e => e.Id == id);
        }


        public async Task<IActionResult> Return(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var assetAssignment = await _context.AssetAssignments
                .Include(a => a.Asset)
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (assetAssignment==null)
            {
                return NotFound();
            }

            return View(assetAssignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return (int id, [Bind("Id, ReturnDate, ConditionAtReturn")] AssetAssignment updatedAssignment)
        {
            var assignment = await _context.AssetAssignments
                .Include(a => a.Asset)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (assignment==null)
            {
                NotFound();
            }

            assignment.ReturnDate = updatedAssignment.ReturnDate ?? DateTime.Now;
            assignment.ConditionAtReturn = updatedAssignment.ConditionAtReturn;

            if (assignment.Asset!=null)
            {
                assignment.Asset.Status = "Available";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
