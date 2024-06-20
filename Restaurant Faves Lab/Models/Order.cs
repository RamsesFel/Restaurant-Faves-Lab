using System;
using System.Collections.Generic;

namespace Restaurant_Faves_Lab.Models;

public partial class Order
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Restaurant { get; set; }

    public int? Rating { get; set; }

    public bool? OrderAgain { get; set; }
}
