using Microsoft.EntityFrameworkCore;
using ShopAccAPI.Data;
using ShopAccAPI.Dtos.Game;
using ShopAccAPI.Interfaces;
using ShopAccAPI.Models;

namespace ShopAccAPI.Respository
{
    public class GameRepository : IGameRepository
    {
        private readonly DBContext _context;
        public GameRepository(DBContext context)
        {
            _context = context;
        }
        public async Task<List<Game>> AllGamesAsync()
        {
            return await _context.Games
                .Include(g => g.AccountGames)
                .ToListAsync();
        }

        public async Task<Game> CreateGameAsync(Game game)
        {
            var existingGame = await _context.Games.FirstOrDefaultAsync(g => g.Name == game.Name);
            if (existingGame != null)
            {
                throw new InvalidOperationException($"Game with name '{game.Name}' already exists.");
            }
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<Game> DeleteGameAsync(int id)
        {
            var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
            if (game == null)
            {
                throw new KeyNotFoundException($"Game with ID {id} not found.");
            }
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<Game> GetGameByIdAsync(int id)
        {
            var game = await _context.Games
                .Include(g => g.AccountGames)
                .FirstOrDefaultAsync(g => g.Id == id);
            if (game == null)
            {
                throw new KeyNotFoundException($"Game with ID {id} not found.");
            }
            return game;
        }

        public async Task<Game> UpdateGameAsync(int id, CreateGameDto game)
        {
            var existingGame = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
            if (existingGame == null)
            {
                throw new KeyNotFoundException($"Game with ID {id} not found.");
            }
            existingGame.Name = game.Name;
            existingGame.Description = game.Description;
            existingGame.Genre = game.Genre;
            existingGame.Publisher = game.Publisher;
            existingGame.ReleaseDate = game.ReleaseDate;
            existingGame.ImageUrl = game.ImageUrl;

            await _context.SaveChangesAsync();
            return existingGame;
        }
    }
}
