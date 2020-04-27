using Microsoft.EntityFrameworkCore;

namespace GovernmentGrantAPI.Model
{
    public class HouseholdContext : DbContext
    {
        public HouseholdContext(DbContextOptions<HouseholdContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FamilyMember>().Property(e => e.Id).ValueGeneratedNever();
        }

        public DbSet<Household> Households { get; set; }
    }
}
