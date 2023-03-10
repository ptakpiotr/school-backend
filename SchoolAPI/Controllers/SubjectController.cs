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
            List<SubjectModel> subjects = await _subjectService.GetAllSubjects();

            return Ok(subjects);
        }

        [HttpGet("detailed")]
        public async Task<IActionResult> GetDetailedSubjects()
        {
            List<DetailedSubjectModel> subjects = await _subjectService.GetDetailedSubjects();

            return Ok(subjects);
        }

        [HttpGet("subjectClasses")]
        public async Task<IActionResult> GetSubjectClasses()
        {
            List<SubjectClassModel> subjects = await _subjectService.GetSubjectClasses();

            return Ok(subjects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            SubjectModel subject = await _subjectService.GetSubject(id);

            if (subject is null)
            {
                return StatusCode(Constants.DataNotFound);
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

        [HttpPost("subjectClasses")]
        public async Task<IActionResult> AddSubjectClass(SubjectClassDTO subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectService.AddSubjectClass(subject);

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
                return StatusCode(Constants.DataNotFound);
            }

            await _subjectService.DeleteSubject(id);

            return NoContent();
        }
    }
}
