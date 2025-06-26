using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.BookingDTOs;
using Utility.Services.IService;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        
        [HttpPost]
        //[Authorize(Roles = "Tourist")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            try
            {
                var response = await _bookingService.CreateBookingAsync(dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new[] { ex.Message } });
            }
        }

        [HttpGet("{bookingId}")]
       // [Authorize(Roles = "Tourist,TourGuide,Hotel,TourismCompany,Admin")]
        public async Task<IActionResult> GetBooking(string bookingId)
        {
            try
            {
                var response = await _bookingService.GetBookingByIdAsync(bookingId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return NotFound(new { Errors = new[] { ex.Message } });
            }
        }

        [HttpGet("tourist/{touristEmail}")]
       // [Authorize(Roles = "Tourist,Admin")]
        public async Task<IActionResult> GetBookingsByTourist(string touristEmail)
        {
            try
            {
                var response = await _bookingService.GetBookingsByTouristAsync(touristEmail);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new[] { ex.Message } });
            }
        }

        [HttpGet("provider/{providerEmail}")]
       // [Authorize(Roles = "TourGuide,Hotel,TourismCompany,Admin")]
        public async Task<IActionResult> GetBookingsByProvider(string providerEmail)
        {
            try
            {
                var response = await _bookingService.GetBookingsByProviderAsync(providerEmail);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new[] { ex.Message } });
            }
        }

        [HttpGet("status/{status}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetBookingsByStatus(string status)
        {
            try
            {
                var response = await _bookingService.GetBookingsByStatusAsync(status);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new[] { ex.Message } });
            }
        }

        [HttpPut("{bookingId}")]
      // [Authorize(Roles = "Tourist,Admin")]
        public async Task<IActionResult> UpdateBooking(string bookingId, [FromBody] UpdateBookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            try
            {
                var response = await _bookingService.UpdateBookingAsync(bookingId, dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new[] { ex.Message } });
            }
        }

        [HttpDelete("{bookingId}")]
        //[Authorize(Roles = "Tourist,Admin")]
        public async Task<IActionResult> DeleteBooking(string bookingId)
        {
            try
            {
                await _bookingService.DeleteBookingAsync(bookingId);
                return Ok(new { Message = "Booking deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new[] { ex.Message } });
            }
        }

        //---------------------------------------
        //---------------------------------------
        //---------------------------------------
        //---------------------------------------
        //---------------------------------------
        [HttpPost("hotel")]
        //[Authorize(Roles = "Tourist")]
        public async Task<IActionResult> CreateHotelBooking([FromBody] CreateHotelBookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            try
            {
                var response = await _bookingService.CreateHotelBookingAsync(dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new[] { ex.Message } });
            }
        }
        [HttpPost("tourguide")]
        //[Authorize(Roles = "Tourist")]
        public async Task<IActionResult> CreateTourGuideBooking([FromBody] CreateTourGuideBookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            try
            {
                var response = await _bookingService.CreateTourGuideBookingAsync(dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new[] { ex.Message } });
            }
        }

        [HttpPost("package")]
        //[Authorize(Roles = "Tourist")]
        public async Task<IActionResult> CreatePackageBooking([FromBody] CreatePackageBookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            try
            {
                var response = await _bookingService.CreatePackageBookingAsync(dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new[] { ex.Message } });
            }
        }

    }
}
