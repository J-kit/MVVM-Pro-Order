using MvvmJonasTest.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MvvmJonasTest.ViewModels
{
    public class OrderLogFilterViewModel : ViewModelBase
    {
        private ModelBase _selectedUser;
        private ModelBase _selectedProducts;

        public ModelBase SelectedUser
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

        public ModelBase SelectedProduct
        {
            get => _selectedProducts;
            set
            {
                if (Equals(value, _selectedProducts))
                {
                    return;
                }

                _selectedProducts = value;
                OnPropertyChanged();
            }
        }
    }

    public class OrderLogViewModel : ViewModelBase
    {
        public OrderLogViewModel()
        {
            LogFilter = new OrderLogFilterViewModel();

            var orderModels = ModelGenerator.GetOrderLogItems()
                .Select(x => new OrderLogItemViewModel(x, LogFilter))
                .ToList();

            var userModels = ModelGenerator.GetUserModels();
            var productModels = ModelGenerator.GetProducts();

            OrderLogItems = new ObservableCollection<OrderLogItemViewModel>(orderModels);
            Users = new ObservableCollection<ModelBase>(userModels);
            Products = new ObservableCollection<ModelBase>(productModels);
        }

        public ObservableCollection<OrderLogItemViewModel> OrderLogItems { get; private set; }
        public IReadOnlyList<ModelBase> Users { get; private set; }
        public IReadOnlyList<ModelBase> Products { get; private set; }

        public OrderLogFilterViewModel LogFilter { get; set; }

        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }

        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }
    }
}