using System;

namespace MvvmJonasTest.Models
{
    public class OrderLogItem : Order
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}