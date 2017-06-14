using System.Web.Mvc;

namespace HanoiAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/app/");
        }
    }
}