using Microsoft.EntityFrameworkCore;
using MovieRentWebAPI.Models;

namespace MovieRentWebAPI.Context
{
    public class MovieRentContext : DbContext
    {
        public MovieRentContext(DbContextOptions<MovieRentContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne(u => u.Customer)
                .HasForeignKey<Customer>(c => c.UserId)
                .HasConstraintName("FK_Customer_User")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Wishlists)
                .WithOne(w => w.Customer)
                .HasForeignKey(c => c.WishlistId)
                .HasConstraintName("FK_Customer_Wishlist")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Reservations)
                .WithOne(r => r.Customer)
                .HasForeignKey(c => c.ReservationId)
                .HasConstraintName("FK_Customer_Reservation")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerId)
                .HasConstraintName("FK_Rental_Customer")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rental>()
                 .HasOne(r => r.Movie)
                 .WithMany(m => m.Rentals)
                 .HasForeignKey(m => m.MovieId)
                 .HasConstraintName("FK_Rental_Movie")
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Movie>()
                 .HasMany(m => m.Reservations)
                 .WithOne(r => r.Movie)
                 .HasForeignKey(m => m.ReservationId)
                 .HasConstraintName("FK_Movie_Reservation")
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Payments)
                .HasForeignKey(p => p.CustomerId)
                .HasConstraintName("FK_Payment_Customer")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
