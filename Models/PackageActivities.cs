namespace EgyptTripApi.Models
{
    public class PackageActivities
    {
        public string PackageID { get; set; } = null!;
        public string ActivityID { get; set; } = null!;

        // Navigation properties
        public TourPackage TourPackage { get; set; } = null!;
        public Activity Activity { get; set; } = null!;
    }
}
