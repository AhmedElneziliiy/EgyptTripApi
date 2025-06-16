namespace EgyptTripApi.Models
{
    public class TourGuide
    {
        public string GuideID { get; set; } = Guid.NewGuid().ToString();
        public string UserID { get; set; } = null!;
        public string GuideName { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string Languages { get; set; } = string.Empty;
        public string AreasCovered { get; set; } = string.Empty;
        public decimal PricePerHour { get; set; }
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;

        // Navigation properties
        public ApplicationUser User { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
