using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L00177784_Project.Models
{
    /// <summary>
    /// Model for Product
    /// This will hold the details of a product including which Loyalty Group it belongs to
    /// </summary>
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string? Barcode { get; set; }

        public int? Sku { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public int? LoyaltyGroup_Id { get; set; }
        [ForeignKey("LoyaltyGroup_Id")]
        public virtual LoyaltyGroup? LoyaltyGroup { get; set; }

    }
}