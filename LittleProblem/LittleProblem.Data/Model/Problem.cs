using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace LittleProblem.Data.Model
{
    public class Problem
    {
        public ObjectId Id { get; set; }
        public ObjectId UserId { get; set; }
        public DateTime OpenedDate { get; set; }
        public DateTime? ClosureDate { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Response CurrentSolution { get; set; }
        public List<Response> Responses { get; set; }

        public Problem()
        {
            Responses = new List<Response>();
            ClosureDate = null;
        }

        public bool IsClosed()
        {
            return ClosureDate != null;
        }
    }
}
