namespace ShopAccAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Genre { get; set; } 
        public string? Publisher { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? ImageUrl { get; set; } 
        public List<Account> AccountGames { get; set; } = new List<Account>();
    }
}
