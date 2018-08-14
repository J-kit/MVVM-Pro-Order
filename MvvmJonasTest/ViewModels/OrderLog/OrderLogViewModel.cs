using MvvmJonasTest.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MvvmJonasTest.ViewModels.OrderLog
{
    public class OrderLogViewModel : ViewModelBase
    {
        private IEnumerable<OrderLogItem> _orderModels;

        public OrderLogViewModel()
        {
            LogFilter = new OrderLogFilterViewModel();

            var userModels = Enumerable.Repeat(new ModelBase { Name = "Alle Benutzer", Id = Guid.Empty }, 1).Concat(ModelGenerator.GetUserModels());
            var productModels = Enumerable.Repeat(new ModelBase { Name = "Alle Produkte", Id = Guid.Empty }, 1).Concat(ModelGenerator.GetProducts());
            _orderModels = ModelGenerator.GetOrderLogItems();

            Users = new ObservableCollection<ModelBase>(userModels);
            Products = new ObservableCollection<ModelBase>(productModels);

            LogFilter.SelectedUser = Users.FirstOrDefault();
            LogFilter.SelectedProduct = Products.FirstOrDefault();
            LogFilter.MinDateFrom = _orderModels.Min(x => x.OrderDate);

            LogFilter.PropertyChanged += LogFilter_PropertyChanged;
        }

        private void LogFilter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(OrderLogItems));
        }

        public IEnumerable<OrderLogItemViewModel> OrderLogItems => LogFilter.FilterItems(_orderModels).Select(x => new OrderLogItemViewModel(x));
        public IReadOnlyList<ModelBase> Users { get; }
        public IReadOnlyList<ModelBase> Products { get; }

        public OrderLogFilterViewModel LogFilter { get; set; }

        private static IEnumerable<T> AddLeadingItem<T>(IEnumerable<T> source, T value)
        {
            yield return value;
            foreach (var item in source)
            {
                yield return item;
            }
        }
    }
}