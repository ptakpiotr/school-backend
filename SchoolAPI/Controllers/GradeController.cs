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
    }
}
