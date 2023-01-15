using School.DataAccess.Models.DTOs;

namespace SchoolAPI.Validation
{
    /// <summary>
    /// Walidator dla użytkownika aplikacji
    /// </summary>
    public class UserValidator : AbstractValidator<UserDTO>
    {
        private readonly IUserService _userService;

        public UserValidator(IUserService userService)
        {
            _userService = userService;

            RuleFor(x => x.Email).Matches(@".+@.+\..+").Must(BeUniqueEmail).WithMessage("Email exists or doesn't match the pattern");
            RuleFor(x => x.Password).NotEmpty();
        }

        /// <summary>
        /// Metoda sprawdzająca unikalność adresu email (po stronie bazy danych tylko sprawdzanie formatu maila)
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>

        public bool BeUniqueEmail(string email)
        {
            bool res = _userService.CheckUserExists(email).GetAwaiter().GetResult();

            return !res;
        }
    }
}
