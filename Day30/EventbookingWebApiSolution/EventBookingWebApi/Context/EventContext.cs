using EventBookingWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EventBookingWebApi.Context
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> contextOptions) : base(contextOptions) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Events)
                .WithOne(ev => ev.Employee)
                .HasForeignKey(ev => ev.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict) 
                .HasConstraintName("FK_Event_Employee");

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Employee)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict) 
                .HasConstraintName("FK_Booking_Employee");

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Event)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EventId)
                .OnDelete(DeleteBehavior.Restrict) 
                .HasConstraintName("FK_Booking_Event");
        }
    }
}
