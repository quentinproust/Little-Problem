using MongoDB.Driver;

namespace LittleProblem.Data.Server
{
    public class DbConnexion : IConnexion
    {
        private readonly MongoDatabase db;

        public DbConnexion() : this("LittleProblem")
        {
        }

        public DbConnexion(string databaseName)
        {
            MongoServer server = MongoServer.Create("mongodb://localhost");
            db = server.GetDatabase(databaseName);
        }

        public MongoCollection<T> Collection<T>(string colName)
        {
            return db.GetCollection<T>(colName);
        }
    }
}
