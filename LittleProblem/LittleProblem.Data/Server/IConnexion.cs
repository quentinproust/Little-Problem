using MongoDB.Driver;

namespace LittleProblem.Data.Server
{
    public interface IConnexion
    {
        /// <summary>
        /// Get a mongo database
        /// </summary>
        MongoDatabase Database { get; }
        /// <summary>
        /// Get a collection from its name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="colName"></param>
        /// <returns></returns>
        MongoCollection<T> Collection<T>(string colName);

        // TODO : create an abstraction to get collection from type ?
    }
}
