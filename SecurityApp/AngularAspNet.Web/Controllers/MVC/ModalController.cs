using System.Web.Mvc;

namespace SecurityApp.Web.Controllers.MVC
{
    public class ModalController : Controller
    {
        // GET: Modal
        public PartialViewResult Render(string feature, string name)
        {
            return PartialView($"~/App/{feature}/Modal/{name}");
        }
    }
}