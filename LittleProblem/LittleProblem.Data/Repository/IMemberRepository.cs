using LittleProblem.Data.Model;
using MongoDB.Bson;

namespace LittleProblem.Data
{
    public interface IMemberRepository
    {
        /// <summary>
        /// Get a member from its openId.
        /// </summary>
        /// <param name="openId">OpenId</param>
        /// <returns>Existing member.</returns>
        Member Get(string openId);
        /// <summary>
        /// Get a member from it mongo id.
        /// </summary>
        /// <param name="id">Object Id</param>
        /// <returns>Existing member.</returns>
        Member Get(ObjectId id);
    }
}
