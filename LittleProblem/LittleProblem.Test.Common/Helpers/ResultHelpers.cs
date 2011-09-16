using System.Web.Mvc;

namespace LittleProblem.Test.Common.Helpers
{
    public static class ResultHelpers
    {
        public static string Error(this ViewResult result, string field = "", int index = 0)
        {
            return result.ViewData.ModelState[field].Errors[index].ErrorMessage;
        }
    }
}
