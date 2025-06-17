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
            if (await _roleManager.Roles.AnyAsync() || await _userManager.Users.AnyAsync())
            {
                return; 
            }

            // Seed Roles
            string[] roleNames = { Role.Tourist.ToString(), Role.TourGuide.ToString(), Role.Hotel.ToString(), Role.TourismCompany.ToString(), Role.Admin.ToString() };
            foreach (var roleName in roleNames)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new ApplicationRole(roleName));
                }
            }

            // Seed Tourist
            var tourist = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Alice",
                LastName = "Smith",
                UserName = "alicesmith",
                Email = "alice.smith@example.com",
                PhoneNumber = "1234567890",
                Address = "123 Cairo St, Giza",
                Preferences = "History, Adventure",
                FavoriteDestinations = "Pyramids, Luxor"
            };
            var touristResult = await _userManager.CreateAsync(tourist, "P@ss123");
            if (touristResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(tourist, Role.Tourist.ToString());
            }

            // Seed TourGuide
            var tourGuideUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Mohamed",
                LastName = "Hassan",
                UserName = "mohamedhassan",
                Email = "mohamed.hassan@example.com",
                PhoneNumber = "9876543210",
                Address = "456 Nile Rd, Cairo"
            };
            var tourGuideResult = await _userManager.CreateAsync(tourGuideUser, "P@ss123");
            if (tourGuideResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(tourGuideUser, Role.TourGuide.ToString());
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
                await _context.SaveChangesAsync();
            }

            // Seed Hotel
            var hotelUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Sara",
                LastName = "Ahmed",
                UserName = "saraahmed",
                Email = "sara.ahmed@example.com",
                PhoneNumber = "5556667777",
                Address = "789 Sphinx Ave, Giza"
            };
            var hotelResult = await _userManager.CreateAsync(hotelUser, "P@ss123");
            if (hotelResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(hotelUser, Role.Hotel.ToString());
                var hotel = new Hotel
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
                _context.Hotels.Add(hotel);
                await _context.SaveChangesAsync();
            }

            // Seed TourismCompany
            var companyUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Khaled",
                LastName = "Mahmoud",
                UserName = "khaledmahmoud",
                Email = "khaled.mahmoud@example.com",
                PhoneNumber = "1112223333",
                Address = "101 Nile St, Cairo"
            };
            var companyResult = await _userManager.CreateAsync(companyUser, "P@ss123");
            if (companyResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(companyUser, Role.TourismCompany.ToString());
                var company = new TourismCompany
                {
                    CompanyID = Guid.NewGuid().ToString(),
                    UserID = companyUser.Id,
                    CompanyName = "Egypt Adventures",
                    LicenseNumber = "TC789012",
                    Description = "Leading tourism company for cultural tours",
                    ContactEmail = "info@egyptadventures.com",
                    ContactPhone = "1112223333"
                };
                _context.TourismCompanies.Add(company);
                await _context.SaveChangesAsync();
            }

            // Seed Admin
            var admin = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Admin",
                LastName = "User",
                UserName = "adminuser",
                Email = "admin@example.com",
                PhoneNumber = "9998887777",
                Address = "500 Admin Rd, Cairo"
            };
            var adminResult = await _userManager.CreateAsync(admin, "P@ss123");
            if (adminResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, Role.Admin.ToString());
            }

            // Additional Tourist
            var tourist2 = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Bob",
                LastName = "Johnson",
                UserName = "bobjohnson",
                Email = "bob.johnson@example.com",
                PhoneNumber = "4445556666",
                Address = "321 Luxor St, Luxor",
                Preferences = "Culture, Food",
                FavoriteDestinations = "Aswan, Alexandria"
            };
            var tourist2Result = await _userManager.CreateAsync(tourist2, "P@ss123");
            if (tourist2Result.Succeeded)
            {
                await _userManager.AddToRoleAsync(tourist2, Role.Tourist.ToString());
            }

            // Additional TourGuide
            var tourGuide2User = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Fatima",
                LastName = "Ali",
                UserName = "fatimaali",
                Email = "fatima.ali@example.com",
                PhoneNumber = "3334445555",
                Address = "654 Aswan Rd, Aswan"
            };
            var tourGuide2Result = await _userManager.CreateAsync(tourGuide2User, "P@ss123");
            if (tourGuide2Result.Succeeded)
            {
                await _userManager.AddToRoleAsync(tourGuide2User, Role.TourGuide.ToString());
                var tourGuide2 = new TourGuide
                {
                    GuideID = Guid.NewGuid().ToString(),
                    UserID = tourGuide2User.Id,
                    GuideName = "Fatima The Historian",
                    LicenseNumber = "TG789012",
                    Languages = "English, Arabic, Spanish",
                    AreasCovered = "Aswan, Abu Simbel",
                    PricePerHour = 45.00m,
                    ContactEmail = "fatima.guide@example.com",
                    ContactPhone = "3334445555"
                };
                _context.TourGuides.Add(tourGuide2);
                await _context.SaveChangesAsync();
            }
        }
    }
}