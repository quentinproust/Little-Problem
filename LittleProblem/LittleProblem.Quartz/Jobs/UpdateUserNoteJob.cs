using System;
using LittleProblem.Data.Services;
using NLog;
using Quartz;

namespace LittleProblem.Quartz.Jobs
{
    /// <summary>
    /// Job that will start a map/reduce to get user note.
    /// </summary>
    public class UpdateUserNoteJob : IJob
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMembershipService _membershipService;

        public UpdateUserNoteJob(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        public void Execute(JobExecutionContext context)
        {
            _membershipService.UpdateUserNote();
            Logger.Info("User notes have been updated successfully");
        }
    }
}
