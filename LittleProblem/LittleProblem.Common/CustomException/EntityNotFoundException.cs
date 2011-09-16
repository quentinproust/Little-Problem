using System;

namespace LittleProblem.Common.CustomException
{
    /// <summary>
    /// Exception thrown when an entity is not found in mongoDB.
    /// </summary>
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
