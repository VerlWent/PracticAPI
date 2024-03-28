using Microsoft.AspNetCore.Mvc;
//using PracticeAPI.Model;
using BCrypt.Net;
using MediatR;
using PracticeAPI.MediatR.Сontainer;
using PracticeAPI.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PracticeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : Controller
    {
        DataContext _context = new DataContext();

        [HttpGet]
        public IActionResult CheckUser(string Login, string Password)
        {
            var CheckLogin = _context.UsersTs.FirstOrDefault(x => x.Login == Login);

            if (CheckLogin == null)
            {
                return NotFound("Пользователь не найден");
            }

            string HashedPassword = BCrypt.Net.BCrypt.HashPassword(Password, CheckLogin.Salt);

            var GetUser = _context.UsersTs.FirstOrDefault(x => x.Login == Login && x.Password == HashedPassword);

            if (GetUser == null)
            {
                return NotFound("Пользователь не найден");
            }
            else
            {
				var token = Generate(GetUser);

				var result = new UserTokenResponse
				{
					Token = token,
					User = GetUser
				};

				return Ok(result);
            }

            return BadRequest();
        }

		private string Generate(UsersT user) // генерация токена для пользователя
		{
			var config = new ConfigurationBuilder()
		   .SetBasePath(Directory.GetCurrentDirectory())
		   .AddJsonFile("appsettings.json")
		   .Build();

			var authOptions = config.GetSection("AuthOptions");
			string key = authOptions["KEY"];

			var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("mysupersecret_secretsecretsecretkey!123"));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.NickName),
				new Claim(ClaimTypes.Role, user.RoleId.ToString())
			};

			var token = new JwtSecurityToken("http://localhost/", "http://localhost/",
				claims,
				expires: DateTime.Now.AddMinutes(15),
				signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		//private readonly IMediator _mediator;

		//public AuthorizationController(IMediator mediator)
		//{
		//    _mediator = mediator;
		//}

		//[HttpGet]
		//public async Task<IActionResult> CheckUser(string Login, string Password)
		//{
		//    var result = await _mediator.Send(new CheckUserQuery
		//    {
		//        Login = Login,
		//        Password = Password
		//    });

		//    if (result == null)
		//    {
		//        return NotFound("Пользователь не найден");
		//    }

		//    return Ok(result);

		//}
	}
}
