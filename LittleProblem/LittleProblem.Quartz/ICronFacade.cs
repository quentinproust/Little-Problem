using LittleProblem.Quartz.Task;

namespace LittleProblem.Quartz
{
    public interface ICronFacade
    {
        /// <summary>
        /// Create a task which contains everything needed to start a job.
        /// </summary>
        /// <param name="task">New Task</param>
        void CreateTask(ITask task);
    }
}
