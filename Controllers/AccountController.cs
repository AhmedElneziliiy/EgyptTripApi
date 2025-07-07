
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.AccountDTOs;
using Utility.Services.IService;

namespace EgyptTripApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register/tourist")]
        public async Task<IActionResult> RegisterTourist([FromBody] RegisterTouristDto model)
        {
            var response = await _authService.RegisterTouristAsync(model);
           
            if (!response.Success && response.Message is null)
                return BadRequest(new { response.Errors});
            
            if(response.Message is not null && response.Success == false)
                return BadRequest(new { response.Message });

            return Ok(new { response.Token, response.Message });
        }

        [HttpPost("register/tourguide")]
        public async Task<IActionResult> RegisterTourGuide([FromBody] RegisterTourGuideDto model)
        {
            var response = await _authService.RegisterTourGuideAsync(model);
            if (!response.Success && response.Message is null)
                return BadRequest(new { response.Errors });

            if (response.Message is not null && response.Success == false)
                return BadRequest(new { response.Message });

            return Ok(new { response.Token, response.Message });
        }

        [HttpPost("register/hotel")]
        public async Task<IActionResult> RegisterHotel([FromBody] RegisterHotelDto model)
        {
            var response = await _authService.RegisterHotelAsync(model);
           
            if (!response.Success && response.Message is null)
                return BadRequest(new { response.Errors });

            if (response.Message is not null && response.Success == false)
                return BadRequest(new { response.Message });

            return Ok(new { response.Token, response.Message });
        }

        [HttpPost("register/tourismcompany")]
        public async Task<IActionResult> RegisterTourismCompany([FromBody] RegisterTourismCompanyDto model)
        {
            var response = await _authService.RegisterTourismCompanyAsync(model);
            
            if (!response.Success && response.Message is null)
                return BadRequest(new { response.Errors });

            if (response.Message is not null && response.Success == false)
                return BadRequest(new { response.Message });

            return Ok(new { response.Token, response.Message });
        }

        [HttpPost("register/admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminDto model)
        {
            // TODO: Restrict access to admin roles only
            var response = await _authService.RegisterAdminAsync(model);
            
            if (!response.Success && response.Message is null)
                return BadRequest(new { response.Errors });

            if (response.Message is not null && response.Success == false)
                return BadRequest(new { response.Message });

            return Ok(new { response.Token, response.Message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var response = await _authService.LoginAsync(model);
            if (!response.Success)
                return Unauthorized(new { response.Message });
            return Ok(new { response.Token, response.Message });
        }

    }
}
       
