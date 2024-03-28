using MediatR;
using PracticeAPI.MediatR.Classes;

namespace PracticeAPI.MediatR.Сontainer
{
    public class CreateUserCommand : IRequest<bool>
	{
		public string NickName { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public int RoleId { get; set; }
	}
}
