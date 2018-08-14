using MvvmJonasTest.ViewModels.CustomerOrderOverView;
using MvvmJonasTest.ViewModels.OrderLog;
using MvvmJonasTest.ViewModels.UserAdministration;

namespace MvvmJonasTest.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            UserAdministration = new UserAdministrationViewModel();
            OrderLog = new OrderLogViewModel();
            CustomerOrderOverView = new CustomerOrderOverViewViewModel();
        }

        // Tab 1
        public UserAdministrationViewModel UserAdministration { get; }

        // Tab 2
        public OrderLogViewModel OrderLog { get; }

        // Tab 3
        public CustomerOrderOverViewViewModel CustomerOrderOverView { get; }
    }
}