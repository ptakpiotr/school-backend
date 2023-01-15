namespace SchoolAPI.Validation
{
    /// <summary>
    /// Walidator dla nauczyciela
    /// </summary>
    public class TeacherValidator : AbstractValidator<TeacherDTO>
    {
        public TeacherValidator()
        {
            RuleFor(x => x.Imie).NotEmpty();
            RuleFor(x => x.Nazwisko).NotEmpty();
        }
    }
}
