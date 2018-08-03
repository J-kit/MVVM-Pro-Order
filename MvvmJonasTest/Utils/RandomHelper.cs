using System;
using System.Linq;

namespace MvvmJonasTest.Utils
{
    class RandomHelper
    {
        private static readonly Random Random = new Random();

        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                             "abcdefghijklmnopqrstuvwxyz" +
                             "0123456789";

        public static string RandomString(int length)
        {

            return new string(Enumerable.Repeat(Chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
