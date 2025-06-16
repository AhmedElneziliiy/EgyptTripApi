namespace EgyptTripApi.Models
{
    public class TourPackage
    {
        public string PackageID { get; set; } = Guid.NewGuid().ToString();
        public string CompanyID { get; set; } = null!;
        public string PackageName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DurationDays { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation properties
        public TourismCompany TourismCompany { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<PackageDestinations> PackageDestinations { get; set; } = new List<PackageDestinations>();
        public ICollection<PackageActivities> PackageActivities { get; set; } = new List<PackageActivities>();
    }
}
