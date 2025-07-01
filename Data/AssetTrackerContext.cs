using Microsoft.EntityFrameworkCore;
using AssetTracker.Web.Models.Entities;
using System.Collections.Generic;

namespace AssetTracker.Web.Data
{
    public class AssetTrackerContext : DbContext
    {
        public AssetTrackerContext(DbContextOptions<AssetTrackerContext> options)
        : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetAssignment> AssetAssignments { get; set; }
    }
}
