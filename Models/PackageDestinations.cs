namespace EgyptTripApi.Models
{
    public class PackageDestinations
    {
        public string PackageID { get; set; } = null!;
        public string DestinationID { get; set; } = null!;

        // Navigation properties
        public TourPackage TourPackage { get; set; } = null!;
        public Destination Destination { get; set; } = null!;
    }
}
