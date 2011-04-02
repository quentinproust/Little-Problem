using LittleProblem.Common.BootStrap;
using LittleProblem.Quartz.Task;
using StructureMap;

namespace LittleProblem.Quartz
{
    public class BootStrapper : IBootStrap
    {
        public void Initialize()
        {
            var facade = ObjectFactory.GetInstance<ICronFacade>();
            facade.CreateTask(new UpdateUserNoteTask());
        }
    }
}