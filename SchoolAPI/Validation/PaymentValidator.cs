namespace SchoolAPI.Validation
{
    /// <summary>
    /// Walidator dla nowo wprowadzonej przez użytkownika płatności 
    /// </summary>
    public class PaymentValidator : AbstractValidator<PaymentDTO>
    {
        public PaymentValidator()
        {
            RuleFor(x => x.Wartosc).GreaterThan(0U);
            RuleFor(x => x.Rodzaj_id).NotNull();
            RuleFor(x => x.Uczen_id).NotNull();
        }
    }
}
