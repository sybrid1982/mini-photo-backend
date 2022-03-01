using MiniBackend.DTOs;
using MiniBackend.Models;

namespace MiniBackend
{
    public static class Extension {
        public static MiniDTO AsDto(this Mini mini)
        {
            return new MiniDTO {
                Id = mini.mini_id,
                CompletionDate = mini.completion_date,
                MiniName = mini.mini_name,
                Sculptor = mini.sculptor,
                GameId = mini.game_id
            };
        }

        public static GameDTO AsDto(this Game game)
        {
            return new GameDTO
            {
                Id = game.game_id,
                YearPublished = game.year_published,
                GameName = game.game_name,
                BoxArtUrl = game.box_art,
                MetaId = game.meta_id
            };
        }
    }
}