using Microsoft.EntityFrameworkCore;
using coreworking_space_booking_backend.Models;
using coreworking_space_booking_backend.Dtos.Responses.Product;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using coreworking_space_booking_backend.Models.Facility;
using coreworking_space_booking_backend.Models.Booking;
using coreworking_space_booking_backend.Models.Permission;
using coreworking_space_booking_backend.Models.CardDetails;
using coreworking_space_booking_backend.Controllers;

namespace coreworking_space_booking_backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Pricing> Pricings { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Advertising> Advertisings { get; set; }

        public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }
        
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<CardDetail> CardDetails { get; set; }

        public ApplicationDbContext()
        {

       
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>()
                .Property(r => r.Type)
                .HasConversion<string>();
        }

        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
    }

}
