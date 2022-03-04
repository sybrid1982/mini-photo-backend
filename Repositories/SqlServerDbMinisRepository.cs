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
            return context.Minis.Where(mini => mini.MiniId == id).Include(m => m.Game).SingleOrDefault();
        }
        public IEnumerable<Mini> GetMinis() {
            return context.Minis.Include(m => m.Game);
        }

        public IEnumerable<Mini> GetMinisByGame(int gameId)
        {
            var minis = context.Minis.Where(mini => mini.Game.GameId == gameId).Include(m => m.Game);
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
            return context.Games.Include(g => g.MiniMeta);
        }

        public Game GetGame(int id)
        {
            return context.Games.Where(game => game.GameId == id).Include(g => g.MiniMeta).SingleOrDefault();
        }

        public void CreateGame(Game game)
        {
            context.Games.Add(game);
            context.SaveChanges();
        }

        public void UpdateGame(Game game)
        {
            var result = context.Games.SingleOrDefault(g => g.GameId == game.GameId);
            if (result != null)
            {
                context.Entry(result).CurrentValues.SetValues(game);
                context.SaveChanges();
            }
        }

        public void DeleteGame(int id)
        {
            var game = GetGame(id);
            context.Games.Remove(game);
            context.SaveChanges();
        }

        // Photos
        public IEnumerable<Photo> GetPhotosForMini(int id)
        {
            return context.Photos.Where(photo => photo.Mini.MiniId == id);
        }

        public void CreatePhoto(Photo photo)
        {
            context.Photos.Add(photo);
            context.SaveChanges();
        }

        public void UpdatePhoto(Photo photo)
        {
            var result = context.Photos.SingleOrDefault(p => p.PhotoId == photo.PhotoId);
            if (result != null)
            {
                context.Entry(result).CurrentValues.SetValues(photo);
                context.SaveChanges();
            }
        }

        public void DeletePhoto(int id)
        {
            
            var game = GetGame(id);
            context.Games.Remove(game);
            context.SaveChanges();
        }

        public MiniMeta GetMeta(int id)
        {
            return context.MiniMeta.SingleOrDefault(meta => meta.MetaId == id);
        }

        public int FindMetaIdByValues(string style, string scale)
        {
            MiniMeta meta = context.MiniMeta.Where(meta => meta.Scale == scale && meta.Style == style).SingleOrDefault();
            if(meta is null) {
                return -1;
            }
            return meta.MetaId;
        }

        public void CreateMeta(MiniMeta meta) {
            context.MiniMeta.Add(meta);
            context.SaveChanges();
        }
    }
}