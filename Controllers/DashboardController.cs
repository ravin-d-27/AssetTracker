using Microsoft.AspNetCore.Mvc;
using AssetTracker.Web.Models;
using AssetTracker.Web.Data;
using Microsoft.AspNetCore.Authorization;

namespace AssetTracker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly AssetTrackerContext _context;   // replace with your real context class name

        public DashboardController(AssetTrackerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var totalAssets = _context.Assets.Count();
            var availableAssets = _context.Assets.Count(a => a.Status == "Available");
            var assignedAssets = _context.Assets.Count(a => a.Status == "Assigned");
            var underServiceAssets = _context.Assets.Count(a => a.Status == "Under Service");
            var notWorkingAssets = _context.Assets.Count(a => a.Status == "Not Working");
            var totalEmployees = _context.Employees.Count();

            ViewBag.TotalAssets = totalAssets;
            ViewBag.AvailableAssets = availableAssets;
            ViewBag.AssignedAssets = assignedAssets;
            ViewBag.UnderServiceAssets = underServiceAssets;
            ViewBag.NotWorkingAssets = notWorkingAssets;
            ViewBag.TotalEmployees = totalEmployees;

            return View();
        }
    }
}
