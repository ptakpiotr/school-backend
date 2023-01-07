using Microsoft.AspNetCore.Mvc;
using School.DataAccess.Models;
using School.DataAccess.Services.Contracts;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly ILogger<GradeController> _logger;
        private readonly IGradeService _gradeService;

        public GradeController(ILogger<GradeController> logger, IGradeService gradeService)
        {
            _logger = logger;
            _gradeService = gradeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGrades()
        {
            List<GradeModel> grades = await _gradeService.GetGrades();

            return Ok(grades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGrade(int id)
        {
            GradeModel grade = await _gradeService.GetGrade(id);

            if (grade is null)
            {
                return NotFound();
            }

            return Ok(grade);
        }

        [HttpPost]
        public async Task<IActionResult> AddGrade(GradeModel grade)
        {
            if (ModelState.IsValid)
            {
                await _gradeService.AddGrade(grade);

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            GradeModel grade = await _gradeService.GetGrade(id);

            if (grade is null)
            {
                return NotFound();
            }

            await _gradeService.DeleteGrade(id);

            return NoContent();
        }

        [HttpGet("grouped")]
        public async Task<IActionResult> GetGroupedGrades([FromQuery] int? classId, [FromQuery] string? np)
        {
            if (classId is null)
            {
                classId = -1;
            }

            if (string.IsNullOrEmpty(np))
            {
                np = "-1";
            }

            List<GroupedGradesModel> res = await _gradeService.GetGroupedGrades(classId.Value, np);

            return Ok(res);
        }

        [HttpGet("studentGrades/{id}")]
        public async Task<IActionResult> GetStudentGrades(int id)
        {
            List<StudentGradesModel> res = await _gradeService.GetStudentGrades(id);

            if (res is null || res.Count == 0)
            {
                return NotFound();
            }

            return Ok(res);
        }
    }
}
