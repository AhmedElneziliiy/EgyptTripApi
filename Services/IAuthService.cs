using EgyptTripApi.DTOs.AccountDTOs;
using EgyptTripApi.Models;
using System.IdentityModel.Tokens.Jwt;

namespace EgyptTripApi.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterTouristAsync(RegisterTouristDto model);
        Task<AuthResponse> RegisterTourGuideAsync(RegisterTourGuideDto model);
        Task<AuthResponse> RegisterHotelAsync(RegisterHotelDto model);
        Task<AuthResponse> RegisterTourismCompanyAsync(RegisterTourismCompanyDto model);
        Task<AuthResponse> RegisterAdminAsync(RegisterAdminDto model);
        Task<AuthResponse> LoginAsync(LoginDto model);
    }
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
        public string[]? Errors { get; set; }
        public int? Duration{ get; set; }

        public AuthResponse(bool success, string? token = null, string? message = null, string[]? errors = null, int? duration = null)
        {
            Success = success;
            Token = token;
            Message = message;
            Errors = errors;
            Duration = duration;
        }
    }
}
