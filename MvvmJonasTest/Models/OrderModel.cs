using System;

namespace MvvmJonasTest.Models
{
    public class OrderModel
    {

        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Anmerkungen { get; set; }
    }
}