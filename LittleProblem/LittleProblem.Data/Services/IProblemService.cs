using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LittleProblem.Data.Model;

namespace LittleProblem.Data.Services
{
    public interface IProblemService
    {
        Problem CreateProblem(string title, string text, Member member);
        void AddResponse(string problemId, string text, Member member);
        void CloseProblem(string problemId, Member member);
        void ValidateAsASolution(string problemId, Member member, Response response);
        void DownResponse(string problemId, string responseId, Member member);
        void UpResponse(string problemId, string responseId, Member member);
    }
}
