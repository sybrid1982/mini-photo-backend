using System.Collections.Generic;
using MiniBackend.Repositories;
using MiniBackend.Models;
using MiniBackend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MiniBackend.Controllers
{
    [ApiController]
    [Route("games")]
    public class GamesController : ControllerBase
    {
        private readonly IMinisRepository repository;

        public GamesController(IMinisRepository repository) 
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<GameDTO> GetGames()
        {
            var games = repository.GetGames().Select( game => game.AsDto());
            return games;
        }

        // GET games/{id}
        [HttpGet("{id}")]
        public ActionResult<GameDTO> GetGame(int id)
        {
            var game = repository.GetGame(id);

            if( game is null) 
            {
                return NotFound();
            }

            return game.AsDto();
        }

        // POST /games
        [HttpPost]
        public ActionResult<GameDTO> CreateGame(CreateGameDTO gameDto)
        {
            Game game = new()
            {
                game_name = gameDto.GameName,
                year_published = gameDto.YearPublished,
                box_art = gameDto.BoxArtUrl,
                meta_id = gameDto.MetaId
            };

            repository.CreateGame(game);

            return CreatedAtAction(nameof(GetGame), new { id = game.game_id}, game.AsDto());
        }

        // PUT /games/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateGame(int id, UpdateGameDTO gameDto)
        {
            var existingGame = repository.GetGame(id);
            if(existingGame is null)
            {
                return NotFound();
            }

            Game updatedGame = existingGame with
            {
                game_name = gameDto.GameName,
                year_published = gameDto.YearPublished,
                box_art = gameDto.BoxArtUrl,
                meta_id = gameDto.MetaId
            };

            repository.UpdateGame(updatedGame);

            return NoContent();
        }

        // DELETE /games/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteGame(int id)
        {
            var existingGame = repository.GetGame(id);
            if(existingGame is null) {
                return NotFound();
            }

            repository.DeleteGame(id);

            return NoContent();
        }
    }
}