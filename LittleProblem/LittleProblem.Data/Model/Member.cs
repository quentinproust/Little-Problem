using System;
using MongoDB.Bson;

namespace LittleProblem.Data.Model
{
    public class Member
    {
        public ObjectId Id { get; set; }
        public string OpenId { get; set; }
        public string UserName { get; set; }
        public DateTime LastConnection { get; set; }
        public DateTime FirstConnection { get; set; }
        public string Email { get; set; }
    }
}
