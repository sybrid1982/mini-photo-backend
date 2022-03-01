using System.Collections.Generic;
using MiniBackend.Repositories;
using MiniBackend.Models;
using MiniBackend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MiniBackend.Controllers
{
    [ApiController]
    [Route("minis")]
    public class MinisController : ControllerBase
    {
        private readonly IMinisRepository repository;

        public MinisController(IMinisRepository repository) {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<MiniDTO> GetMinis()
        {
            var minis = repository.GetMinis().Select( mini => mini.AsDto());
            return minis;
        }

        // GET minis/{id}
        [HttpGet("{id}")]
        public ActionResult<MiniDTO> GetMini(int id)
        {
            var mini = repository.GetMini(id);

            if (mini is null) {
                return NotFound();
            }

            return mini.AsDto();
        }

        // GET /minis/game/{id}
        // Gets all the minis associated with a game
        [HttpGet("game/{id}")]
        public IEnumerable<MiniDTO> GetMinisByGame(int id)
        {
            var minis = repository.GetMinisByGame(id).Select(mini => mini.AsDto());
            return minis;
        }


        // POST /minis
        [HttpPost]
        public ActionResult<MiniDTO> CreateMini(CreateMiniDto miniDto)
        {
            DateTime completionDate = miniDto.CompletionDate != null ? miniDto.CompletionDate : DateTime.UtcNow;
            Mini mini = new()
            {
                mini_name = miniDto.MiniName,
                sculptor = miniDto.Sculptor,
                game_id = miniDto.GameId,
                completion_date = completionDate
            };

            repository.CreateMini(mini);

            return CreatedAtAction(nameof(GetMini), new { id = mini.mini_id}, mini.AsDto());
        }

        // PUT /minis/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateMini(int id, UpdateMiniDto miniDto)
        {
            var existingMini = repository.GetMini(id);
            if(existingMini is null) {
                return NotFound();
            }

            Mini updatedMini = existingMini with
            {
                mini_name = miniDto.MiniName,
                sculptor = miniDto.Sculptor,
                game_id = miniDto.GameId,
                completion_date = miniDto.CompletionDate
            };

            repository.UpdateMini(updatedMini);

            return NoContent();
        }

        // DELETE /minis/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteMini(int id)
        {
            var existingMini = repository.GetMini(id);
            if(existingMini is null) {
                return NotFound();
            }

            repository.DeleteMini(id);

            return NoContent();
        }
    }
}