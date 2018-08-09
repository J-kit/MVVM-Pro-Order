using MvvmJonasTest.Models;

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace MvvmJonasTest.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private UserViewModel _selectedUser;

        public MainWindowViewModel()
        {
            Users = new ObservableCollection<UserViewModel>(ModelGenerator.GetUserModels()
                .Select(x => new UserViewModel(x)));
            OrderLogViewModel = new OrderLogViewModel();
            _selectedUser = Users.FirstOrDefault();
        }

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

        public ObservableCollection<UserViewModel> Users { get; private set; }

        public ICommand DoSomethingCommand
        {
            get { return new RelayCommand(x => Debugger.Break()); }
        }

        public OrderLogViewModel OrderLogViewModel { get; }
    }
}