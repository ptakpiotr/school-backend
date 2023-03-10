

namespace SchoolAPI.Validation
{
    /// <summary>
    /// Walidator dla AttendanceDTO
    /// </summary>
    public class AttendanceValidator : AbstractValidator<AttendanceDTO>
    {
        public AttendanceValidator()
        {
            RuleFor(x => x.Uczen_id).NotEmpty();
            RuleFor(x => x.Data).NotEmpty().LessThan(DateTime.UtcNow);
        }
    }
}
