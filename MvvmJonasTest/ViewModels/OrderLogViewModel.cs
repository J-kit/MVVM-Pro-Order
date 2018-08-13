using MvvmJonasTest.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MvvmJonasTest.ViewModels
{
    public class OrderLogViewModel : ViewModelBase
    {
        public OrderLogViewModel()
        {
            //OrderLogItems = new ObservableCollection<OrderLogItem>(ModelGenerator.GetOrderLogItems());
        }

        public ObservableCollection<OrderLogItem> OrderLogItems { get; private set; }
        public IReadOnlyList<ModelBase> Users { get; private set; }
        public IReadOnlyList<ModelBase> Products { get; private set; }

        public ModelBase SelectedUser { get; set; }
        public ModelBase SelectedProducts { get; set; }

        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }

        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }
    }
}