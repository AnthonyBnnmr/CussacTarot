using ServiceStack.OrmLite;
using ServiceStack.Data;
using CussacTarot.Models;

namespace CussacTarot.Core.Services
{
    public static class LaunchBddService
    {
        private static string CONNECTION_BDD = $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/cussac_tarot.db";

        public static IDbConnectionFactory CreateBdd()
        {
            IDbConnectionFactory dbFactory = CreateConnectionBdd();            
            System.Data.IDbConnection db = dbFactory.OpenDbConnection();            
            db.CreateTableIfNotExists<Gamer>();
            db.CreateTableIfNotExists<GameSheet>();
            db.CreateTableIfNotExists<ScoreByGamer>();
            return dbFactory;
        }

        public static IDbConnectionFactory CreateConnectionBdd()
        {
            OrmLiteConnectionFactory dbFactory = new(CONNECTION_BDD, SqliteDialect.Provider);
            return dbFactory;
        }



    }
}
