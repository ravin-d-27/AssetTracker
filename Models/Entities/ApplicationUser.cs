using Microsoft.AspNetCore.Identity;

namespace AssetTracker.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; }
        public int? EmployeeId { get; set; }
    }
}
