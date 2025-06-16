using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EgyptTripApi.Context;
using EgyptTripApi.DTOs.AccountDTOs;
using EgyptTripApi.Helpers;
using EgyptTripApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EgyptTripApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly EgyptTripDbContext _context;
        private readonly JWT _jwt;


        public AuthService(
                   UserManager<ApplicationUser> userManager,
                   RoleManager<ApplicationRole> roleManager,
                   EgyptTripDbContext context,
                   IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _jwt = jwt.Value;
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            
            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? string.Empty),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<AuthResponse> RegisterTouristAsync(RegisterTouristDto model)
        {
            // TODO: Replace with your DTO-to-ApplicationUser mapping
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FName,
                LastName = model.LName,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Preferences = model.Preferences,
                FavoriteDestinations = model.FavoriteDestinations
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new AuthResponse(false, errors: result.Errors.Select(e => e.Description).ToArray());

            await _userManager.AddToRoleAsync(user, "Tourist");
            var token = await GenerateJwtToken(user);
            return new AuthResponse(true, token: token, message: "Tourist registered successfully");
        }

        public async Task<AuthResponse> RegisterTourGuideAsync(RegisterTourGuideDto model)
        {
            // TODO: Replace with your DTO-to-ApplicationUser mapping
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FName,
                LastName = model.LName,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new AuthResponse(false, errors: result.Errors.Select(e => e.Description).ToArray());

            // TODO: Replace with your DTO-to-TourGuide mapping
            var tourGuide = new TourGuide
            {
                GuideID = Guid.NewGuid().ToString(),
                UserID = user.Id,
                GuideName = model.GuideName,
                LicenseNumber = model.LicenseNumber,
                Languages = model.Languages,
                AreasCovered = model.AreasCovered,
                PricePerHour = model.PricePerHour,
                ContactEmail = model.ContactEmail,
                ContactPhone = model.ContactPhone
            };

            _context.TourGuides.Add(tourGuide);
            await _context.SaveChangesAsync();
            await _userManager.AddToRoleAsync(user, "TourGuide");
            var token = await GenerateJwtToken(user);
            return new AuthResponse(true, token: token, message: "TourGuide registered successfully");
        }

        public async Task<AuthResponse> RegisterHotelAsync(RegisterHotelDto model)
        {
            // TODO: Replace with your DTO-to-ApplicationUser mapping
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new AuthResponse(false, errors: result.Errors.Select(e => e.Description).ToArray());

            // TODO: Replace with your DTO-to-Hotel mapping
            var hotel = new Hotel
            {
                HotelID = Guid.NewGuid().ToString(),
                UserID = user.Id,
                HotelName = model.HotelName,
                Address = model.HotelAddress,
                Description = model.Description,
                Rating = model.Rating,
                ContactEmail = model.ContactEmail,
                ContactPhone = model.ContactPhone
            };

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
            await _userManager.AddToRoleAsync(user, "Hotel");
            var token = await GenerateJwtToken(user);
            return new AuthResponse(true, token: token, message: "Hotel registered successfully");
        }

        public async Task<AuthResponse> RegisterTourismCompanyAsync(RegisterTourismCompanyDto model)
        {
            // TODO: Replace with your DTO-to-ApplicationUser mapping
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new AuthResponse(false, errors: result.Errors.Select(e => e.Description).ToArray());

            // TODO: Replace with your DTO-to-TourismCompany mapping
            var company = new TourismCompany
            {
                CompanyID = Guid.NewGuid().ToString(),
                UserID = user.Id,
                CompanyName = model.CompanyName,
                LicenseNumber = model.LicenseNumber,
                Description = model.Description,
                ContactEmail = model.ContactEmail,
                ContactPhone = model.ContactPhone
            };

            _context.TourismCompanies.Add(company);
            await _context.SaveChangesAsync();
            await _userManager.AddToRoleAsync(user, "TourismCompany");
            var token = await GenerateJwtToken(user);
            return new AuthResponse(true, token: token, message: "TourismCompany registered successfully");
        }

        public async Task<AuthResponse> RegisterAdminAsync(RegisterAdminDto model)
        {
            // TODO: Replace with your DTO-to-ApplicationUser mapping
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new AuthResponse(false, errors: result.Errors.Select(e => e.Description).ToArray());

            await _userManager.AddToRoleAsync(user, "Admin");
            var token = await GenerateJwtToken(user);
            return new AuthResponse(true, token: token, message: "Admin registered successfully");
        }

        public async Task<AuthResponse> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return new AuthResponse(false, message: "Invalid email or password");

            var token = await GenerateJwtToken(user);
            return new AuthResponse(true, token: token, message: "Login successful");
        }


    }
}