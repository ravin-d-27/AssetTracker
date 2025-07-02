// File: Models/Entities/AssetRequest.cs
namespace AssetTracker.Web.Models.Entities
{
    public class AssetRequest
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }  // e.g., Pending, Approved, Rejected

        // Navigation properties
        public Asset Asset { get; set; }
        public Employee Employee { get; set; }
    }
}
