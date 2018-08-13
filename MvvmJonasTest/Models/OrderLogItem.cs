using System;

namespace MvvmJonasTest.Models
{
    public class OrderLogItem
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Comment { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
    }
}