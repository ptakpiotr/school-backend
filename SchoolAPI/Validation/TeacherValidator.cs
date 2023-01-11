using FluentValidation;
using School.DataAccess.Models.Dtos;

namespace SchoolAPI.Validation
{
    public class TeacherValidator : AbstractValidator<TeacherDTO>
    {
        public TeacherValidator()
        {
            RuleForEach((x) =>
                new[] { x.Imie, x.Nazwisko }
            ).NotEmpty();
        }
    }
}
