using System.Web.Mvc;

namespace SecurityApp.Web.Controllers.MVC
{
    public class TemplateController : Controller
    {
        // GET: Templates
        public PartialViewResult Render(string feature, string name)
        {
            return PartialView($"~/App/{feature}/Template/{name}");
        }
    }
}