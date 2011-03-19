using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LittleProblem.Data.Model;

namespace LittleProblem.Data.Repository
{
    public interface IProblemRepository
    {
        List<Problem> All();
        List<Problem> All(int start);
        Problem Get(string id);
    }
}
