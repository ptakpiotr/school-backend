using School.DataAccess.Models.DTOs;

namespace SchoolAPI.Validation
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        private readonly IUserService _userService;

        public UserValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(x => x.Email).Matches(@".+@.+\..+").Must(BeUniqueEmail).WithMessage("Email exists or doesn't match the pattern");
            RuleFor(x => x.Password).NotEmpty();
        }

        public bool BeUniqueEmail(string email)
        {
            bool res = _userService.CheckUserExists(email).GetAwaiter().GetResult();

            return !res;
        }
    }
}
