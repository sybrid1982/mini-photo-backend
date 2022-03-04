using MiniBackend.DTOs;
using MiniBackend.Models;

namespace MiniBackend
{
    public static class Extension {
        public static MiniDTO AsDto(this Mini mini)
        {
            try {
                return new MiniDTO {
                    Id = mini.MiniId,
                    CompletionDate = mini.CompletionDate,
                    MiniName = mini.MiniName,
                    Sculptor = mini.Sculptor,
                    GameId = mini.Game.GameId
                };
            } catch (Exception ex)
            {
                throw ex;
                return new MiniDTO {
                    Id = mini.MiniId,
                    CompletionDate = mini.CompletionDate,
                    MiniName = mini.MiniName,
                    Sculptor = mini.Sculptor,
                    GameId = -1
                };
            }
        }

        public static GameDTO AsDto(this Game game)
        {
            return new GameDTO
            {
                Id = game.GameId,
                YearPublished = game.YearPublished,
                GameName = game.GameName,
                BoxArtUrl = game.BoxArtUrl,
                Style = game.MiniMeta.Style,
                Scale = game.MiniMeta.Scale
            };
        }

        public static PhotoDTO AsDto(this Photo photo)
        {
            return new PhotoDTO
            {
                Id = photo.PhotoId,
                FileName = photo.Filename,
                MiniId = photo.Mini.MiniId
            };
        }
    }
}