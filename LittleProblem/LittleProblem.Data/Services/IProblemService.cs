using LittleProblem.Data.Model;

namespace LittleProblem.Data.Services
{
    public interface IProblemService
    {
        /// <summary>
        /// Create a problem.
        /// </summary>
        /// <param name="title">Title of the problem.</param>
        /// <param name="text">Description of the problem.</param>
        /// <param name="member">Author of the problem.</param>
        /// <returns>Problem that has been created.</returns>
        Problem CreateProblem(string title, string text, Member member);

        /// <summary>
        /// Add a response to an existing problem.
        /// The problem should not has been closed or have a solution.
        /// </summary>
        /// <param name="problemId">Problem Id</param>
        /// <param name="text">Message of the response.</param>
        /// <param name="member">Member that has answer the problem.</param>
        void AddResponse(string problemId, string text, Member member);

        /// <summary>
        /// Close a problem.
        /// No solution has been found but it is closed even though.
        /// </summary>
        /// <param name="problemId">Problem that is closed.</param>
        /// <param name="member">
        /// Member closing the problem. 
        /// It should the author, an admin or a moderator (admin and moderator doesn't exist yet).
        /// </param>
        void CloseProblem(string problemId, Member member);

        /// <summary>
        /// Validate a response as a solution to a problem.
        /// The user who had answer will be given some points.
        /// </summary>
        /// <param name="problemId">Problem that has been solved.</param>
        /// <param name="member">Member who has solved the problem.</param>
        /// <param name="response">Response containing the solution.</param>
        void ValidateAsASolution(string problemId, Member member, Response response);
        
        /// <summary>
        /// Add some points to a response.
        /// The member seems to think that this response has some value.
        /// </summary>
        /// <param name="problemId">Problem containing the response.</param>
        /// <param name="responseId">Response gaining points.</param>
        /// <param name="member">
        /// Member giving points so we can check 
        /// if it's not the member who has answered.
        /// </param>
        void UpResponse(string problemId, string responseId, Member member);

        /// <summary>
        /// Remove some points to a response.
        /// The member seems to think that this response is not helping at all.
        /// </summary>
        /// <param name="problemId">Problem containing the response.</param>
        /// <param name="responseId">Response losing points.</param>
        /// <param name="member">
        /// Member removing points so we can check 
        /// if it's not the member who has answered.
        /// </param>
        void DownResponse(string problemId, string responseId, Member member);
    }
}
