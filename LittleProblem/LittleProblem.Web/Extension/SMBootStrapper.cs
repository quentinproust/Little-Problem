using LittleProblem.Common.BootStrap;
using LittleProblem.Data;
using LittleProblem.Quartz;
using StructureMap;

namespace LittleProblem.Web.Extension
{
    public class SMBootStrapper : IBootStrap
    {
        public void Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<DataRegistry>();
                x.AddRegistry<QuartzRegistry>();
                x.AddRegistry<WebRegistry>();
            });
        }
    }
}