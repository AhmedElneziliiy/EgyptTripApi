namespace EgyptTripApi.Models
{
    public class Booking
    {
        public string BookingID { get; set; } = Guid.NewGuid().ToString();
        public string UserID { get; set; } = null!;
        public string? PackageID { get; set; }
        public string? HotelID { get; set; }
        public string? RoomID { get; set; }
        public string? GuideID { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;

        // Navigation properties
        public ApplicationUser User { get; set; } = null!;
        public TourPackage? TourPackage { get; set; }
        public Hotel? Hotel { get; set; }
        public Room? Room { get; set; }
        public TourGuide? TourGuide { get; set; }
    }
}
