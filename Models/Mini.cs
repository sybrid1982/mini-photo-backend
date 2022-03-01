using System.ComponentModel.DataAnnotations;

namespace MiniBackend.Models {
    public record Mini {
        [Key]
        public int mini_id { get; init; }
        public DateTime completion_date { get; init; }
        public string mini_name { get; init; }
        public string sculptor { get; init; }
        public int game_id { get; init; }
    }
}