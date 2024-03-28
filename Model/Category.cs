using System;
using System.Collections.Generic;

namespace PracticeAPI.Model;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductsT> ProductsTs { get; } = new List<ProductsT>();
}
