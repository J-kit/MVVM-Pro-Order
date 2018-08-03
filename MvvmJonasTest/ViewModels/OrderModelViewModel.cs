using MvvmJonasTest.Models;

namespace MvvmJonasTest.ViewModels
{
    public class OrderModelViewModel : ViewModelBase
    {
        private readonly OrderModel _model;

        public OrderModelViewModel(OrderModel model)
        {
            _model = model;
        }


        public string ProductName => _model.ProductName;

        public double Price
        {
            get => _model.Price;
            set
            {
                if (value.Equals(_model.Price)) return;
                _model.Price = value;
                OnPropertyChanged();
            }
        }

        public string Anmerkungen
        {
            get => _model.Anmerkungen;
            set
            {
                if (value == _model.Anmerkungen) return;
                _model.Anmerkungen = value;
                OnPropertyChanged();
            }
        }


    }
}