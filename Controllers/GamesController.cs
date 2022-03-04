using System.Collections.Generic;
using MiniBackend.Repositories;
using MiniBackend.Models;
using MiniBackend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

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
            int metaId = repository.FindMetaIdByValues(gameDto.Style, gameDto.Scale);

            MiniMeta meta;

            if(metaId >= 0) {
                meta = repository.GetMeta(metaId);
            } else {
                meta = new() {Style = gameDto.Style, Scale = gameDto.Scale };
                repository.CreateMeta(meta);
            }
            
            Game game = new()
            {
                GameName = gameDto.GameName,
                YearPublished = gameDto.YearPublished,
                BoxArtUrl = gameDto.BoxArtUrl,
                MiniMeta = meta
            };

            repository.CreateGame(game);

            return CreatedAtAction(nameof(GetGame), new { id = game.GameId}, game.AsDto());
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
            MiniMeta Meta = repository.GetMeta(gameDto.MetaId);

            Game updatedGame = existingGame with
            {
                GameName = gameDto.GameName,
                YearPublished = gameDto.YearPublished,
                BoxArtUrl = gameDto.BoxArtUrl,
                MiniMeta = Meta
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

        // POST /games/photo
        [HttpPost("photo")]
        public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
        {
            // if(file.Count == 0)
            // {
            //     return BadRequest();
            // }
            IFormFile theFile = file;
            string extension = Path.GetExtension(file.FileName);
            long size = theFile.Length;

            if(theFile.Length > 0) {
                var filePath = Path.Combine("/Users/sybri/vue-projects/mini-photos-app/MiniBackend/Images/BoxArt",
                    Path.GetRandomFileName());

                filePath += extension;

                using (var stream = System.IO.File.Create(filePath))
                {
                    await theFile.CopyToAsync(stream);
                }
                return Ok( new { fileName = filePath});
            }
            return BadRequest();
        }
    }
}