namespace SchoolAPI.Validation
{
    /// <summary>
    /// Walidator dla nowo wprowadzonej przez użytkownika informacji o planie zajęć
    /// </summary>
    public class ScheduleValidator : AbstractValidator<ScheduleDTO>
    {
        public ScheduleValidator()
        {
            RuleFor(x => x.Przedmiot_oddzial_id).NotNull();
            RuleFor(x => x.Termin_Od).LessThan(x => x.Termin_Do);
        }
    }
}
