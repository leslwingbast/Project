namespace L00177784_Project.Models
{
    public class Sale
    {
        public DateTime DateTime { get; set; }
        public int Sku { get; set; }
        public string Barcode { get; set; }
        public int CustomerId { get; set; }
        public bool IsFree { get; set; }
        public bool Qty { get; set; }

    }
}
