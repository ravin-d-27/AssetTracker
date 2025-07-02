using AssetTracker.Models.Entities;
using AssetTracker.Web.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AssetTracker.Web.Data
{
    public class AssetTrackerContext : IdentityDbContext<ApplicationUser>
    {
        public AssetTrackerContext(DbContextOptions<AssetTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AssetAssignment> AssetAssignments { get; set; }
    }
}
