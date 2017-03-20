using System.Web.Mvc;

namespace SecurityApp.Web.Controllers.MVC
{
    /// <summary>
    /// Generic controller to render strongly typed Angular views.
    /// </summary>
    public class NgController : Controller
    {
        public PartialViewResult RenderComponents(string feature, string name)
        {
            return PartialView($"~/App/{feature}/Components/{name}");
        }
    }
}