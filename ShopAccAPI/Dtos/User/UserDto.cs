using ShopAccAPI.Helpers;
using ShopAccAPI.Models;
using System.Text.Json.Serialization;

namespace ShopAccAPI.Dtos.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; } = null!;
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime? DateOfBirth { get; set; }
        public decimal Balance { get; set; } = 0;
        public string Role { get; set; } = "User";
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? AvatarUrl { get; set; }
        public int CountOrder ;
    }
}
