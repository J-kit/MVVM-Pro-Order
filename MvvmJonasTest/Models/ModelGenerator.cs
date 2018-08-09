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
        private static Random _rnd = new Random();
        private static ResourceManager _resources = new ResourceManager("MvvmJonasTest.Properties.Resources", typeof(Resources).Assembly);

        private static Lazy<string[]> ProductList = new Lazy<string[]>(() => _resources.GetString("Produkte")?.Split(new[] { Environment.NewLine }, StringSplitOptions.None));

        private static IEnumerable<UserModel> _userModelCache;

        private static IEnumerable<UserModel> UserModelCache => _userModelCache ?? (_userModelCache = GenerateUserModels());

        private static IEnumerable<OrderLogItem> _orderLogItemsCache;

        private static IEnumerable<OrderLogItem> OrderLogItemsCache => _orderLogItemsCache ?? (_orderLogItemsCache = GenerateOrderLogItems());

        //public static IEnumerable<ModelBase> GetDistinctUsers()
        //{
        //}

        //public static IEnumerable<ModelBase> GetDistinctProducts()
        //{
        //}

        public static IEnumerable<OrderLogItem> GetOrderLogItems()
        {
            return OrderLogItemsCache;
        }

        public static IEnumerable<UserModel> GetUserModels()
        {
            return UserModelCache;
        }

        private static IEnumerable<OrderLogItem> GenerateOrderLogItems()
        {
            var result = new List<OrderLogItem>();

            foreach (UserModel userModel in UserModelCache)
            {
                foreach (OrderModel orderModel in userModel.Orders)
                {
                    var item = new OrderLogItem
                    {
                        UserId = userModel.Id,
                        UserName = userModel.Name,
                        Id = orderModel.Id,
                        Price = orderModel.Price,
                        Name = orderModel.Name,
                        Anmerkungen = orderModel.Anmerkungen,
                        OrderDate = orderModel.OrderDate,
                    };

                    result.Add(item);
                }
            }

            return result;
        }

        private static IEnumerable<UserModel> GenerateUserModels()
        {
            var entenHausen = _resources.GetString("Entenhausen")
                ?.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            if (entenHausen == null)
            {
                return default;
            }

            List<UserModel> result = entenHausen
                .Select(x => new UserModel
                {
                    Id = Guid.NewGuid(),
                    Name = x,
                    PersonalText = RandomHelper.RandomString(50),
                    Orders = RandomProducts(_rnd.Next(2, 10)).Select(m => new OrderModel
                    {
                        Id = Guid.NewGuid(),
                        Name = m,
                        Price = _rnd.Next(5000),
                        Anmerkungen = RandomHelper.RandomString(30),
                        OrderDate = GetRandomDatetime()
                    }).ToList(),
                })
                .ToList();

            return result;
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

        private static IEnumerable<string> RandomProducts(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                yield return ProductList.Value[_rnd.Next(ProductList.Value.Length)];
            }
        }
    }
}