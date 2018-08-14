using MvvmJonasTest.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MvvmJonasTest.UserControls
{
    /// <summary>
    /// Interaction logic for UserGraph.xaml
    /// </summary>
    public partial class UserGraph : UserControl
    {
        private const int GraphWidth = 450;
        private const int GraphHeight = 200;

        private const int GraphMarginLeft = 20;
        private const int GraphMarginBottom = 20;

        private static Brush[] _goodBrushes = {
            Brushes.Red,
            Brushes.Blue,
            Brushes.Brown,
            Brushes.BlueViolet,
            Brushes.CadetBlue,
            Brushes.DarkGoldenrod,
            Brushes.DeepPink,
            Brushes.LimeGreen,
            Brushes.Gold,
            Brushes.IndianRed,
            Brushes.Purple,
            Brushes.SlateBlue,
            Brushes.Teal,
            Brushes.Olive,
        };

        private int _currentBrush = 0;

        public UserGraph()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register("ItemSource",
            typeof(IEnumerable<User>), typeof(UserGraph), new PropertyMetadata(ItemSourceOnChangeCallback));

        public IEnumerable<User> ItemSource
        {
            get { return (IEnumerable<User>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        private static void ItemSourceOnChangeCallback(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            if (o is UserGraph userGraph)
            {
                userGraph.ItemSourceChanged();
            }
        }

        private void ItemSourceChanged()
        {
            List<int> years = ItemSource.SelectMany(x => x.Orders.Select(m => m.OrderDate.Year)).Distinct().ToList();
            double maxSpendMoneyByUser = ItemSource.Select(x => CalculateMaxExpenditureByYear(x)).Max();

            DrawAxis(years);

            foreach (var user in ItemSource.Take(20))
            {
                DrawUser(user, years, maxSpendMoneyByUser);
            }
        }

        private double CalculateMaxExpenditureByYear(User user)
        {
            var groupedByYear = user.Orders.GroupBy(x => x.OrderDate.Year);
            var yearSums = groupedByYear.Select(x => x.Sum(m => m.TotalPrice));

            return yearSums.Max();
        }

        private void DrawAxis(List<int> years)
        {
            var lineX = new Line
            {
                X1 = 0,
                Y1 = GraphHeight - GraphMarginBottom,
                X2 = GraphWidth,
                Y2 = GraphHeight - GraphMarginBottom,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            var lineY = new Line
            {
                X1 = GraphMarginLeft,
                Y1 = 0,
                X2 = GraphMarginLeft,
                Y2 = GraphHeight,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            UserCanvas.Children.Add(lineX);
            UserCanvas.Children.Add(lineY);
        }

        private void DrawUser(User user, List<int> years, double maxSpendMoneyByUser)
        {
            double unitY = (GraphHeight - GraphMarginBottom) / maxSpendMoneyByUser;
            int unitX = (GraphWidth - GraphMarginLeft) / Math.Max(years.Count - 1, 1);

            var pointZero = new Point(GraphMarginLeft, GraphHeight - GraphMarginBottom);

            var userLine = new Polyline
            {
                Stroke = GetNextBrush(),
                StrokeThickness = 1
            };

            for (int i = 0; i < years.Count; i++)
            {
                double currentX = unitX * i;
                double currentYearSum = user.Orders.Where(x => x.OrderDate.Year == years[i]).Sum(x => x.TotalPrice);
                Point tempPoint = pointZero + new Vector(currentX, -currentYearSum * unitY);

                userLine.Points.Add(tempPoint);

                var dataPoint = new Ellipse
                {
                    Fill = userLine.Stroke,
                    Width = 4,
                    Height = 4,
                };

                UserCanvas.Children.Add(dataPoint);

                Canvas.SetLeft(dataPoint, tempPoint.X - dataPoint.Width / 2);
                Canvas.SetTop(dataPoint, tempPoint.Y - dataPoint.Height / 2);
            }

            UserCanvas.Children.Add(userLine);
        }

        private Brush GetNextBrush()
        {
            if (_currentBrush > _goodBrushes.Length - 1)
            {
                _currentBrush = 0;
            }

            return _goodBrushes[_currentBrush++];
        }
    }
}