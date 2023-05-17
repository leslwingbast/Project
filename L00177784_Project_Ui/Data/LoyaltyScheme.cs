using System.ComponentModel;

namespace L00177784_Project_Ui.Data
{
    public class LoyaltyScheme
    {
        public int id { get; set; }
        public string? groupName { get; set; }
        public int remainingItems { get; set; }
        public DateTime? lastFreeBag { get; set; }
        public int customerId { get; set; }
        public int loyaltyGroup_Id { get; set; }
        public LoyaltyGroup? loyaltyGroup { get; set; }
    }
}
