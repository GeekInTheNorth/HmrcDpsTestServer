using System.Web.Mvc;
using HmrcTpvsProxy.DAL.Repositories;
using HmrcTpvsProxy.Domain.Datasets;
using TestProxy.Models.Dataset;

namespace TestProxy.Controllers.MVC
{
    public class DatasetController : Controller
    {
        private readonly IDatasetService service;

        public DatasetController()
        {
            service = new DatasetService(new DatasetRepository());
        }

        // GET: Dataset
        public ActionResult Index()
        {
            var apiPath = Request.Url.AbsoluteUri.Replace("/Index", string.Empty);
            apiPath = apiPath.Replace("Dataset", "api/Dataset/{0}");

            var summaries = service.GetDatasetSummaries();
            var model = new DatasetModel(summaries, apiPath);

            return View(model);
        }
    }
}