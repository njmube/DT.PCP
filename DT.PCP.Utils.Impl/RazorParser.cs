using RazorEngine;

namespace DT.PCP.Utils.Impl
{
    public static class RazorParser
    {
        public static string ParseView(string view, object model)
        {
            return Razor.Parse(view, model);
        }
    }
}
