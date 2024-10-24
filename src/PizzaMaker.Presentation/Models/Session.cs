﻿using PizzaMaker.Presentation.Models.Orders;
using PizzaMaker.Presentation.Models.Users;

namespace PizzaMaker.Presentation.Models;

public class Session
{
    public User? CurrentUser { get; set; }
    public List<CartItem> Items { get; set; } = [];
}