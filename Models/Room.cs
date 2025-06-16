namespace EgyptTripApi.Models
{
    public class Room
    {
        public string RoomID { get; set; } = Guid.NewGuid().ToString();
        public string HotelID { get; set; } = null!;
        public string RoomType { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }

        // Navigation properties
        public Hotel Hotel { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
