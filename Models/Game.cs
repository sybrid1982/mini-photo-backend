using System.ComponentModel.DataAnnotations;

namespace MiniBackend.Models {
    public record Game {
        [Key]
        public int game_id { get; init; }
        public string year_published { get; init; }
        public string game_name { get; init; }
        public string box_art { get; init; }     // location of the image
        public int meta_id { get; init; }
    }
}