namespace ShopAccAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public decimal Amount { get; set; } // số tiền thay đổi
        public string Type { get; set; } = null!; // Deposit, Withdraw, Purchase, Refund
        public string Status { get; set; } = "Success"; // Pending, Success, Failed
        public string? Note { get; set; } // ghi chú thêm
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // FK
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
