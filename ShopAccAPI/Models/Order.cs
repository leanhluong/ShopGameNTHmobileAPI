namespace ShopAccAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalPrice { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;
    }
}
