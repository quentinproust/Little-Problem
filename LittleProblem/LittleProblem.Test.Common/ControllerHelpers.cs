using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FakeItEasy;

namespace LittleProblem.Test.Common
{
    public static class ControllerHelpers
    {
        private static HttpContextBase FakeHttpContext()
        {
            var context = A.Fake<HttpContextBase>();
            HttpSessionStateBase session = null; // Controller are forbidden to call session directly
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
