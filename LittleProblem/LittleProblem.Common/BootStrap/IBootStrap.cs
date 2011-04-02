namespace LittleProblem.Common.BootStrap
{
    /// <summary>
    /// Interface to specify some actions to do when the application is starting.
    /// </summary>
    public interface IBootStrap
    {
        /// <summary>
        /// Action to do when application start.
        /// </summary>
        void Initialize();
    }
}
