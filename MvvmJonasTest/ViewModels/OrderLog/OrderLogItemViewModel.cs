using System;
using MvvmJonasTest.Models;

namespace MvvmJonasTest.ViewModels.OrderLog
{
    public class OrderLogItemViewModel : ViewModelBase
    {
        private readonly OrderLogItem _model;

        public OrderLogItemViewModel(OrderLogItem model)
        {
            _model = model;
        }

        public string UserName => _model.UserName;

        public string ProductName => _model.ProductName;

        public double ProductPrice => _model.Price;

        public int Amount => _model.Amount;

        public DateTime OrderDateTime => _model.OrderDate;

        public string Comment => _model.Comment;
    }
}