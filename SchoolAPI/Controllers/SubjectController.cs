using Microsoft.AspNetCore.Mvc;
using School.DataAccess.Models;
using School.DataAccess.Models.Dtos;
using School.DataAccess.Services.Contracts;

namespace SchoolAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ILogger<SubjectController> _logger;
        private readonly ISubjectService _subjectService;

        public SubjectController(ILogger<SubjectController> logger, ISubjectService subjectService)
        {
            _logger = logger;
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjects()
        {
            List<SubjectModel> teachers = await _subjectService.GetAllSubjects();

            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            SubjectModel subject = await _subjectService.GetSubject(id);

            if (subject is null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubject(SubjectDTO subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectService.AddSubject(subject);

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            SubjectModel subject = await _subjectService.GetSubject(id);

            if (subject is null)
            {
                return NotFound();
            }

            await _subjectService.DeleteSubject(id);

            return NoContent();
        }
    }
}
