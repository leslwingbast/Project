﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L00177784_Project.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Index(IsUnique = true)]
    public string Barcode { get; set; }
    [Index(IsUnique = true)]
    public int Sku { get; set; }
    public string Name { get; set; }
    public int? LoyaltyGroup_Id { get; set; }
    [ForeignKey("LoyaltyGroup_Id")]
    public virtual LoyaltyGroup LoyaltyGroup { get; set; }

}
