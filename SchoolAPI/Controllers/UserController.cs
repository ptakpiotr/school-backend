using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using School.DataAccess.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IOptionsSnapshot<JWTOptions> _options;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IMapper mapper, IOptionsSnapshot<JWTOptions> options, IUserService userService)
        {
            _logger = logger;
            _mapper = mapper;
            _options = options;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest();
            }

            bool validPassword = await _userService.ComparePassword(_mapper.Map<UserDTO>(user));

            if (!validPassword)
            {
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new()
            {
                    new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(_options.Value.Issuer,
              _options.Value.Audience,
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                UserModel usr = _mapper.Map<UserModel>(user);
                usr.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                await _userService.CreateUser(usr);

                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("verifyJWT")]
        public IActionResult VerifyJWT(JWTValidateTokenModel token)
        {
            if (string.IsNullOrEmpty(token.Token))
            {
                return BadRequest();
            }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.UTF8.GetBytes(_options.Value.Key);
            try
            {
                tokenHandler.ValidateToken(token.Token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _options.Value.Issuer,
                    ValidAudience = _options.Value.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                return Ok(new { tokenValid = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return Unauthorized();
            }
        }
    }
}
