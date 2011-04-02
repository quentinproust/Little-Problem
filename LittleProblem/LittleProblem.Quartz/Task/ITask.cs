using Quartz;

namespace LittleProblem.Quartz.Task
{
    public interface ITask
    {
        JobDetail Detail { get; }
        Trigger Trigger { get; }
    }
}
