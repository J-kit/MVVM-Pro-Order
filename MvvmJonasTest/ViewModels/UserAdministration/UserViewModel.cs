using System;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmJonasTest.Annotations;
using MvvmJonasTest.Models;

namespace MvvmJonasTest.ViewModels.UserAdministration
{
    public class UserViewModel : ViewModelBase
    {
        public UserViewModel([NotNull]User model)
        {
            UserModel = model ?? throw new ArgumentException("Model can't be null");
            Orders = new ObservableCollection<OrderViewModel>(model.Orders.OrderBy(x => x.OrderDate).Select(x => new OrderViewModel(x)));
        }

        public User UserModel { get; private set; }

        public override string Name => UserModel.Name;

        public string PersonalText
        {
            get => UserModel.PersonalText;
            set
            {
                if (value == UserModel.PersonalText) return;
                UserModel.PersonalText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<OrderViewModel> Orders { get; private set; }
    }
}