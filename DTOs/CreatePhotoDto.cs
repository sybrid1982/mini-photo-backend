using System.ComponentModel.DataAnnotations;

namespace MiniBackend.DTOs
{
    public record CreatePhotoDTO {
        [Required]
        public int MiniId { get; init; }
        [Required]
        public string FileName { get; init; }
    }
}