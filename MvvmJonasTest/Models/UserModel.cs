using System;
using System.Collections.Generic;

namespace MvvmJonasTest.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PersonalText { get; set; }
        public List<OrderModel> Orders { get; set; }
    }
}
