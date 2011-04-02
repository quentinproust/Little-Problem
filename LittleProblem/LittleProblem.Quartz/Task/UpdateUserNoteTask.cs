using System;
using LittleProblem.Quartz.Jobs;
using Quartz;

namespace LittleProblem.Quartz.Task
{
    public class UpdateUserNoteTask : ITask
    {
        public JobDetail Detail
        {
            get
            {
                var detail = new JobDetail
                {
                    Description = "Task to update user note in MongoDB.",
                    Name = GetType().ToString(),
                    JobType = typeof(UpdateUserNoteJob)
                };
                return detail;
            }
        }

        public Trigger Trigger
        {
            get
            {
                var trigger = TriggerUtils.MakeHourlyTrigger();
                trigger.StartTimeUtc = TriggerUtils.GetEvenHourDate(DateTime.UtcNow);
                trigger.Name = "myTrigger";
                return trigger;
            }
        }
    }
}
