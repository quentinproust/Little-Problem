using MongoDB.Driver;

namespace LittleProblem.Data.Server
{
    public interface IConnexion
    {
        MongoDatabase Database { get; }
        MongoCollection<T> Collection<T>(string colName);
    }
}
