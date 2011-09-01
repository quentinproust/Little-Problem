using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FakeItEasy;
using LittleProblem.Test.Common.Session;

namespace LittleProblem.Test.Common
{
    public static class ControllerHelpers
    {
        public static HttpContextBase FakeHttpContext()
        {
            var context = A.Fake<HttpContextBase>();
            var session = new SessionWrapper();
            var request = A.Fake<HttpRequestBase>();
            var response = A.Fake<HttpResponseBase>();
            var server = A.Fake<HttpServerUtilityBase>();

            A.CallTo(() => context.Request).Returns(request);
            A.CallTo(() => context.Response).Returns(response);
            A.CallTo(() => context.Session).Returns(session);
            A.CallTo(() => context.Server).Returns(server);

            return context;
        }

        public static Controller InjectFakeContext(this Controller controller, RouteData route = null)
        {
            if(route == null) route = new RouteData();

            // assign the fake context
            var context = new ControllerContext(FakeHttpContext(),
                              route,
                              controller);

            controller.ControllerContext = context;
            return controller;
        }
    }
}
