namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _studentService;

        public StudentController(ILogger<StudentController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            List<StudentModel> teachers = await _studentService.GetStudents();

            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            StudentModel subject = await _studentService.GetStudentById(id);

            if (subject is null)
            {
                return StatusCode(Constants.DataNotFound);
            }

            return Ok(subject);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentDTO subject)
        {
            if (ModelState.IsValid)
            {
                await _studentService.AddStudent(subject);

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            StudentModel subject = await _studentService.GetStudentById(id);

            if (subject is null)
            {
                return StatusCode(Constants.DataNotFound);
            }

            await _studentService.DeleteStudent(id);

            return NoContent();
        }
    }
}
