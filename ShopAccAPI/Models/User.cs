namespace ShopAccAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public decimal Balance { get; set; } = 0;
        public string Role { get; set; } = "User";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? AvatarUrl { get; set; } 

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
