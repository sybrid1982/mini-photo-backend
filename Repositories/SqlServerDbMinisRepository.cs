using Microsoft.EntityFrameworkCore;
using MiniBackend.Models;

namespace MiniBackend.Repositories
{
    public class SqlServerDbMinisRepository : IMinisRepository 
    {
        private readonly MiniContext context;

        public SqlServerDbMinisRepository(MiniContext miniContext) {
            this.context = miniContext;
        }

        // Minis
        public Mini GetMini(int id) {
            return context.Minis.Where(mini => mini.mini_id == id).SingleOrDefault();
        }
        public IEnumerable<Mini> GetMinis() {
            return context.Minis;
        }

        public IEnumerable<Mini> GetMinisByGame(int gameId)
        {
            var minis = context.Minis.Where(mini => mini.game_id == gameId);
            return minis;
        }

        public void CreateMini(Mini mini) {
            context.Minis.Add(mini);
            context.SaveChanges();
        }
        public void UpdateMini(Mini mini) {
            context.Minis.Update(mini);
            context.SaveChanges();
        }
        public void DeleteMini(int id) {
            var mini = GetMini(id);
            context.Minis.Remove(mini);
            context.SaveChanges();
        }

        // Games
        public IEnumerable<Game> GetGames() {
            return context.Games;
        }

        public Game GetGame(int id)
        {
            return context.Games.Where(game => game.game_id == id).SingleOrDefault();
        }

        public void CreateGame(Game game)
        {
            context.Games.Add(game);
            context.SaveChanges();
        }

        public void UpdateGame(Game game)
        {
            context.Games.Update(game);
            context.SaveChanges();
        }

        public void DeleteGame(int id)
        {
            var game = GetGame(id);
            context.Games.Remove(game);
            context.SaveChanges();
        } 
    }
}