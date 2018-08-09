using System;

namespace MvvmJonasTest.Models
{
    public class OrderModel : ModelBase
    {
        public DateTime OrderDate { get; set; }
        public double Price { get; set; }
        public string Anmerkungen { get; set; }
    }
}