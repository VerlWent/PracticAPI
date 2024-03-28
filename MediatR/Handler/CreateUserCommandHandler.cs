using MediatR;
using PracticeAPI.MediatR.Сontainer;
using PracticeAPI.Model;
using BCrypt;
using PracticeAPI.MediatR.Classes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PracticeAPI.MediatR.Handler
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
	{
        DataContext _context = new DataContext();
        public async Task<bool> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(command.Password, salt);

            var CheckLogin = _context.UsersTs.FirstOrDefault(x => x.Login == command.Login);

            if (CheckLogin != null)
            {
                return false;
			}

            var NewUser = new UsersT
            {
                NickName = command.NickName,
                Login = command.Login,
                Password = hashedPassword,
                Salt = salt,
                RoleId = command.RoleId
            };

            _context.UsersTs.Add(NewUser);

            await _context.SaveChangesAsync();

            return true;
		}
    }
}
