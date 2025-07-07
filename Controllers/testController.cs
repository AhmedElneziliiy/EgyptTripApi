using Microsoft.AspNetCore.Mvc;

namespace EgyptTripApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class testController : ControllerBase
    {
        //[Authorize(Roles = "Tourist")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello, this is a test endpoint!");
        }
    }
}
