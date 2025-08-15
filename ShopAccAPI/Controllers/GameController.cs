using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAccAPI.Data;
using ShopAccAPI.Dtos.Game;
using ShopAccAPI.Interfaces;
using ShopAccAPI.Mappers;
using ShopAccAPI.Models;

namespace ShopAccAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepo;
        public GameController(IGameRepository gameRepository)
        {
            _gameRepo = gameRepository;
        }

        [HttpGet("GetAllGames")]
        public async Task<IActionResult> GetAllGames()
        {
            var games = await _gameRepo.AllGamesAsync();
            var gameDto = games.Select(g => g.ToGameDto());
            if (games == null || !games.Any())
            {
                return NotFound("No games found.");
            }
            return Ok(gameDto);
        }
        [HttpGet("GetGameById/{id}")]
        public async Task<IActionResult> GetGameById([FromRoute] int id)
        {
            var game = await _gameRepo.GetGameByIdAsync(id);
            if (game == null)
            {
                return NotFound($"Game with ID {id} not found.");
            }
            return Ok(game.ToGameDto());
        }

        [HttpPost("CreateGame")]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameDto createGameDto)
        {
            var game = createGameDto.ToCreateGame();
            var createdGame = await _gameRepo.CreateGameAsync(game);
            return CreatedAtAction(nameof(GetGameById), new { id = createdGame.Id }, createdGame.ToGameDto());

        }

        [HttpPut("UpdateGame/{id}")]
        public async Task<IActionResult> UpdateGame([FromRoute] int id, [FromBody] CreateGameDto updateGameDto)
        {
            var game = await _gameRepo.UpdateGameAsync(id, updateGameDto);
            if (game == null)
            {
                return NotFound($"Game with ID {id} not found.");
            }
            return Ok(game.ToGameDto());
        }

        [HttpDelete("DeleteGame/{id}")]
        public async Task<IActionResult> DeleteGame([FromRoute] int id)
        {
            var game = await _gameRepo.DeleteGameAsync(id);
            if (game == null)
            {
                return NotFound($"Game with ID {id} not found.");
            }
            return NoContent();
        }
    }
}
