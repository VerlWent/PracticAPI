using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PracticeAPI.Model;

public partial class OrdersT
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductKeyId { get; set; }

    public DateOnly? DatePurchase { get; set; }

    public int? Count { get; set; }
    
    public virtual ProductsT ProductKey { get; set; } = null!;

    public virtual UsersT User { get; set; } = null!;
}
