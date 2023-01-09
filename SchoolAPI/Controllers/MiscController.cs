using Microsoft.AspNetCore.Mvc;
using School.DataAccess.Models;
using School.DataAccess.Models.Dtos;
using School.DataAccess.Services.Contracts;

namespace SchoolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MiscController : ControllerBase
    {
        private readonly ILogger<MiscController> _logger;
        private readonly IMiscService _miscService;

        public MiscController(ILogger<MiscController> logger, IMiscService miscService)
        {
            _logger = logger;
            _miscService = miscService;
        }

        [HttpGet("paymentType")]
        public async Task<IActionResult> GetPaymentTypes()
        {
            List<PaymentTypeModel> paymentTypes = await _miscService.GetPaymentTypes();

            return Ok(paymentTypes);
        }

        [HttpGet("room")]
        public async Task<IActionResult> GetRooms()
        {
            List<RoomModel> rooms = await _miscService.GetRooms();

            return Ok(rooms);
        }

        [HttpPost("paymentType")]
        public async Task<IActionResult> AddPaymentType(PaymentTypeDTO payment)
        {
            if (ModelState.IsValid)
            {
                await _miscService.AddPaymentType(payment);

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("room")]
        public async Task<IActionResult> AddRoom(RoomDTO room)
        {
            if (ModelState.IsValid)
            {
                await _miscService.AddRoom(room);

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("paymentType/{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                await _miscService.DeleteRoom(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("room/{id}")]
        public async Task<IActionResult> DeletePaymentType(int id)
        {
            try
            {
                await _miscService.DeletePaymentType(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
