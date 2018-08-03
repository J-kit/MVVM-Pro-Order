using MvvmJonasTest.Annotations;
using MvvmJonasTest.Models;

using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MvvmJonasTest.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private OrderModelViewModel _currentOrder;

        public UserViewModel([NotNull]UserModel model)
        {
            UserModel = model ?? throw new ArgumentException("Model can't be null");
            Orders = new ObservableCollection<OrderModelViewModel>(model.Orders.Select(x => new OrderModelViewModel(x)));
            _currentOrder = Orders.FirstOrDefault();
        }

        public UserModel UserModel { get; private set; }



        public string UserName => UserModel.Name;

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

        public ObservableCollection<OrderModelViewModel> Orders { get; private set; }

        public OrderModelViewModel CurrentOrder
        {
            get => _currentOrder;
            set
            {
                if (Equals(value, _currentOrder)) return;
                _currentOrder = value;
                OnPropertyChanged();
            }
        }
    }
}