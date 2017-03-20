using System.Web.Mvc;

namespace SecurityApp.Web.Controllers.MVC
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}