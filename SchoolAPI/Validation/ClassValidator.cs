namespace SchoolAPI.Validation
{
    /// <summary>
    /// Walidator dla klasy
    /// </summary>
    public class ClassValidator : AbstractValidator<ClassDTO>
    {
        public ClassValidator()
        {
            RuleFor(x => x.Wychowawca_id).NotEmpty();
            RuleFor(x => x.Rok).GreaterThanOrEqualTo(1).LessThanOrEqualTo(8);
        }
    }
}
