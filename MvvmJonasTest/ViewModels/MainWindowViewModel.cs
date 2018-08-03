using MvvmJonasTest.Models;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MvvmJonasTest.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private UserViewModel _selectedUser;

        public MainWindowViewModel()
            : this(ModelGenerator.GenerateUserModels())
        {

        }

        public MainWindowViewModel(IEnumerable<UserModel> models)
        {
            Users = new ObservableCollection<UserViewModel>(models.Select(x => new UserViewModel(x)));
            _selectedUser = Users.FirstOrDefault();
        }

        public UserViewModel SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (Equals(value, _selectedUser)) return;
                _selectedUser = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<UserViewModel> Users { get; private set; }

        public ICommand DoSomethingCommand
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
