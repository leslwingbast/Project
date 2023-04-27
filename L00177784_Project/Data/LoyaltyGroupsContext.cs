using Microsoft.EntityFrameworkCore;
using L00177784_Project.Models;
namespace L00177784_Project.Data;

public class LoyaltyGroupsContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<LoyaltyGroup> LoyaltyGroups { get; set;}
    public DbSet<LoyaltyScheme> LoyaltySchemes { get; set; }
    public DbSet<Sale> Sales { get; set; }

    public LoyaltyGroupsContext(DbContextOptions<LoyaltyGroupsContext> options)
: base(options)
    {
    }

}
