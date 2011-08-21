using System;
using LittleProblem.Data.Services;
using NLog;
using Quartz;
using StructureMap;

namespace LittleProblem.Quartz.Jobs
{
    /// <summary>
    /// Job that will start a map/reduce to get user note.
    /// </summary>
    public class UpdateUserNoteJob : IJob
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static IMembershipService _service;

        public IMembershipService Service { 
            get { return _service ?? (_service = ObjectFactory.GetInstance<IMembershipService>()); }
            set { _service = value; }
        }

        public void Execute(JobExecutionContext context)
        {
            Service.UpdateUserNote();
            Logger.Info("User notes have been updated successfully");
        }
    }
}
