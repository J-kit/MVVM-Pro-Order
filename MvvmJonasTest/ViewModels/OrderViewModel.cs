using MvvmJonasTest.Models;

using System.Collections.ObjectModel;
using System.Linq;
using MvvmJonasTest.ViewModels.UserAdministration;

namespace MvvmJonasTest.ViewModels
{
    public class OrderViewModel : ViewModelBase
    {
        private readonly Order _model;

        public OrderViewModel(Order model)
        {
            _model = model;
            OrderItems = new ObservableCollection<OrderItemViewModel>(model.OrderItems.Select(x => new OrderItemViewModel(x)));
        }

        public ObservableCollection<OrderItemViewModel> OrderItems { get; set; }

        public override string Name => $"{_model.OrderDate:d} ({_model.OrderItems.Count})";

        public double Price => _model.TotalPrice;

        public string Comment
        {
            get => _model.Comment;
            set
            {
                if (value == _model.Comment)
                {
                    return;
                }

                _model.Comment = value;
                OnPropertyChanged();
            }
        }
    }
}