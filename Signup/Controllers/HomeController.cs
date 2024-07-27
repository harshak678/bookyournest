using System.Web.Mvc;

namespace Signup.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult signup()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult signin()
        {


            return View();
        }


    }
}