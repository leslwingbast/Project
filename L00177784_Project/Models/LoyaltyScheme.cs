using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L00177784_Project.Models;

public class LoyaltyScheme
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string GroupName { get; set; }
    public int? RemainingItems { get; set; }
    
    [DefaultValue(null)]
    public DateTime? LastFreeBag { get; set; }
    public int CustomerId { get; set; }
    public int? LoyaltyGroup_Id { get; set; }
    [ForeignKey("LoyaltyGroup_Id")]
    public LoyaltyGroup? LoyaltyGroup { get; set; }

}
