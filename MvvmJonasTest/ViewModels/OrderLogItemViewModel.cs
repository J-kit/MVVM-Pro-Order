using MvvmJonasTest.Models;

using System;

namespace MvvmJonasTest.ViewModels
{
    public class OrderLogItemViewModel : ViewModelBase
    {
        private readonly OrderLogItem _model;
        private readonly OrderLogFilterViewModel _filter;
        private bool _productSelectionVisible = true;
        private bool _userSelectionVisible = true;

        private bool UserSelectionVisible
        {
            get => _userSelectionVisible;
            set
            {
                if (Equals(value, _userSelectionVisible))
                {
                    return;
                }
                _userSelectionVisible = value;
                base.OnPropertyChanged(nameof(IsVisible));
            }
        }

        private bool ProductSelectionVisible
        {
            get => _productSelectionVisible;
            set
            {
                if (Equals(value, _productSelectionVisible))
                {
                    return;
                }
                _productSelectionVisible = value;
                base.OnPropertyChanged(nameof(IsVisible));
            }
        }

        public OrderLogItemViewModel(OrderLogItem model, OrderLogFilterViewModel filter)
        {
            _model = model;
            _filter = filter;
            filter.PropertyChanged += Filter_PropertyChanged;
        }

        private void Filter_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var visible = true;
            switch (e.PropertyName)
            {
                case nameof(OrderLogFilterViewModel.SelectedUser):
                    if (_filter.SelectedUser == null)
                    {
                        UserSelectionVisible = true;
                    }
                    else
                    {
                        UserSelectionVisible = _filter.SelectedUser.Id == _model.UserId;
                    }
                    break;

                case nameof(OrderLogFilterViewModel.SelectedProduct):
                    if (_filter.SelectedProduct == null)
                    {
                        ProductSelectionVisible = true;
                    }
                    else
                    {
                        ProductSelectionVisible = _filter.SelectedProduct.Id == _model.ProductId;
                    }
                    break;

                default:
                    break;
            }
        }

        public bool IsVisible => ProductSelectionVisible && UserSelectionVisible;

        public string UserName => _model.UserName;

        public string ProductName => _model.ProductName;

        public double ProductPrice => _model.Price;

        public int Amount => _model.Amount;

        public DateTime OrderDateTime => _model.OrderDate;

        public string Comment => _model.Comment;
    }
}