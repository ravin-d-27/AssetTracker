using System.ComponentModel.DataAnnotations;

namespace AssetTracker.Web.Models.Entities
{
    public class Asset
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public DateTime PurchaseDate { get; set; }

        public string Status { get; set; } // Available, Assigned, Damaged, Lost

        public ICollection<AssetAssignment> AssetAssignments { get; set; } = new List<AssetAssignment>();


    }
}
