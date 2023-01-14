using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
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
                return StatusCode(Constants.DataNotFound);
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
                return StatusCode(Constants.DataNotFound);
            }

            await _teacherService.DeleteTeacher(id);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> EditTeacher(int id, JsonPatchDocument<TeacherDTO> teacherDTO)
        {
            TeacherModel teacher = await _teacherService.GetTeacherById(id);
            if (teacher is null)
            {
                return BadRequest();
            }
            TeacherDTO teacherMapped = _mapper.Map<TeacherDTO>(teacher);

            teacherDTO.ApplyTo(teacherMapped);

            teacher = _mapper.Map<TeacherModel>(teacherMapped);

            await _teacherService.UpdateTeacher(id, teacher);

            return NoContent();
        }

    }
}
