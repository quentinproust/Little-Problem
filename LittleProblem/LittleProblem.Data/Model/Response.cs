using MongoDB.Bson;

namespace LittleProblem.Data.Model
{
    public class Response
    {
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public string Text { get; set; }
        public int Note { get; set; }

        public Response()
        {
            Id = ObjectId.GenerateNewId();
        }
    }
}
