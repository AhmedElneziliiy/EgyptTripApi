using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.HotelsDTOs;
using Models.DTOs.RoomsDTOs;
using Utility.Services.IService;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            try
            {
                var hotels = await _hotelService.GetAllHotelsAsync();
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new[] { ex.Message } });
            }
        }

        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetHotel(string hotelId)
        {
            try
            {
                var hotel = await _hotelService.GetHotelByIdAsync(hotelId);
                return Ok(hotel);
            }
            catch (Exception ex)
            {
                return NotFound(new { Errors = new[] { ex.Message } });
            }
        }


        [HttpPut("{hotelId}")]
        //[Authorize(Roles = "Hotel")]
        public async Task<IActionResult> UpdateHotel(string hotelId, [FromBody] UpdateHotelDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            try
            {
                var response = await _hotelService.UpdateHotelAsync(hotelId, dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new[] { ex.Message } });
            }
        }

        [HttpDelete("{hotelId}")]
        //[Authorize(Roles = "Hotel,Admin")]
        public async Task<IActionResult> DeleteHotel(string hotelId)
        {
            try
            {
                await _hotelService.DeleteHotelAsync(hotelId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new[] { ex.Message } });
            }
        }

        [HttpGet("{hotelId}/rooms")]
        public async Task<IActionResult> GetHotelRooms(string hotelId)
        {
            try
            {
                var rooms = await _hotelService.GetHotelRoomsAsync(hotelId);
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new[] { ex.Message } });
            }
        }

        [HttpPost("{hotelId}/rooms")]
        //[Authorize(Roles = "Hotel")]
        public async Task<IActionResult> AddRoom(string hotelId, [FromBody] CreateRoomDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            try
            {
                var response = await _hotelService.AddRoomAsync(hotelId, dto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Errors = new[] { ex.Message } });
            }
        }
    }
}
