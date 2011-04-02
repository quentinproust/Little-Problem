using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Spi;
using StructureMap;

namespace LittleProblem.Quartz
{
    internal class SMJobFactory : IJobFactory
    {
        public IJob NewJob(TriggerFiredBundle bundle)
        {
            Type jobType = bundle.JobDetail.JobType;
            var job = ObjectFactory.GetInstance(jobType) as IJob;
            ObjectFactory.BuildUp(job);
            return job;
        }
    }
}
