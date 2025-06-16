namespace EgyptTripApi.Models
{
    public class TourismCompany
    {
        public string CompanyID { get; set; } = Guid.NewGuid().ToString();
        public string UserID { get; set; } = null!;
        public string CompanyName { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;

        // Navigation properties
        public ApplicationUser User { get; set; } = null!;
        public ICollection<TourPackage> TourPackages { get; set; } = new List<TourPackage>();
    }
}
