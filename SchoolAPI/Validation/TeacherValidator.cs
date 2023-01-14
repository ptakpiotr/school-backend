namespace SchoolAPI.Validation
{
    public class TeacherValidator : AbstractValidator<TeacherDTO>
    {
        public TeacherValidator()
        {
            RuleFor(x => x.Imie).NotEmpty();
            RuleFor(x => x.Nazwisko).NotEmpty();
        }
    }
}
