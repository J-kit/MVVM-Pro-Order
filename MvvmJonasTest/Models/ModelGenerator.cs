using MvvmJonasTest.Properties;
using MvvmJonasTest.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;

namespace MvvmJonasTest.Models
{
    class ModelGenerator
    {

        private static Random _rnd = new Random();
        private static ResourceManager _resources = new ResourceManager("MvvmJonasTest.Properties.Resources", typeof(Resources).Assembly);

        private static Lazy<string[]> ProductList = new Lazy<string[]>(() => _resources.GetString("Produkte")?.Split(new[] { Environment.NewLine }, StringSplitOptions.None));

        public static ICollection<UserModel> GenerateUserModels()
        {
            var entenHausen = _resources.GetString("Entenhausen")
                ?.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            if (entenHausen == null)
            {
                return default;
            }

            return entenHausen
                .Select(x => new UserModel
                {
                    Id = Guid.NewGuid(),
                    Name = x,
                    PersonalText = RandomHelper.RandomString(50),
                    Orders = RandomProducts(_rnd.Next(2, 10)).Select(m => new OrderModel
                    {
                        Id = Guid.NewGuid(),
                        ProductName = m,
                        Price = _rnd.Next(5000),
                        Anmerkungen = RandomHelper.RandomString(30)
                    }).ToList(),
                })
                .ToList();

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
