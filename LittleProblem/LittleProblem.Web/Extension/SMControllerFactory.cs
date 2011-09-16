using System;
using System.Web.Mvc;
using System.Web.Routing;
using NLog;
using StructureMap;

namespace LittleProblem.Web.Extension
{
    public class SMControllerFactory : DefaultControllerFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            try
            {
                var controllerType = base.GetControllerType(requestContext, controllerName);
                var controller = ObjectFactory.GetInstance(controllerType) as IController;
                ObjectFactory.BuildUp(controller);
                return controller;
            }
            catch (Exception ex)
            {
                Logger.InfoException("Default controller logic is being used.", ex);

                //Use the default logic.);
                return base.CreateController(requestContext, controllerName);
            }

        }
    }
}