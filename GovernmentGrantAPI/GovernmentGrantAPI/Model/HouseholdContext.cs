using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            //modelBuilder.Entity<Household>()
            //    .HasMany(h => h.FamilyMembers)
            //    .WithOne(f => f.Household)
            //    .HasForeignKey(f => f.HouseholdId)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FamilyMember>().Property(e => e.Id).ValueGeneratedNever();
            //modelBuilder.Entity<FamilyMember>().Property(e => e.Id).ValueGeneratedOnAdd();
        }

        public DbSet<Household> Households { get; set; }
    }
}
