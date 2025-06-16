namespace EgyptTripApi.Models
{
    public class Activity
    {
        public string ActivityID { get; set; } = Guid.NewGuid().ToString();
        public string ActivityName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // Navigation property
        public ICollection<PackageActivities> PackageActivities { get; set; } = new List<PackageActivities>();
    }
}
