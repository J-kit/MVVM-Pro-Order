namespace MvvmJonasTest.Models
{
    public class OrderItem : IdBase
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
        public double TotalPrice => Product?.Price * Amount ?? 0;
    }
}