using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace L00177784_Project.Models
{
    /// <summary>
    /// Model for Sale
    /// This will hold the details from a sale in order to update the loyalty schemes
    /// </summary>
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int? Sku { get; set; }
        public string? Barcode { get; set; }
        public int CustomerId { get; set; }

        [DefaultValue(false)]
        public bool IsFree { get; set; }
        public int Qty { get; set; }

        [DefaultValue(false)]
        public bool Processed { get; set; }

    }
}
