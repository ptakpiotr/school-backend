using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace SchoolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MiscController : ControllerBase
    {
        private readonly ILogger<MiscController> _logger;
        private readonly IMiscService _miscService;
        private readonly IMapper _mapper;

        public MiscController(ILogger<MiscController> logger, IMiscService miscService, IMapper mapper)
        {
            _logger = logger;
            _miscService = miscService;
            _mapper = mapper;
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

        [HttpPatch("room/{id}")]
        public async Task<IActionResult> EditRoom(int id, JsonPatchDocument<RoomDTO> roomDTO)
        {
            List<RoomModel> rooms = await _miscService.GetRooms();
            if (!rooms.Any(r => r.Id == id))
            {
                return BadRequest();
            }
            RoomModel room = rooms.FirstOrDefault(r => r.Id == id);

            RoomDTO roomMapped = _mapper.Map<RoomDTO>(room);

            roomDTO.ApplyTo(roomMapped);

            await _miscService.UpdateRoom(id, roomMapped);

            return NoContent();
        }

        [HttpPatch("paymentType/{id}")]
        public async Task<IActionResult> EditPaymentType(int id, JsonPatchDocument<PaymentTypeDTO> paymentTypeDTO)
        {
            List<PaymentTypeModel> payments = await _miscService.GetPaymentTypes();
            if (!payments.Any(r => r.Id == id))
            {
                return BadRequest();
            }
            PaymentTypeModel paymentType = payments.FirstOrDefault(r => r.Id == id);

            PaymentTypeDTO paymentTypeMapped = _mapper.Map<PaymentTypeDTO>(paymentType);

            paymentTypeDTO.ApplyTo(paymentTypeMapped);

            await _miscService.UpdatePaymentType(id, paymentTypeMapped);

            return NoContent();
        }
    }
}
