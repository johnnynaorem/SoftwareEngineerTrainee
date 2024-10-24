using Microsoft.EntityFrameworkCore;
using PolicyClaimWebApi.Models;

namespace PolicyClaimWebApi.Contexts
{
    public class PolicyContext : DbContext
    {
        public PolicyContext(DbContextOptions<PolicyContext> contextOptions) : base(contextOptions) { }

        public DbSet<Claimant> Claimants { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<ClaimType> ClaimTypes { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<ClaimFile> ClaimFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claim>()
                .HasOne(c => c.Claimant)
                .WithMany(cl => cl.Claims)
                .HasForeignKey(cl => cl.ClaimantId)
                .HasConstraintName("FK_Claim_Claimant");
            
            modelBuilder.Entity<Claim>()
                .HasOne(c => c.Policy)
                .WithMany(p=> p.Claims)
                .HasForeignKey(p => p.PolicyNumber)
                .HasConstraintName("FK_Claim_Policy");

            modelBuilder.Entity<Policy>()
                .HasMany(p => p.Types)
                .WithOne(t => t.Policy)
                .HasForeignKey(t => t.PolicyNumber)
                .HasConstraintName("FK_Policy_ClaimType");

            modelBuilder.Entity<ClaimFile>()
            .HasOne(cf => cf.Claim)
            .WithMany(c => c.ClaimFiles)
            .HasForeignKey(cf => cf.ClaimID)
            .HasConstraintName("FK_ClaimFile_Claim");
        }
    }
}
