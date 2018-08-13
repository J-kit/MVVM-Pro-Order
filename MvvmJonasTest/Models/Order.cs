using System;
using System.Collections.Generic;
using System.Linq;

namespace MvvmJonasTest.Models
{
    public class Order : IdBase
    {
        public DateTime OrderDate { get; set; }

        public string Comment { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public double TotalPrice => OrderItems?.Sum(x => x.TotalPrice) ?? 0;
    }
}