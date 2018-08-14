using MvvmJonasTest.Models;

namespace MvvmJonasTest.ViewModels.UserAdministration
{
    public class OrderItemViewModel : ViewModelBase
    {
        private readonly OrderItem _model;

        public OrderItemViewModel(OrderItem model)
        {
            _model = model;
        }

        public override string Name => $"{_model.Product.Name} ({_model.Amount}/{_model.TotalPrice:C})";
    }
}