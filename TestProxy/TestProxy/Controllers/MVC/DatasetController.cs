using System.Web.Mvc;
using HmrcTpvsProxy.DAL.Repositories;
using HmrcTpvsProxy.Domain.Datasets;
using HmrcTpvsProxy.Domain.Validators;
using TestProxy.Models.Dataset;

namespace TestProxy.Controllers.MVC
{
    public class DatasetController : Controller
    {
        private readonly IDatasetService service;

        public DatasetController()
        {
            service = new DatasetService(new DatasetRepository(), new PayeReferenceValidator());
        }

        // GET: Dataset
        public ActionResult Index()
        {
            var apiPath = Request.Url
                                 .AbsoluteUri
                                 .ToLower()
                                 .Replace("/index", string.Empty)
                                 .Replace("dataset", "api/dataset/{0}");

            var summaries = service.GetDatasetSummaries();
            var model = new DatasetModel(summaries, apiPath);

            return View(model);
        }

        public ActionResult Create()
        {
            return View(new DatasetEditModel());
        }

        [HttpPost]
        public ActionResult Create(DatasetEditModel model)
        {
            if (service.Create(model.Description, model.PayeReference))
                return RedirectToAction("Index");

            model.ShowValidationError = true;

            return View(model);
        }
    }
}