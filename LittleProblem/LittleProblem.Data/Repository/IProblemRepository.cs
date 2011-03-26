using System.Collections.Generic;
using LittleProblem.Data.Aggregate;
using LittleProblem.Data.Model;

namespace LittleProblem.Data.Repository
{
    public interface IProblemRepository
    {
        List<Problem> All();
        List<Problem> All(int start);
        ProblemAggregate Get(string id);
        int Count();
    }
}
