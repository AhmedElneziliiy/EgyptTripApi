namespace EgyptTripApi.Models
{
    public class Hotel
    {
        public string HotelID { get; set; } = Guid.NewGuid().ToString();
        public string UserID { get; set; } = null!;
        public string HotelName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;

        // Navigation properties
        public ApplicationUser User { get; set; } = null!;
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
