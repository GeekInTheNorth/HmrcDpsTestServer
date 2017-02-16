using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HmrcTpvsProxy.DAL.Repositories;
using HmrcTpvsProxy.Domain;
using HmrcTpvsProxy.Domain.Datasets;
using HmrcTpvsProxy.Domain.Datasets.CsvParsing;
using HmrcTpvsProxy.Domain.Validators;
using TestProxy.Models.Dataset;

namespace TestProxy.Controllers.MVC
{
    public class DatasetController : Controller
    {
        private readonly IDatasetService service;

        public DatasetController()
        {
            service = new DatasetService(new DatasetRepository(), new PayeReferenceValidator(), new CsvParser());
        }

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

        public ActionResult Upload(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index");

            var datasetBeingModified = service.GetDatasetSummaries().FirstOrDefault(x => x.Id == id.Value);

            if (datasetBeingModified == null) return RedirectToAction("Index");

            var model = new DatasetUploadModel { DatasetBeingModified = datasetBeingModified.Name };

            return View(model);
        }

        [HttpPost]
        public ActionResult Upload(int? id, DatasetUploadModel model, HttpPostedFileBase file)
        {
            var fileName = file.FileName;

            if (!fileName.EndsWith(".csv", StringComparison.CurrentCultureIgnoreCase))
            {
                model.ShowValidationError = true;
                model.ValidationError = "Only CSV files should be uploaded.";
                model.DatasetBeingModified = service.GetDatasetSummaries().FirstOrDefault(x => x.Id == id.Value).Name;

                return View(model);
            }

            try
            {
                RequestType messageType;
                Enum.TryParse(model.MessageType, out messageType);

                service.SaveCsv(id.Value, messageType, file.InputStream);
            }
            catch
            {
                model.ShowValidationError = true;
                model.ValidationError = "Unable to parse the provided file.";
                model.DatasetBeingModified = service.GetDatasetSummaries().FirstOrDefault(x => x.Id == id.Value).Name;

                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}