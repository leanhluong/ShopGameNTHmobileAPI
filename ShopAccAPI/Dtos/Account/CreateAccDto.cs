namespace ShopAccAPI.Dtos.Account
{
    public class CreateAccDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Rank { get; set; }
        public string? Skin { get; set; }
        public string? Note { get; set; }
        public decimal Price { get; set; }
        public bool IsSold { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? ImageUrl { get; set; }

        public int GameId { get; set; } = 1;
    }
}
