using System.ComponentModel.DataAnnotations;

namespace MiniBackend.Models {
    public record MiniMeta {
        [Key]
        public int meta_id { get; init; }
        public string style { get; init; }
        public int scale { get; init; }
    }
}