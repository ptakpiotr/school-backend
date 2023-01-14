namespace SchoolAPI.Validation
{
    public class ScheduleValidator : AbstractValidator<ScheduleDTO>
    {
        public ScheduleValidator()
        {
            RuleFor(x => x.Przedmiot_oddzial_id).NotNull();
            RuleFor(x => x.Termin_Do.Day).Equal(x => x.Termin_Od.Day);
            RuleFor(x => x.Termin_Od).LessThan(x => x.Termin_Do);
        }
    }
}
