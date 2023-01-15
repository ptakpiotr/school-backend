namespace SchoolAPI.Validation
{
    /// <summary>
    /// Walidator dla rodzaju opłat
    /// </summary>
    public class PaymentTypeValidator : AbstractValidator<PaymentTypeDTO>
    {
        public PaymentTypeValidator()
        {
            RuleFor(x => x.Powod).NotEmpty();
        }
    }
}
