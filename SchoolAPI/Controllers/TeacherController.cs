using Microsoft.AspNetCore.Mvc;
using School.DataAccess.Models;
using School.DataAccess.Models.Dtos;
using School.DataAccess.Services.Contracts;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            List<TeacherModel> teachers = await _teacherService.GetTeachers();

            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher(int id)
        {
            TeacherModel teacher = await _teacherService.GetTeacherById(id);

            if (teacher is null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher(TeacherDTO teacher)
        {
            if (ModelState.IsValid)
            {
                await _teacherService.AddTeacher(teacher);

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            TeacherModel teacher = await _teacherService.GetTeacherById(id);

            if (teacher is null)
            {
                return NotFound();
            }

            await _teacherService.DeleteTeacher(id);

            return NoContent();
        }

    }
}
