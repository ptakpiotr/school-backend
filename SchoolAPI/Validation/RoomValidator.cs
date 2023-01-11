using FluentValidation;
using School.DataAccess.Models.Dtos;

namespace SchoolAPI.Validation
{
    public class RoomValidator : AbstractValidator<RoomDTO>
    {
        public RoomValidator()
        {
            RuleFor(x => x.NumerSali).NotEmpty();
        }
    }
}
