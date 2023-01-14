namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly ILogger<ClassController> _logger;
        private readonly IClassService _classService;

        public ClassController(ILogger<ClassController> logger, IClassService classService)
        {
            _logger = logger;
            _classService = classService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClasss()
        {
            List<ClassModel> classes = await _classService.GetAllClasses();

            return Ok(classes);
        }

        [HttpGet("avg")]
        public async Task<IActionResult> GetClassAverages([FromQuery] int? npId)
        {
            List<OperatorModel> operators = new();
            if (npId is not null)
            {
                //operators.Add(new OperatorModel { FieldName = "npId", Operator = OperatorType.GreaterThan, Value = npId });
            }

            string condition = Utils.BuildCondition(operators.ToArray());

            List<ClassAvgModel> classes = await _classService.GetClassAverages();

            return Ok(classes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClass(int id)
        {
            ClassModel subject = await _classService.GetClass(id);

            if (subject is null)
            {
                return StatusCode(Constants.DataNotFound);
            }

            return Ok(subject);
        }

        [HttpPost]
        public async Task<IActionResult> AddClass(ClassDTO subject)
        {
            if (ModelState.IsValid)
            {
                await _classService.AddClass(subject);

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            ClassModel subject = await _classService.GetClass(id);

            if (subject is null)
            {
                return StatusCode(Constants.DataNotFound);
            }

            await _classService.DeleteClass(id);

            return NoContent();
        }
    }
}
