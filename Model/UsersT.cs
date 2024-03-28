using System;
using System.Collections.Generic;

namespace PracticeAPI.Model;

public partial class UsersT
{
    public int Id { get; set; }

    public string NickName { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public string? Salt { get; set; }

    public virtual ICollection<OrdersT> OrdersTs { get; } = new List<OrdersT>();

    public virtual RolesT Role { get; set; } = null!;
}
