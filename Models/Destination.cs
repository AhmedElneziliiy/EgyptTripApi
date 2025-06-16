namespace EgyptTripApi.Models
{
    public class Destination
    {
        public string DestinationID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        // Navigation property
        public ICollection<PackageDestinations> PackageDestinations { get; set; } = new List<PackageDestinations>();
    }
}
