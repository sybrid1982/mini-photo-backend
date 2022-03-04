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
            Game game = repository.GetGame(miniDto.GameId);
            Mini mini = new()
            {
                MiniName = miniDto.MiniName,
                Sculptor = miniDto.Sculptor,
                Game = game,
                CompletionDate = completionDate
            };

            repository.CreateMini(mini);

            return CreatedAtAction(nameof(GetMini), new { id = mini.MiniId}, mini.AsDto());
        }

        // PUT /minis/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateMini(int id, UpdateMiniDto miniDto)
        {
            var existingMini = repository.GetMini(id);
            if(existingMini is null) {
                return NotFound();
            }

            Game game = repository.GetGame(miniDto.GameId);

            Mini updatedMini = existingMini with
            {
                MiniName = miniDto.MiniName,
                Sculptor = miniDto.Sculptor,
                Game = game,
                CompletionDate = miniDto.CompletionDate
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

        // POST /minis/photo/id
        [HttpPost("photo/{id}")]
        public async Task<IActionResult> OnPostUploadAsync(int id, List<IFormFile> files)
        {
            if(files.Count == 0)
            {
                return BadRequest();
            }
            
            foreach(var file in files) {
                string extension = Path.GetExtension(file.FileName);
                long size = file.Length;

                if(file.Length > 0) {
                    var filePath = Path.Combine("/Users/sybri/vue-projects/mini-photos-app/MiniBackend/Images/MiniPictures",
                        Path.GetRandomFileName());

                    filePath += extension;

                    Mini mini = repository.GetMini(id);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }

                    string filenameForDb = filePath.Split("Images")[1];

                    Photo photo = new()
                    {
                        Mini = mini,
                        Filename = filenameForDb
                    };

                    repository.CreatePhoto(photo);
                    // return Ok( new { fileName = filePath});
                }
            }
            return Ok(new { count = files.Count });
        }
    }
}