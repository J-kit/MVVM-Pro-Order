using MvvmJonasTest.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MvvmJonasTest.ViewModels
{
    public class OrderLogFilterViewModel : ViewModelBase
    {
        private const int SelectedUserIndex = 0;
        private const int SelectedProductIndex = 1;

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

        public IEnumerable<OrderLogItem> FilterItems(IEnumerable<OrderLogItem> items)
        {
            var logItems = items;

            if (SelectedUser != null)
            {
                var suid = SelectedUser.Id;
                logItems = logItems.Where(x => x.UserId == suid);
            }

            if (SelectedProduct != null)
            {
                var spid = SelectedProduct.Id;
                logItems = logItems.Where(x => x.ProductId == spid);
            }

            return logItems;
        }
    }

    public class OrderLogViewModel : ViewModelBase
    {
        private IEnumerable<OrderLogItem> _orderModels;

        public OrderLogViewModel()
        {
            LogFilter = new OrderLogFilterViewModel();

            _orderModels = ModelGenerator.GetOrderLogItems();

            var userModels = ModelGenerator.GetUserModels();
            var productModels = ModelGenerator.GetProducts();

            // OrderLogItems = _orderModels.Select(x => new OrderLogItemViewModel(x, LogFilter)).ToList();
            Users = new ObservableCollection<ModelBase>(userModels);
            Products = new ObservableCollection<ModelBase>(productModels);

            LogFilter.PropertyChanged += LogFilter_PropertyChanged;
        }

        private void LogFilter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(OrderLogItems));
        }

        public IEnumerable<OrderLogItemViewModel> OrderLogItems => LogFilter.FilterItems(_orderModels).Select(x => new OrderLogItemViewModel(x, LogFilter));
        public IReadOnlyList<ModelBase> Users { get; private set; }
        public IReadOnlyList<ModelBase> Products { get; private set; }

        public OrderLogFilterViewModel LogFilter { get; set; }

        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }

        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }
    }
}