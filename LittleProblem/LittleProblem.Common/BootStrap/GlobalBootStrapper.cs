using System;
using System.Collections.Generic;
using LittleProblem.Common.Colllections;

namespace LittleProblem.Common.BootStrap
{
    /// <summary>
    /// BootStrapper that will register other bootstrapper to initialize them.
    /// </summary>
    public class GlobalBootStrapper : IBootStrap
    {
        /// <summary>
        /// If I wanted to make something really cool, it would be dealing with graph.
        /// But that would be a lot of complexity without any need.
        /// </summary>
        private readonly IList<IBootStrap> _bootList = new List<IBootStrap>();

        /// <summary>
        /// Register an other bootstrapper to initialize.
        /// </summary>
        /// <param name="boot">BootStrapper to initialize.</param>
        public void Register(IBootStrap boot)
        {
            if (_bootList.Contains(boot)) 
                throw new InvalidOperationException("This bootstrapper has already been added to the initialization list.");

            _bootList.Add(boot);
        }

        /// <summary>
        /// Initialize bootstrapper.
        /// </summary>
        public virtual void Initialize()
        {
            _bootList.ForEach(x => x.Initialize());
        }
    }
}
