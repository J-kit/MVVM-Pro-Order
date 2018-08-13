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
        private const string Produkte = "Produkte";
        private static Random _rnd = new Random();
        private static ResourceManager _resources = new ResourceManager("MvvmJonasTest.Properties.Resources", typeof(Resources).Assembly);
        private static Product[] _products;

        private static IEnumerable<User> _userModelCache;

        private static IEnumerable<OrderLogItem> _orderLogItemsCache;

        // private static IEnumerable<OrderLogItem> OrderLogItemsCache => _orderLogItemsCache ?? (_orderLogItemsCache = GenerateOrderLogItems());

        //public static IEnumerable<ModelBase> GetDistinctUsers()
        //{
        //}

        //public static IEnumerable<ModelBase> GetDistinctProducts()
        //{
        //}

        //public static IEnumerable<OrderLogItem> GetOrderLogItems()
        //{
        //    return OrderLogItemsCache;
        //}

        private static IEnumerable<OrderLogItem> GenerateOrderLogItems()
        {
            var result = new List<OrderLogItem>();

            foreach (User userModel in GetUserModels())
            {
                foreach (Order orderModel in userModel.Orders)
                {
                    var item = new OrderLogItem
                    {
                        UserId = userModel.Id,
                        UserName = userModel.Name,
                        Id = orderModel.Id,
                        Comment = orderModel.Comment,
                        OrderDate = orderModel.OrderDate,
                        OrderItems = orderModel.OrderItems
                    };

                    result.Add(item);
                }
            }

            return result;
        }

        public static IEnumerable<User> GetUserModels()
        {
            if (_userModelCache == null)
            {
                var entenHausen = _resources.GetString("Entenhausen")
                    ?.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                if (entenHausen == null)
                {
                    return default;
                }

                _userModelCache = entenHausen
                    .Select(x => new User
                    {
                        Id = Guid.NewGuid(),
                        Name = x,
                        PersonalText = RandomHelper.RandomString(50),
                        Orders = Enumerable.Range(10, 100).Select(m => RandomOrders(_rnd.Next(m))).ToList(),
                    })
                    .ToList();
            }
            return _userModelCache;
        }

        private static Order RandomOrders(int products)
        {
            var model = new Order
            {
                OrderDate = GetRandomDatetime(),
                Comment = RandomHelper.RandomString(10),
            };

            model.OrderItems = RandomProducts(products)
                .Select(x => new OrderItem
                {
                    Product = x,
                    Amount = _rnd.Next(1, 500),
                }).ToList();

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

        private static IEnumerable<Product> RandomProducts(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                yield return Products[_rnd.Next(Products.Length)];
            }
        }

        private static Product[] Products
        {
            get
            {
                if (_products == null)
                {
                    _products = _resources.GetString(Produkte)?
                        .Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(x => new Product
                        {
                            Name = x,
                            Price = _rnd.Next(5000),
                            ProductDescription = RandomHelper.RandomString(30),
                        })
                        .ToArray();
                }

                return _products;
            }
        }
    }
}