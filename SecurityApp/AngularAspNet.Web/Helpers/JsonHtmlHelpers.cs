using System.Web;
using System.Web.Mvc;
using SecurityApp.Web.Utilities;

namespace SecurityApp.Web.Helpers
{
    public static class JsonHtmlHelpers
    {
        public static IHtmlString JsonFor<T>(this HtmlHelper helper, T obj)
        {
            return helper.Raw(obj.ToJson());
        }
    }
}