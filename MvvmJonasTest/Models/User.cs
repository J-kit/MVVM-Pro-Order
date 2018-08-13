using System.Collections.Generic;

namespace MvvmJonasTest.Models
{
    public class User : ModelBase
    {
        public string PersonalText { get; set; }
        public List<Order> Orders { get; set; }
    }
}