using System;
using System.Collections.Generic;
using System.Linq;
using MvvmJonasTest.Models;

namespace MvvmJonasTest.ViewModels.OrderLog
{
    public class OrderLogFilterViewModel : ViewModelBase
    {
        private const int SelectedUserIndex = 0;
        private const int SelectedProductIndex = 1;

        private ModelBase _selectedUser;
        private ModelBase _selectedProducts;
        private DateTime? _dateTimeFrom;
        private DateTime? _dateTimeTo;
        private double? _priceSelectionMin;
        private double? _priceSelectionMax;

        public OrderLogFilterViewModel()
        {
            //_dateTimeFrom = DateTime.Now.Date;
            //_dateTimeTo = DateTime.Now.Date;
        }

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

        public DateTime MaxDate => DateTime.Now;

        public DateTime MinDateFrom { get; set; }

        public DateTime MaxDateFrom => DateTimeTo ?? MaxDate;

        public DateTime MinDateTo => DateTimeFrom ?? MinDateFrom;

        public DateTime? DateTimeFrom
        {
            get => _dateTimeFrom;
            set
            {
                if (value.Equals(_dateTimeFrom))
                {
                    return;
                }

                if (value > _dateTimeTo)
                {
                    OnPropertyChanged();
                    return;
                }
                _dateTimeFrom = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MinDateTo));
            }
        }

        public DateTime? DateTimeTo
        {
            get => _dateTimeTo;
            set
            {
                if (value.Equals(_dateTimeTo))
                {
                    return;
                }

                if (value < _dateTimeFrom)
                {
                    OnPropertyChanged();
                    return;
                }

                _dateTimeTo = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MaxDateFrom));
            }
        }

        public double? PriceSelectionMin
        {
            get => _priceSelectionMin;
            set
            {
                if (value.Equals(_priceSelectionMin))
                {
                    return;
                }

                if (value > _priceSelectionMax)
                {
                    return;
                }

                _priceSelectionMin = value;
                OnPropertyChanged();
            }
        }

        public double? PriceSelectionMax
        {
            get => _priceSelectionMax;
            set
            {
                if (value.Equals(_priceSelectionMax))
                {
                    return;
                }

                if (value < _priceSelectionMin)
                {
                    return;
                }

                _priceSelectionMax = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<OrderLogItem> FilterItems(IEnumerable<OrderLogItem> items)
        {
            var logItems = items;

            if (_dateTimeFrom.HasValue)
            {
                logItems = logItems.Where(x => x.OrderDate >= _dateTimeFrom.Value);
            }

            if (_dateTimeTo.HasValue)
            {
                logItems = logItems.Where(x => x.OrderDate <= _dateTimeTo.Value);
            }

            if (SelectedUser != null && SelectedUser.Id != Guid.Empty)
            {
                logItems = logItems.Where(x => x.UserId == SelectedUser.Id);
            }

            if (SelectedProduct != null && SelectedProduct.Id != Guid.Empty)
            {
                logItems = logItems.Where(x => x.ProductId == SelectedProduct.Id);
            }

            if (_priceSelectionMin.HasValue)
            {
                logItems = logItems.Where(x => x.Price >= _priceSelectionMin.Value);
            }

            if (_priceSelectionMax.HasValue)
            {
                logItems = logItems.Where(x => x.Price <= _priceSelectionMax.Value);
            }
            return logItems;
        }
    }
}