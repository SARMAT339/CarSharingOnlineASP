using Microsoft.AspNetCore.Mvc;
using CarSharingOnlineASP.Services;

namespace CarSharingOnlineASP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentController : ControllerBase
    {
        private readonly IRentService rentService;

        public RentController(IRentService rentService)
        {
            this.rentService = rentService;
        }

        [HttpPost("start")]
        public IActionResult StartRent([FromBody] StartRentRequest request)
        {
            try
            {
                var rent = rentService.StartRent(request.UserId, request.CarId, DateTime.Now);
                return Ok(rent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("end/{rentId}")]
        public IActionResult EndRent(Guid rentId)
        {
            try
            {
                var rent = rentService.EndRent(rentId, DateTime.Now);
                return Ok(rent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{rentId}")]
        public IActionResult GetRent(Guid rentId)
        {
            try
            {
                var rent = rentService.GetRent(rentId);
                return Ok(rent);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetUserRents(Guid userId)
        {
            try
            {
                var rents = rentService.GetUserRents(userId);
                return Ok(rents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
