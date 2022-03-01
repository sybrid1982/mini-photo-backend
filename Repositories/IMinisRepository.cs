using System.Collections.Generic;
using MiniBackend.Models;

namespace MiniBackend.Repositories
{
    public interface IMinisRepository
    {
        // Minis
        Mini GetMini(int id);
        IEnumerable<Mini> GetMinis();
        IEnumerable<Mini> GetMinisByGame(int id);
        void CreateMini(Mini mini);
        void UpdateMini(Mini mini);
        void DeleteMini(int id);
        // Games
        IEnumerable<Game> GetGames();
        Game GetGame(int id);
        void CreateGame(Game game);
        void UpdateGame(Game game);
        void DeleteGame(int id);
    }
}