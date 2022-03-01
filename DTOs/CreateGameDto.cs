using System.ComponentModel.DataAnnotations;

namespace MiniBackend.DTOs
{
    public record CreateGameDTO {
        public string YearPublished { get; init; }
        [Required]
        public string GameName { get; init; }
        public string BoxArtUrl { get; init; }
        public int MetaId { get; init; }
    }
}