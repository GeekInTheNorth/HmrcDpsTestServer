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
            var summaries = service.GetDatasetSummaries();
            var model = new DatasetModel(summaries);

            return View(model);
        }
    }
}