using System.Collections.Generic;

namespace MvvmJonasTest.Models
{
    public class UserModel : ModelBase
    {
        public string PersonalText { get; set; }
        public List<OrderModel> Orders { get; set; }
    }
}