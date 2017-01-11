using System.Linq;
using System.Web.Mvc;
using HmrcTpvsProxy.Domain;
using HmrcTpvsProxy.Domain.TestDataTransformer;

namespace TestProxy.Controllers.MVC
{
    public class StudentLoanEdgeCasesController : Controller
    {
        // GET: StudentLoanEdgeCases
        public ActionResult Index()
        {
            var responseFileRetriever = new ResponseFileRetriever();
            var sl1Xml = responseFileRetriever.GetResponse(RequestType.SL1);
            var sl2Xml = responseFileRetriever.GetResponse(RequestType.SL2);

            var transformer = new StudentLoanNoticeTransformer();
            var transformedNotices = transformer.Transform(sl1Xml).ToList();
            transformedNotices.AddRange(transformer.Transform(sl2Xml));

            transformedNotices = transformedNotices.OrderBy(x => x.Name).ThenBy(x => x.EffectiveDate).ToList();

            var view = View(transformedNotices);
            view.ViewBag.Title = "Student Loan Notices";

            return View();
        }
    }
}