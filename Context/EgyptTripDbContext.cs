using EgyptTripApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EgyptTripApi.Context
{
    public class EgyptTripDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public EgyptTripDbContext(DbContextOptions options) : base(options) { }

        // DbSets for default schema (dbo)
        public DbSet<TourismCompany> TourismCompanies { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<TourGuide> TourGuides { get; set; }
        public DbSet<TourPackage> TourPackages { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<PackageDestinations> PackageDestinations { get; set; }
        public DbSet<PackageActivities> PackageActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Identity tables in 'security' schema and rename them
            builder.Entity<ApplicationUser>().ToTable("Users", "security");
            builder.Entity<ApplicationRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");



            // Configure TourismCompany
            builder.Entity<TourismCompany>()
                 .HasKey(tc => tc.CompanyID);
            builder.Entity<TourismCompany>()
                .HasOne(tc => tc.User)
                .WithOne(u => u.TourismCompany)
                .HasForeignKey<TourismCompany>(tc => tc.UserID)
                .IsRequired();

            // Configure Hotel
            builder.Entity<Hotel>()
                .HasKey(h => h.HotelID);
            builder.Entity<Hotel>()
                .HasOne(h => h.User)
                .WithOne(u => u.Hotel)
                .HasForeignKey<Hotel>(h => h.UserID)
                .IsRequired();

            // Configure TourGuide
            builder.Entity<TourGuide>()
                .HasKey(tg => tg.GuideID);
            builder.Entity<TourGuide>()
                .HasOne(tg => tg.User)
                .WithOne(u => u.TourGuide)
                .HasForeignKey<TourGuide>(tg => tg.UserID)
                .IsRequired();

            // Configure TourPackage
            builder.Entity<TourPackage>()
                .HasKey(tp => tp.PackageID);
            builder.Entity<TourPackage>()
                .HasOne(tp => tp.TourismCompany)
                .WithMany(tc => tc.TourPackages)
                .HasForeignKey(tp => tp.CompanyID)
                .IsRequired();

            // Configure Destination
            builder.Entity<Destination>()
                .HasKey(d => d.DestinationID);

            // Configure Activity
            builder.Entity<Activity>()
                .HasKey(a => a.ActivityID);

            // Configure Room
            builder.Entity<Room>()
                .HasKey(r => r.RoomID);
            builder.Entity<Room>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelID)
                .IsRequired();

            // Configure Booking
            builder.Entity<Booking>()
                .HasKey(b => b.BookingID);
            builder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserID)
                .IsRequired();
            builder.Entity<Booking>()
                .HasOne(b => b.TourPackage)
                .WithMany(tp => tp.Bookings)
                .HasForeignKey(b => b.PackageID);
            builder.Entity<Booking>()
                .HasOne(b => b.Hotel)
                .WithMany(h => h.Bookings)
                .HasForeignKey(b => b.HotelID);
            builder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomID);
            builder.Entity<Booking>()
                .HasOne(b => b.TourGuide)
                .WithMany(tg => tg.Bookings)
                .HasForeignKey(b => b.GuideID);

            // Configure Review
            builder.Entity<Review>()
                .HasKey(r => r.ReviewID);
            builder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserID)
                .IsRequired();
            builder.Entity<Review>()
                .HasOne(r => r.TourPackage)
                .WithMany(tp => tp.Reviews)
                .HasForeignKey(r => r.PackageID);
            builder.Entity<Review>()
                .HasOne(r => r.Hotel)
                .WithMany(h => h.Reviews)
                .HasForeignKey(r => r.HotelID);
            builder.Entity<Review>()
                .HasOne(r => r.TourGuide)
                .WithMany(tg => tg.Reviews)
                .HasForeignKey(r => r.GuideID);

            // Configure PackageDestinations (junction table)
            builder.Entity<PackageDestinations>()
                .HasKey(pd => new { pd.PackageID, pd.DestinationID });
            builder.Entity<PackageDestinations>()
                .HasOne(pd => pd.TourPackage)
                .WithMany(p => p.PackageDestinations)
                .HasForeignKey(pd => pd.PackageID);
            builder.Entity<PackageDestinations>()
                .HasOne(pd => pd.Destination)
                .WithMany(d => d.PackageDestinations)
                .HasForeignKey(pd => pd.DestinationID);

            // Configure PackageActivities (junction table)
            builder.Entity<PackageActivities>()
                .HasKey(pa => new { pa.PackageID, pa.ActivityID });
            builder.Entity<PackageActivities>()
                .HasOne(pa => pa.TourPackage)
                .WithMany(p => p.PackageActivities)
                .HasForeignKey(pa => pa.PackageID);
            builder.Entity<PackageActivities>()
                .HasOne(pa => pa.Activity)
                .WithMany(a => a.PackageActivities)
                .HasForeignKey(pa => pa.ActivityID);
        }
    }
}
