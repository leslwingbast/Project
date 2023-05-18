namespace L00177784_Project_Ui.Data
{
    public class Product
    {
        public int id { get; set; }
        public string? barcode { get; set; }
        public int? sku { get; set; }
        public string name { get; set; } = string.Empty;
        public int? loyaltyGroup_Id { get; set; }

        public LoyaltyGroup? productLoyaltyGroup { get; set; }
    }
}
