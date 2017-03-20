using System.Web.Mvc;

namespace SecurityApp.Web.Controllers.MVC
{
    public class ApplicationsController : Controller
    {
        // GET: Applications
        public ActionResult Index()
        {
            return View();
        }
    }
}