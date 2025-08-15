namespace ShopAccAPI.Models.Authentication
{
    public class LoginResponseModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public decimal Balance { get; set; } = 0;
        public string Role { get; set; } = "User";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? AvatarUrl { get; set; }
    }
}
