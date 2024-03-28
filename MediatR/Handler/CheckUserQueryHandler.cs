using MediatR;
using PracticeAPI.MediatR.Сontainer;
using PracticeAPI.Model;

namespace PracticeAPI.MediatR.Handler
{
    public class CheckUserQueryHandler : IRequestHandler<CheckUserQuery, UsersT>
    {
        DataContext _context = new DataContext();
        public async Task<UsersT> Handle(CheckUserQuery request, CancellationToken cancellationToken)
        {
            var CheckLogin = _context.UsersTs.FirstOrDefault(x => x.Login == request.Login);

            if (CheckLogin == null)
            {
                throw new Exception("Пользователь не найден");
            }

            string HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, CheckLogin.Salt);

            var CheckPassword = _context.UsersTs.FirstOrDefault(x => x.Login == request.Login && x.Password == HashedPassword);

            if (CheckPassword == null)
            {
                throw new Exception("Пользователь не найден");
            }

            return CheckPassword;
        }
    }
}
