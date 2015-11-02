using System.Web.Mvc;

namespace TestProxy.Controllers
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