using System.Collections.Generic;
using MiniBackend.Repositories;
using MiniBackend.Models;
using MiniBackend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MiniBackend.Controllers
{
    [ApiController]
    [Route("photos")]
    public class PhotosController : ControllerBase
    {
        private readonly IMinisRepository repository;

        public PhotosController(IMinisRepository repository) {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public PhotoDTO GetPhoto(int id)
        {
            // TODO: go back and create this repository function
            return new PhotoDTO()
            {
                MiniId = 0,
                Id = 0,
                FileName = "nope"
            };
        }

        [HttpGet("mini/all/{id}")]
        public IEnumerable<PhotoDTO> GetPhotosForMini(int id)
        {
            var photos = repository.GetPhotosForMini(id).Select( photo => photo.AsDto());
            return photos;
        }

        // TODO: create function for getting the first photo for a mini

        // TODO: PUT
        // TODO: DELETE
    }
}