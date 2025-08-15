namespace ShopAccAPI.Dtos.User
{
    public class CreateUserDto
    {
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public decimal Balance { get; set; } = 0;
        public string Role { get; set; } = "User";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? AvatarUrl { get; set; }
    }
}
