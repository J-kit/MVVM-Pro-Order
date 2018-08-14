using MvvmJonasTest.Models;

using System;
using System.Collections.Generic;
using System.Linq;

namespace MvvmJonasTest.UserControls
{
    /// <summary>
    /// Generates an abstract model of the expenditures of the given <see longcref="User"/>
    /// </summary>
    public class AbstractExpenditureStatistics
    {
        private readonly User _user;

        public AbstractExpenditureStatistics(User user)
        {
            _user = user;
        }

        public IEnumerable<int> YearsActive => _user.Orders.Select(x => x.OrderDate.Year).Distinct();

        //public Dictionary<int,double>
    }

    public class AbstractExpenditureStatisticsYear
    {
        private readonly User _user;
        private readonly DateTime _year;

        public AbstractExpenditureStatisticsYear(User user, DateTime year)
        {
            _user = user;
            _year = year;
        }

        public IEnumerable<int> MonthsActive => _user.Orders.Where(x => x.OrderDate.Year == _year.Year).Select(x => x.OrderDate.Month).Distinct();

        public double GetTotalExpenditures()
        {
            return _user.Orders
                        .Where(x => x.OrderDate.Year == _year.Year)
                        .Sum(x => x.TotalPrice);
        }

        public Dictionary<int, AbstractExpenditureStatisticsMonth> GetMonthlyExpenditures()
        {
            var expenditure = new Dictionary<int, AbstractExpenditureStatisticsMonth>(12);
            for (int i = 1; i <= 12; i++)
            {
                expenditure[i] = new AbstractExpenditureStatisticsMonth(_user, new DateTime(_year.Year, i, 0));
            }

            return expenditure;
        }
    }

    public class AbstractExpenditureStatisticsMonth
    {
        private readonly User _user;
        private readonly DateTime _month;

        /// <param name="month">Contains Year and month</param>
        public AbstractExpenditureStatisticsMonth(User user, DateTime month)
        {
            _user = user;
            _month = month;
        }

        public IEnumerable<int> DaysActive => _user.Orders
            .Where(x => x.OrderDate.Year == _month.Year)
            .Where(x => x.OrderDate.Month == _month.Month)
            .Select(x => x.OrderDate.Month)
            .Distinct();

        public double GetTotalExpenditures()
        {
            return _user.Orders
                .Where(x => x.OrderDate.Year == _month.Year)
                .Where(x => x.OrderDate.Month == _month.Month)
                .Sum(x => x.TotalPrice);
        }
    }
}