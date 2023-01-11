using FluentValidation;
using School.DataAccess.Models.Dtos;

namespace SchoolAPI.Validation
{
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
