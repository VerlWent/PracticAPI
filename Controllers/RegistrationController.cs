using Microsoft.AspNetCore.Mvc;
using PracticeAPI.Model;
using BCrypt;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using PracticeAPI.MediatR.Сontainer;

namespace PracticeAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : Controller
    {
        //DataContext _context = new DataContext();

        //[HttpPost]
        //public IActionResult AddedUser(UsersT GetUser)
        //{
        //    if (GetUser == null)
        //    {
        //        return NotFound("Ошибка отправки данных");
        //    }

        //    var CheckLogin = _context.UsersTs.FirstOrDefault(x => x.Login == GetUser.Login);

        //    if (CheckLogin != null)
        //    {
        //        return Conflict("Данный логин уже занят");
        //    }

        //    string salt = BCrypt.Net.BCrypt.GenerateSalt();
        //    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(GetUser.Password, salt);
        //    GetUser.Password = hashedPassword;
        //    GetUser.Salt = salt;

        //    GetUser.Role = null;

        //    try
        //    {
        //        _context.UsersTs.Add(GetUser);
        //        _context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //    return Ok();
        //}

        private readonly IMediator _mediator;

        public RegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (result == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
