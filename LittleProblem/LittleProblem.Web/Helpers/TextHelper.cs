namespace LittleProblem.Web.Helpers
{
    public static class TextHelper
    {
        public static string TransformLine(this string text)
        {
            return text.Replace("\n", "<br />");
        }
    }
}