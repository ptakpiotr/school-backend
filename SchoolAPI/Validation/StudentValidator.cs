namespace SchoolAPI.Validation
{
    public class StudentValidator : AbstractValidator<StudentDTO>
    {
        public StudentValidator()
        {
            RuleFor(x => x.Imie).NotEmpty();
            RuleFor(x => x.Nazwisko).NotEmpty();
            RuleFor(x => x.Klasa_id).NotNull();
        }
    }
}
