using Microsoft.AspNetCore.Mvc;
using School.DataAccess.Models;
using School.DataAccess.Models.Dtos;
using School.DataAccess.Services.Contracts;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ILogger<ScheduleController> _logger;
        private readonly IScheduleService _scheduleService;

        public ScheduleController(ILogger<ScheduleController> logger, IScheduleService scheduleService)
        {
            _logger = logger;
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedules()
        {
            List<ScheduleModel> schedules = await _scheduleService.GetFullSchedule();

            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchedule(int id)
        {
            ScheduleModel schedule = await _scheduleService.GetScheduleById(id);

            if (schedule is null)
            {
                return NotFound();
            }

            return Ok(schedule);
        }

        [HttpPost]
        public async Task<IActionResult> AddSchedule(ScheduleDTO schedule)
        {
            if (ModelState.IsValid)
            {
                await _scheduleService.AddSchedule(schedule);

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            ScheduleModel schedule = await _scheduleService.GetScheduleById(id);

            if (schedule is null)
            {
                return NotFound();
            }

            await _scheduleService.DeleteSchedule(id);

            return NoContent();
        }
    }
}
