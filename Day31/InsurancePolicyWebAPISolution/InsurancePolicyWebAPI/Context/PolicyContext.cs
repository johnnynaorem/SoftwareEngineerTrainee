using InsurancePolicyWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicyWebAPI.Context
{
    public class PolicyContext : DbContext
    {
        public PolicyContext(DbContextOptions<PolicyContext> contextOptions) : base(contextOptions) { }

        public DbSet<Claimant> Claimants { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<ClaimType> ClaimTypes { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimReport> ClaimReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claim>()
                .HasOne(c => c.Claimant)
                .WithMany(cr => cr.Claims)
                .HasForeignKey(cr => cr.ClaimantId);

            modelBuilder.Entity<Policy>()
               .HasMany(p => p.Claims)
               .WithOne(c => c.Policy)
               .HasForeignKey(c => c.PolicyNumber);

            modelBuilder.Entity<Claim>()
                .HasMany(c => c.ClaimReports)
                .WithOne(cr => cr.Claim)
                .HasForeignKey(cr => cr.ClaimId);

            modelBuilder.Entity<Claim>()
            .HasOne(c => c.ClaimType)
            .WithMany(ct => ct.Claims)
            .HasForeignKey(c => c.ClaimTypeId);
        }
    }
}
