using MediatR;
using PracticeAPI.Model;

namespace PracticeAPI.MediatR.Сontainer
{
    public class CheckUserQuery : IRequest<UsersT>
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
