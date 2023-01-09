using Microsoft.AspNetCore.Mvc;
using School.DataAccess;
using School.DataAccess.Models;
using School.DataAccess.Models.Dtos;
using School.DataAccess.Services.Contracts;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly ILogger<AttendanceController> _logger;
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(ILogger<AttendanceController> logger, IAttendanceService attendanceService)
        {
            _logger = logger;
            _attendanceService = attendanceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAttendances()
        {
            List<AttendanceModel> classes = await _attendanceService.GetAllAttendance();

            return Ok(classes);
        }

        [HttpPost]
        public async Task<IActionResult> AddAttendance(AttendanceDTO subject)
        {
            if (ModelState.IsValid)
            {
                await _attendanceService.AddAttendance(subject);

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            await _attendanceService.DeleteAttendance(id);

            return NoContent();
        }

        [HttpGet("byclass/{classId}")]
        public async Task<IActionResult> GetStudentGrades(int classId, [FromQuery] string? dateFrom, [FromQuery] string? dateTo)
        {
            List<OperatorModel> operators = new();
            if (!string.IsNullOrEmpty(dateFrom))
            {
                operators.Add(new OperatorModel { FieldName = "data", Operator = OperatorType.GreaterThan, Value = dateFrom });
            }

            if (!string.IsNullOrEmpty(dateTo))
            {
                operators.Add(new OperatorModel { FieldName = "data", Operator = OperatorType.LessThan, Value = dateTo });
            }

            string condition = Utils.BuildCondition(operators.ToArray());
            List<AttendancePerClassModel> res = await _attendanceService.GetAttendancePerClass(classId, condition);

            if (res is null || res.Count == 0)
            {
                return NotFound();
            }

            return Ok(res);
        }
    }
}
