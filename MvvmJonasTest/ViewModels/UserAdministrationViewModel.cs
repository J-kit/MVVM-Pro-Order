using MvvmJonasTest.Models;

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace MvvmJonasTest.ViewModels
{
    public class UserAdministrationViewModel : ViewModelBase
    {
        private UserViewModel _selectedUser;
        private OrderViewModel _selectedOrder;
        private OrderItemViewModel _selectedOrderItem;

        public UserAdministrationViewModel()
        {
            Users = new ObservableCollection<UserViewModel>(ModelGenerator.GetUserModels()
                .Select(x => new UserViewModel(x)));
            _selectedUser = Users.FirstOrDefault();
            _selectedOrder = _selectedUser?.Orders.FirstOrDefault();
        }

        public ObservableCollection<UserViewModel> Users { get; private set; }

        public UserViewModel SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (Equals(value, _selectedUser))
                {
                    return;
                }

                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        public OrderViewModel SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                if (Equals(value, _selectedOrder))
                {
                    return;
                }

                _selectedOrder = value;
                OnPropertyChanged();
            }
        }

        public OrderItemViewModel SelectedOrderItem
        {
            get => _selectedOrderItem;
            set
            {
                if (Equals(value, _selectedOrderItem))
                {
                    return;
                }

                _selectedOrderItem = value;
                OnPropertyChanged();
            }
        }

        public ICommand DoSomethingCommand
        {
            get { return new RelayCommand(x => Debugger.Break()); }
        }
    }
}