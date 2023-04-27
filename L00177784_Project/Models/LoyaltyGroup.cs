using System.ComponentModel.DataAnnotations;

namespace L00177784_Project.Models;

public class LoyaltyGroup
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Threshold { get; set; }
    public string? Image { get; set; }

    public ICollection<Product>? Products { get; set; }
    public ICollection<LoyaltyScheme>? LoyaltySchemes { get; set;}

}
