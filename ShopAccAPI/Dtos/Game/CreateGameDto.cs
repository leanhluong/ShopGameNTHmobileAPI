namespace ShopAccAPI.Dtos.Game
{
    public class CreateGameDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Genre { get; set; }
        public string? Publisher { get; set; }
        public DateTime? ReleaseDate { get; set; } = DateTime.Now;
        public string? ImageUrl { get; set; }
    }
}
