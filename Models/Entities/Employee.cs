using System.ComponentModel.DataAnnotations;

namespace AssetTracker.Web.Models.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }

        public ICollection<AssetAssignment> AssetAssignments { get; set; } =  new List<AssetAssignment>();
    }
}
