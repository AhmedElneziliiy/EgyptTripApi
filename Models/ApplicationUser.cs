
using Microsoft.AspNetCore.Identity;

namespace EgyptTripApi.Models
{
    public class ApplicationUser:IdentityUser<string>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Address { get; set; } = string.Empty;
        // Tourist-specific fields
        public string Preferences { get; set; } = string.Empty;
        public string? FavoriteDestinations { get; set; }

        public TourismCompany? TourismCompany { get; set; }
        public Hotel? Hotel { get; set; }
        public TourGuide? TourGuide { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
