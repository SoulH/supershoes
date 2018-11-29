using Api.Security;
using System.Web.Mvc;

namespace Api.Controllers
{
    [BasicAuthentication]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Help", new { area = "" });
        }
    }
}
