using System;
using System.Runtime.Serialization;

namespace LittleProblem.Common.CustomException
{
    /// <summary>
    /// Exception thrown when an entity is not found in mongoDB.
    /// </summary>
    [Serializable]
    public class EntityNotFoundException : System.Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
