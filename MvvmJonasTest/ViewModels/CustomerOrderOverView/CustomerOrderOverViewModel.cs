using MvvmJonasTest.Models;

using System.Collections.Generic;

namespace MvvmJonasTest.ViewModels.CustomerOrderOverView
{
    public class CustomerOrderOverViewViewModel : ViewModelBase
    {
        public CustomerOrderOverViewViewModel()
        {
            Users = ModelGenerator.GetUserModels();
        }

        public IEnumerable<User> SelectedUsers { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}