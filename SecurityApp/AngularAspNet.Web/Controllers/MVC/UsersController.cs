using System.Web.Mvc;

namespace SecurityApp.Web.Controllers.MVC
{
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}