using Quartz;
using Quartz.Impl;
using StructureMap.Configuration.DSL;

namespace LittleProblem.Quartz
{
    public class QuartzRegistry : Registry
    {
        public QuartzRegistry()
        {
            For<IScheduler>().Singleton().Use(x =>
            {
                ISchedulerFactory schedFact = new StdSchedulerFactory();
                IScheduler sched = schedFact.GetScheduler();
                sched.JobFactory = new SMJobFactory();

                sched.Start();
                return sched;
            });
            For<ICronFacade>().Singleton().Use<CronFacade>();
        }

    }
}
