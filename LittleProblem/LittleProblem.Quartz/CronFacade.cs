using LittleProblem.Quartz.Task;
using NLog;
using Quartz;

namespace LittleProblem.Quartz
{
    public class CronFacade : ICronFacade
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IScheduler _scheduler;

        public CronFacade(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public void CreateTask(ITask task)
        {
            Logger.Info("A new task has been scheduled : " + task.GetType());
            var date = _scheduler.ScheduleJob(task.Detail, task.Trigger);
        }
    }
}
