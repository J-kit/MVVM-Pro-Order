namespace MvvmJonasTest.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            UserAdministration = new UserAdministrationViewModel();
            OrderLog = new OrderLogViewModel();
        }

        // Tab 1
        public UserAdministrationViewModel UserAdministration { get; }

        // Tab 2
        public OrderLogViewModel OrderLog { get; }
    }
}