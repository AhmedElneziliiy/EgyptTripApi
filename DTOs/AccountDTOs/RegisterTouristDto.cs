namespace EgyptTripApi.DTOs.AccountDTOs
{
    public class RegisterTouristDto
    {
        public string FName { get; set; }
        public string? LName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Preferences { get; set; } = string.Empty;
        public string? FavoriteDestinations { get; set; }
    }
}
