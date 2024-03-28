using System;
using System.Collections.Generic;

namespace PracticeAPI.Model;

public partial class ProductsT
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly DateAdded { get; set; }

    public string? Image { get; set; }

    public double Price { get; set; }

    public int CategoryId { get; set; }

    public int? CountInStock { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrdersT> OrdersTs { get; } = new List<OrdersT>();
}
