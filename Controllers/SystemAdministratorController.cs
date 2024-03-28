using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PracticeAPI.Model;

namespace PracticeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SystemAdministratorController : Controller
    {
        private readonly ILogger<SystemAdministratorController> logger;

        public SystemAdministratorController(ILogger<SystemAdministratorController> logger)
        {
            this.logger = logger;
        }

        DataContext _context = new DataContext();

        [Authorize(Roles = "3")]
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var result = _context.UsersTs.Select(x => new
            {
                Id = x.Id,
                NickName = x.NickName,
                Login = x.Login,
                Password = x.Password,
                Role = x.Role.NameRole
            });

            return Ok(result);
        }

        [Authorize(Roles = "3")]
        [HttpGet("GetOneUser")]
        public IActionResult GetUser(int Id)
        {
            var result = _context.UsersTs
                .Where(x => x.Id == Id)
                .Select(x => new
            {
                Id = x.Id,
                NickName = x.NickName,
                Login = x.Login,
                Password = x.Password,
                Role = x.Role.NameRole
            });

            return Ok(result);
        }

        [Authorize(Roles = "3")]
        [HttpPut("EditUser")]
        public IActionResult EditUser(int Id, string NickName, string Login, string Password, string Role)
        {
            var getuser = _context.UsersTs.FirstOrDefault(x => x.Id == Id);

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password, salt);

            logger.LogInformation(Id.ToString());
            logger.LogInformation(NickName);
            logger.LogInformation(Login);
            logger.LogInformation(Password);
            logger.LogInformation(Role);

            if (getuser != null)
            {
                

                getuser.NickName = NickName;
                getuser.Login = Login;
                getuser.Password = hashedPassword;
                getuser.Salt = salt;

                if (Role == "Покупатель")
                {
                    getuser.RoleId = 1;
                }
                else if (Role == "Администратор магазина")
                {
                    getuser.RoleId = 2;
                }
                else
                {
                    getuser.RoleId = 3;
                }

                _context.Update(getuser);

                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest("Пользователь не найден");
            }
        }

        [Authorize(Roles = "3")]
        [HttpPost("CreateUser")]
        public IActionResult CreateUser(string NickName, string Login, string Password, string Role)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password, salt);

            var CheckLogin = _context.UsersTs.FirstOrDefault(x => x.Login == Login);

            if (CheckLogin != null)
            {
                return BadRequest("Данный логин уже занят");
            }

            UsersT newuser = new UsersT();

            newuser.NickName = NickName;
            newuser.Login = Login;
            newuser.Password = hashedPassword;
            newuser.Salt = salt;

            if (Role == "Покупатель")
            {
                newuser.RoleId = 1;
            }
            else if (Role == "Администратор магазина")
            {
                newuser.RoleId = 2;
            }
            else
            {
                newuser.RoleId = 3;
            }

            _context.UsersTs.Add(newuser);
            _context.SaveChanges();

            return Ok();
        }

        [Authorize(Roles = "3")]
        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(int Id)
        {
            logger.LogInformation(Id.ToString());
            var result = _context.UsersTs.FirstOrDefault(x => x.Id == Id);

            if (result != null)
            {
                _context.UsersTs.Remove(result);
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }
    }
}
