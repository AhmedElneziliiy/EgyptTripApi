using AutoMapper;
using DataAccess.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using static Models.Enums.Roles;

namespace Api.Seeders
{
    public class DatabaseSeeder
    {
        private readonly EgyptTripDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;

        public DatabaseSeeder(
            EgyptTripDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task SeedAsync()
        {
            // Seed Roles
            string[] roleNames = { Role.Tourist.ToString(), Role.TourGuide.ToString(), Role.TourismCompany.ToString(), Role.Hotel.ToString() };
            foreach (var roleName in roleNames)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new ApplicationRole(roleName));
                }
            }

            // Update passwords for existing users
            var existingUsers = await _userManager.Users.ToListAsync();
            foreach (var user in existingUsers)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, "P@ss123");
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to reset password for user {user.Email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            // Seed minimal users if they don't exist
            async Task<ApplicationUser> CreateUserIfNotExists(string email, string firstName, string lastName, string role, string address, string phoneNumber)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = firstName,
                        LastName = lastName,
                        UserName = (firstName + lastName).ToLower(),
                        Email = email,
                        PhoneNumber = phoneNumber,
                        Address = address
                    };
                    var result = await _userManager.CreateAsync(user, "P@ss123");
                    if (!result.Succeeded)
                        throw new Exception($"Failed to create user {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    await _userManager.AddToRoleAsync(user, role);
                }
                return user;
            }

            var tourist = await CreateUserIfNotExists(
                "alice.smith@example.com", "Alice", "Smith", Role.Tourist.ToString(),
                "123 Cairo St, Giza", "1234567890");

            var tourGuideUser = await CreateUserIfNotExists(
                "mohamed.hassan@example.com", "Mohamed", "Hassan", Role.TourGuide.ToString(),
                "456 Nile Rd, Cairo", "9876543210");

            var companyUser = await CreateUserIfNotExists(
                "khaled.mahmoud@example.com", "Khaled", "Mahmoud", Role.TourismCompany.ToString(),
                "101 Nile St, Cairo", "1112223333");

            var hotelUser = await CreateUserIfNotExists(
                "sara.ahmed@example.com", "Sara", "Ahmed", Role.Hotel.ToString(),
                "789 Sphinx Ave, Giza", "5556667777");

            // Seed TourGuide if not exists
            if (!await _context.TourGuides.AnyAsync(tg => tg.UserID == tourGuideUser.Id))
            {
                var tourGuide = new TourGuide
                {
                    GuideID = Guid.NewGuid().ToString(),
                    UserID = tourGuideUser.Id,
                    GuideName = "Mohamed The Explorer",
                    LicenseNumber = "TG123456",
                    Languages = "English, Arabic, French",
                    AreasCovered = "Giza, Luxor, Aswan",
                    PricePerHour = 50.00m,
                    ContactEmail = "mohamed.guide@example.com",
                    ContactPhone = "9876543210"
                };
                _context.TourGuides.Add(tourGuide);
            }

            // Seed TourismCompany if not exists
            if (!await _context.TourismCompanies.AnyAsync(c => c.UserID == companyUser.Id))
            {
                var tourismCompany = new TourismCompany
                {
                    CompanyID = Guid.NewGuid().ToString(),
                    UserID = companyUser.Id,
                    CompanyName = "Egypt Adventures",
                    LicenseNumber = "TC789012",
                    Description = "Leading tourism company for cultural tours",
                    ContactEmail = "info@egyptadventures.com",
                    ContactPhone = "1112223333"
                };
                _context.TourismCompanies.Add(tourismCompany);
            }

            // Seed Hotel if not exists
            if (!await _context.Hotels.AnyAsync(h => h.UserID == hotelUser.Id))
            {
                var hotel1 = new Hotel
                {
                    HotelID = Guid.NewGuid().ToString(),
                    UserID = hotelUser.Id,
                    HotelName = "Pyramid View Hotel",
                    Address = "1 Pyramid Rd, Giza",
                    Description = "Luxury hotel with views of the Pyramids",
                    Rating = 4.5m,
                    ContactEmail = "contact@pyramidview.com",
                    ContactPhone = "5556667777"
                };
                _context.Hotels.Add(hotel1);
            }

            await _context.SaveChangesAsync();

            // Seed Rooms (table is empty)
            var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.UserID == hotelUser.Id);
            if (hotel != null && !await _context.Rooms.AnyAsync())
            {
                var room = new Room
                {
                    RoomID = Guid.NewGuid().ToString(),
                    HotelID = hotel.HotelID,
                    RoomType = "Deluxe",
                    PricePerNight = 100.00m,
                    IsAvailable = true
                };
                _context.Rooms.Add(room);
            }

            // Seed TourPackages (table is empty)
            var company = await _context.TourismCompanies.FirstOrDefaultAsync(c => c.UserID == companyUser.Id);
            if (company != null && !await _context.TourPackages.AnyAsync())
            {
                var tourPackage = new TourPackage
                {
                    PackageID = Guid.NewGuid().ToString(),
                    CompanyID = company.CompanyID,
                    PackageName = "Luxor Cultural Tour",
                    Description = "3-day tour of Luxor temples",
                    Price = 300.00m,
                    DurationDays = 3,
                    StartDate = DateTime.UtcNow.AddDays(5),
                    EndDate = DateTime.UtcNow.AddDays(8)
                };
                _context.TourPackages.Add(tourPackage);
            }

            await _context.SaveChangesAsync();

            // Seed Bookings (table is empty)
            if (!await _context.Bookings.AnyAsync())
            {
                var tourGuide = await _context.TourGuides.FirstOrDefaultAsync(tg => tg.UserID == tourGuideUser.Id);
                var tourPackage = company != null
                    ? await _context.TourPackages.FirstOrDefaultAsync(p => p.CompanyID == company.CompanyID)
                    : null;
                var room = hotel != null
                    ? await _context.Rooms.FirstOrDefaultAsync(r => r.HotelID == hotel.HotelID)
                    : null;

                var booking1 = new Booking
                {
                    BookingID = Guid.NewGuid().ToString(),
                    UserID = tourist.Id,
                    GuideID = tourGuide != null ? tourGuide.GuideID : null,
                    BookingDate = DateTime.UtcNow.AddDays(7),
                    TotalPrice = 100.00m,
                    Status = "Pending"
                };
                _context.Bookings.Add(booking1);

                if (tourPackage != null)
                {
                    var booking2 = new Booking
                    {
                        BookingID = Guid.NewGuid().ToString(),
                        UserID = tourist.Id,
                        PackageID = tourPackage.PackageID,
                        HotelID = hotel != null ? hotel.HotelID : null,
                        RoomID = room != null ? room.RoomID : null,
                        BookingDate = tourPackage.StartDate.AddDays(1),
                        TotalPrice = 400.00m, // Includes package + room
                        Status = "Pending"
                    };
                    _context.Bookings.Add(booking2);
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}