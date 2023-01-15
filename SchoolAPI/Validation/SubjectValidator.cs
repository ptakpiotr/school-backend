namespace SchoolAPI.Validation
{
    /// <summary>
    /// Walidator dla przedmiotu
    /// </summary>
    public class SubjectValidator : AbstractValidator<SubjectDTO>
    {
        public SubjectValidator()
        {
            RuleFor(x => x.Nazwa_przedmiotu).NotEmpty();
        }
    }
}
