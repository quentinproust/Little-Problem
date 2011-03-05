using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;
using LittleProblem.Data;

namespace LittleProblem.Web
{
    public class SMControllerFactory : DefaultControllerFactory
    {
        public SMControllerFactory()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<DataRegistry>();
            });
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            try
            {
                var controllerType = base.GetControllerType(requestContext, controllerName);
                var controller = ObjectFactory.GetInstance(controllerType) as IController;
                ObjectFactory.BuildUp(controller);
                return controller;
            }
            catch (Exception)
            {
                //Use the default logic.
                return base.CreateController(requestContext, controllerName);
            }

        }
    }
}