using System.Web.Mvc;

namespace TestProxy.Controllers.MVC
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var view = View();
            view.ViewBag.Title = "Home";

            return view;
        }
    }
}