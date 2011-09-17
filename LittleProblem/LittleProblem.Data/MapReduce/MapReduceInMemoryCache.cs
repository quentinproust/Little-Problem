using System.Collections.Generic;
using MongoDB.Bson;

namespace LittleProblem.Data.MapReduce
{
    public class MapReduceInMemoryCache
    {
        private readonly IDictionary<string, BsonJavaScript> _cacheCollection = new Dictionary<string, BsonJavaScript>();

        public bool IsCached(string mapReduceFile)
        {
            return _cacheCollection.ContainsKey(mapReduceFile);
        }

        public BsonJavaScript Get(string mapReduceFile)
        {
            return _cacheCollection[mapReduceFile];
        }

        public void Save(string mapReduceFile, BsonJavaScript code)
        {
            _cacheCollection[mapReduceFile] = code;
        }
    }
}
