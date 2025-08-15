using ShopAccAPI.Dtos.Game;
using ShopAccAPI.Models;

namespace ShopAccAPI.Interfaces
{
    public interface IGameRepository
    {
        Task<List<Game>>AllGamesAsync();
        Task<Game> GetGameByIdAsync(int id);
        Task<Game> CreateGameAsync(Game game);
        Task<Game> UpdateGameAsync(int id, CreateGameDto game);
        Task<Game> DeleteGameAsync(int id);
    }
}
