﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyOrders.Web.ViewModels.Cart
{
    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } 
            = new List<CartItemViewModel>();

        public decimal Total => Items.Sum(i => i.Total);

        public string SelectedLocation { get; set; } = null!;
    }
}
