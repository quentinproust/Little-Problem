using System;
using System.Collections.Generic;
using LittleProblem.Data.Model;
using MongoDB.Bson;

namespace LittleProblem.Data.Aggregate
{
    public class ProblemAggregate
    {
        public ObjectId Id { get; set; }
        public DateTime OpenedDate { get; set; }
        public Member Submitter { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public List<ResponseAggregate> Responses { get; set; }
    }

    public class ResponseAggregate
    {
        public string Text { get; set; }
        public int Note { get; set; }

        public Member Submitter { get; set; }
    }
}
