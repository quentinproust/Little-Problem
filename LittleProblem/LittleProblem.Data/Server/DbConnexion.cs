using MongoDB.Driver;

namespace LittleProblem.Data.Server
{
    public class DbConnexion : IConnexion
    {
        private readonly MongoDatabase _db;

        public DbConnexion() : this("LittleProblem")
        {
        }

        public DbConnexion(string databaseName)
        {
            MongoServer server = MongoServer.Create("mongodb://localhost");
            _db = server.GetDatabase(databaseName);
        }

        public MongoDatabase Database
        {
            get { return _db; }
        }

        public MongoCollection<T> Collection<T>(string colName)
        {
            return _db.GetCollection<T>(colName);
        }
    }
}
