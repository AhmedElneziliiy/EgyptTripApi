namespace EgyptTripApi.Models
{
    public class Review
    {
        public string ReviewID { get; set; } = Guid.NewGuid().ToString();
        public string UserID { get; set; } = null!;
        public string? PackageID { get; set; }
        public string? HotelID { get; set; }
        public string? GuideID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; }

        // Navigation properties
        public ApplicationUser User { get; set; } = null!;
        public TourPackage? TourPackage { get; set; }
        public Hotel? Hotel { get; set; }
        public TourGuide? TourGuide { get; set; }
    }
}
