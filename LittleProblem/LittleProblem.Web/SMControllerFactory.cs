using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace LittleProblem.Web
{
    public class SMControllerFactory : DefaultControllerFactory
    {
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
                //Use the default logic.
                return base.CreateController(requestContext, controllerName);
            }

        }
    }
}