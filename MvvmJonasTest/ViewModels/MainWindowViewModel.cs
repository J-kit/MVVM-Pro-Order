namespace MvvmJonasTest.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            UserAdministrationViewModel = new UserAdministrationViewModel();
            OrderLogViewModel = new OrderLogViewModel();
        }

        // Tab 1
        public UserAdministrationViewModel UserAdministrationViewModel { get; }

        // Tab 2
        public OrderLogViewModel OrderLogViewModel { get; }
    }
}