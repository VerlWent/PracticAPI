using System;
using System.Collections.Generic;

namespace PracticeAPI.Model;

public partial class RolesT
{
    public int Id { get; set; }

    public string NameRole { get; set; } = null!;

    public virtual ICollection<UsersT> UsersTs { get; } = new List<UsersT>();
}
