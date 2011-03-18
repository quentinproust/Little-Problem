using MongoDB.Driver;

namespace LittleProblem.Data.Server
{
    public interface IConnexion
    {
        MongoCollection<T> Collection<T>(string colName);
    }
}
