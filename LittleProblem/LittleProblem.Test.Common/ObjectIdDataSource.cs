using AutoPoco.Engine;
using MongoDB.Bson;

namespace LittleProblem.Test.Common
{
    /// <summary>
    /// Get autopoco to create a new object id for test objects.
    /// </summary>
    public class ObjectIdDataSource : IDatasource<ObjectId>
    {
        public object Next(IGenerationSession session)
        {
            return ObjectId.GenerateNewId();
        }
    }
}
