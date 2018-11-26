using App.Utils;
using System.Web.Mvc;

namespace App.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.User = this.GetUser(User.Identity);
            ViewBag.Controller = this.GetName();
            return View();
        }
    }
}
