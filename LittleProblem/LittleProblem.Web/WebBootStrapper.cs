using LittleProblem.Common.BootStrap;

namespace LittleProblem.Web
{
    public class WebBootStrapper : GlobalBootStrapper
    {
        public override void Initialize()
        {
            Register(new SMBootStrapper());

            Register(new Quartz.BootStrapper()); // Need Structure map to be boot strap first
            base.Initialize();
        }
    }
}