using System;
using System.Collections.Generic;
using LittleProblem.Data.Aggregate;
using LittleProblem.Data.Model;

namespace LittleProblem.Data.Repository
{
    public interface IProblemRepository
    {
        /// <summary>
        /// Return the number of problem for each pages.
        /// </summary>
        Int32 PageSize { get; }

        /// <summary>
        /// Get all problem corresponding to a page number.
        /// The number of problem can't be greater that PageSize.
        /// </summary>
        /// <param name="start">Page number.</param>
        /// <returns>Get all problems for the requested page.</returns>
        List<Problem> All(int start);

        /// <summary>
        /// Load a problem using its Id.
        /// It also load member of the problem responses.
        /// </summary>
        /// <param name="id">Problem id</param>
        /// <returns>A problem aggregate with more informations than just problem info.</returns>
        ProblemAggregate Get(string id);
        
        int Count();
    }
}
