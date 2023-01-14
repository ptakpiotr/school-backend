namespace SchoolAPI.Validation
{
    public class PaymentTypeValidator : AbstractValidator<PaymentTypeDTO>
    {
        public PaymentTypeValidator()
        {
            RuleFor(x => x.Powod).NotEmpty();
        }
    }
}
