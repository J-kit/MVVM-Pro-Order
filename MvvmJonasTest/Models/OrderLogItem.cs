using System;

namespace MvvmJonasTest.Models
{
    public class OrderLogItem : OrderModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}