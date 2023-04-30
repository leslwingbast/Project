using L00177784_Project.Models;
using Microsoft.EntityFrameworkCore;
namespace L00177784_Project.Data
{
    /// <summary>
    /// DB context for connecting to the local databse
    /// </summary>
    public class LoyaltyGroupsContext : DbContext
    {
        //DbSet to interact with models
        public DbSet<Product> Products { get; set; }
        public DbSet<LoyaltyGroup> LoyaltyGroups { get; set; }
        public DbSet<LoyaltyScheme> LoyaltySchemes { get; set; }
        public DbSet<Sale> Sales { get; set; }

        /// <summary>
        /// Constructor to create connection to database
        /// </summary>
        /// <param name="options">Options for connecting</param>
        public LoyaltyGroupsContext(DbContextOptions<LoyaltyGroupsContext> options)
    : base(options)
        {
        }

    }
}