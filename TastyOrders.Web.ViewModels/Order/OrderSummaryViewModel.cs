﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyOrders.Web.ViewModels.Order
{
    public class OrderSummaryViewModel
    {
        public int Id { get; set; }
        public string OrderDate { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public List<string> Items { get; set; } = new List<string>();
    }
}
