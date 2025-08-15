using ShopAccAPI.Dtos.Game;
using ShopAccAPI.Models;

namespace ShopAccAPI.Mappers
{
    public static class GameMapper
    {
        public static GameDto ToGameDto(this Game game)
        {
            return new GameDto
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Genre = game.Genre,
                Publisher = game.Publisher,
                ReleaseDate = game.ReleaseDate,
                ImageUrl = game.ImageUrl,
                AccountGames = game.AccountGames != null ? game.AccountGames.Count() : 0
            };
        }

        public static Game ToCreateGame(this CreateGameDto createGameDto)
        {
            return new Game
            {
                Name = createGameDto.Name,
                Description = createGameDto.Description,
                Genre = createGameDto.Genre,
                Publisher = createGameDto.Publisher,
                ReleaseDate = createGameDto.ReleaseDate ?? DateTime.Now,
                ImageUrl = createGameDto.ImageUrl
            };
        }
    }
}
