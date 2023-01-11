using FluentValidation;
using School.DataAccess.Models.Dtos;

namespace SchoolAPI.Validation
{
    public class ClassValidator : AbstractValidator<ClassDTO>
    {
        public ClassValidator()
        {
            RuleFor(x => x.Wychowawca_id).NotEmpty();
            RuleFor(x => x.Rok).GreaterThanOrEqualTo(1).LessThanOrEqualTo(8);
        }
    }
}
