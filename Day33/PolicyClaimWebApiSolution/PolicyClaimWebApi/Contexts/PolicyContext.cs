using Microsoft.EntityFrameworkCore;
using PolicyClaimWebApi.Models;

namespace PolicyClaimWebApi.Contexts
{
    public class PolicyContext : DbContext
    {
        public PolicyContext(DbContextOptions<PolicyContext> options) : base(options) { }

        public DbSet<Claimant> Claimants { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<ClaimType> ClaimTypes { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimFile> ClaimFiles { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Policy to ClaimType
            modelBuilder.Entity<ClaimType>()
                .HasOne(ct => ct.Policy)
                .WithMany(p => p.Types)
                .HasForeignKey(ct => ct.PolicyNumber)
                .OnDelete(DeleteBehavior.Cascade);

            // Policy to Claim
            modelBuilder.Entity<Claim>()
                .HasOne(c => c.Policy)
                .WithMany(p => p.Claims)
                .HasForeignKey(c => c.PolicyNumber)
                .OnDelete(DeleteBehavior.Cascade);

            // ClaimType to Claim
            modelBuilder.Entity<Claim>()
                .HasOne(c => c.ClaimType)
                .WithMany(ct => ct.Claims)
                .HasForeignKey(c => c.TypeName)
                .OnDelete(DeleteBehavior.Restrict);

            // Claimant to Claim
            modelBuilder.Entity<Claim>()
                .HasOne(c => c.Claimant)
                .WithMany(cl => cl.Claims)
                .HasForeignKey(c => c.ClaimantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Claim to ClaimFile
            modelBuilder.Entity<ClaimFile>()
                .HasOne(cf => cf.Claim)
                .WithMany(c => c.ClaimFiles)
                .HasForeignKey(cf => cf.ClaimID)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
