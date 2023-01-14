

namespace SchoolAPI.Validation
{
    public class AttendanceValidator : AbstractValidator<AttendanceDTO>
    {
        public AttendanceValidator()
        {
            RuleFor(x => x.Uczen_id).NotEmpty();
            RuleFor(x => x.Data).NotEmpty().GreaterThan(DateTime.UtcNow);
            RuleFor(x => x.Obecny).NotEmpty();
        }
    }
}
