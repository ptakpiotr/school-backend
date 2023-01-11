using FluentValidation;
using School.DataAccess.Models.Dtos;

namespace SchoolAPI.Validation
{
    public class SubjectValidator : AbstractValidator<SubjectDTO>
    {
        public SubjectValidator()
        {
            RuleFor(x => x.Nazwa_przedmiotu).NotEmpty();
        }
    }
}
