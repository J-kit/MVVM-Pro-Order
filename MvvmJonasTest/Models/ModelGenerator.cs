using MvvmJonasTest.Properties;
using MvvmJonasTest.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;

namespace MvvmJonasTest.Models
{
    internal class ModelGenerator
    {
        private static readonly Random _rnd = new Random();
        private static readonly ResourceManager _resources = new ResourceManager("MvvmJonasTest.Properties.Resources", typeof(Resources).Assembly);
        private static Product[] _productsCache;
        private static IEnumerable<User> _userModelCache;
        private static IEnumerable<OrderLogItem> _orderLogItemsCache;
        private static Product[] _products;

        public static IEnumerable<Product> GetProducts()
        {
            return Products;
        }

        public static IEnumerable<OrderLogItem> GetOrderLogItems()
        {
            return GenerateOrderLogItems();
        }

        public static IEnumerable<User> GetUserModels()
        {
            return GenerateUserModels();
        }

        private static IEnumerable<User> GenerateUserModels()
        {
            if (_userModelCache == null)
            {
                var entenHausen = _resources.GetString("Entenhausen")
                    ?.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                    .Select(x => new User
                    {
                        Id = Guid.NewGuid(),
                        Name = x,
                        PersonalText = RandomHelper.RandomString(50),
                        Orders = Enumerable.Range(1, 5).Select(m => GenerateOrderModel(_rnd.Next(m))).ToList(),
                    })
                    .ToList();

                _userModelCache = entenHausen;
            }
            return _userModelCache;
        }

        private static IEnumerable<OrderLogItem> GenerateOrderLogItems()
        {
            if (_orderLogItemsCache == null)
            {
                var result = new List<OrderLogItem>();

                foreach (User userModel in GetUserModels())
                {
                    foreach (Order orderModel in userModel.Orders)
                    {
                        foreach (OrderItem orderItem in orderModel.OrderItems)
                        {
                            var item = new OrderLogItem
                            {
                                UserId = userModel.Id,
                                ProductId = orderItem.Product.Id,
                                UserName = userModel.Name,
                                ProductName = orderItem.Product.Name,
                                OrderDate = orderModel.OrderDate,
                                Comment = orderModel.Comment,
                                Price = orderItem.Product.Price,
                                Amount = orderItem.Amount
                            };

                            result.Add(item);
                        }
                    }
                }

                _orderLogItemsCache = result;
            }

            return _orderLogItemsCache;
        }

        private static Order GenerateOrderModel(int amountProducts)
        {
            var model = new Order
            {
                OrderDate = GetRandomDatetime(),
                Comment = RandomHelper.RandomString(10),
                OrderItems = GenerateOrderItems(amountProducts).ToList(),
            };

            return model;
        }

        private static DateTime GetRandomDatetime()
        {
            return new DateTime(
                _rnd.Next(2014, 2018),
                _rnd.Next(1, 13),
                _rnd.Next(1, 29),
                _rnd.Next(0, 24),
                _rnd.Next(0, 60),
                _rnd.Next(0, 60));
        }

        private static IEnumerable<OrderItem> GenerateOrderItems(int amount)
        {
            return Enumerable.Range(0, amount)
                .Select(x => new OrderItem
                {
                    Product = Products[_rnd.Next(Products.Length)],
                    Amount = _rnd.Next(1, 500),
                });
        }

        private static Product[] Products
        {
            get
            {
                if (_products == null)
                {
                    _products = _resources.GetString("Produkte")
                        ?.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                        .Select(x => new Product
                        {
                            Name = x,
                            Price = _rnd.NextDouble() * 100,
                            ProductDescription = RandomHelper.RandomString(30),
                        })
                        .ToArray();
                }

                return _products;
            }
        }
    }
}