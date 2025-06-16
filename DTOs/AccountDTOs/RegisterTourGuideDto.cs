namespace EgyptTripApi.DTOs.AccountDTOs
{
    public class RegisterTourGuideDto
    {
        public string FName { get; set; }
        public string? LName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string GuideName { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string Languages { get; set; } = string.Empty;
        public string AreasCovered { get; set; } = string.Empty;
        public decimal PricePerHour { get; set; }
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
    }
}
