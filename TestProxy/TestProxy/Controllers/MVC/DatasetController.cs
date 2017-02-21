using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HmrcTpvsProxy.DAL.Repositories;
using HmrcTpvsProxy.Domain;
using HmrcTpvsProxy.Domain.Datasets;
using HmrcTpvsProxy.Domain.Datasets.CsvFiles;
using HmrcTpvsProxy.Domain.Validators;
using TestProxy.Filters;
using TestProxy.Models.Dataset;

namespace TestProxy.Controllers.MVC
{
    public class DatasetController : BaseController
    {
        private readonly IDatasetService service;

        public DatasetController()
        {
            service = new DatasetService(new DatasetRepository(), new PayeReferenceValidator(), new CsvParser(), new CsvCreator());
        }

        public ActionResult Index()
        {
            var apiPath = Request.Url
                                 .AbsoluteUri
                                 .ToLower()
                                 .Replace("/index", string.Empty)
                                 .Replace("dataset", "api/dataset/{0}");

            var summaries = service.GetDatasetSummaries();
            var model = new DatasetModel(summaries, apiPath, CanEdit);

            return View(model);
        }

        [IPAccess("Dataset", "Index")]
        public ActionResult Create()
        {
            return View(new DatasetEditModel());
        }

        [HttpPost]
        [IPAccess("Dataset", "Index")]
        public ActionResult Create(DatasetEditModel model)
        {
            if (service.Create(model.Description, model.PayeReference))
                return RedirectToAction("Index");

            model.ShowValidationError = true;

            return View(model);
        }

        [IPAccess("Dataset", "Index")]
        public ActionResult Upload(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index");

            var datasetBeingModified = service.GetDatasetSummaries().FirstOrDefault(x => x.Id == id.Value);

            if (datasetBeingModified == null) return RedirectToAction("Index");

            var model = new DatasetUploadModel { DatasetBeingModified = datasetBeingModified.Name };

            return View(model);
        }

        [HttpPost]
        [IPAccess("Dataset", "Index")]
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

        public ActionResult View(int? id, string messageType)
        {
            if (!id.HasValue || string.IsNullOrWhiteSpace(messageType))
                return RedirectToAction("Index");

            RequestType messageTypeEnum;
            if (!Enum.TryParse(messageType, true, out messageTypeEnum))
                return RedirectToAction("Index");

            var model = new DatasetViewModel
            {
                Id = id.Value,
                MessageType = messageType,
                Description = service.GetDatasetSummaries().FirstOrDefault(x => x.Id == id.Value).Name,
                Messages = service.GetMessages(id.Value, messageTypeEnum).ToList(),
                CanEdit = CanEdit
            };

            return View(model);
        }

        public FileStreamResult Download(int? id, string messageType)
        {
            if (!id.HasValue || string.IsNullOrWhiteSpace(messageType))
                return null;

            RequestType messageTypeEnum;
            if (!Enum.TryParse(messageType, true, out messageTypeEnum))
                return null;

            var csvInMemory = service.GetMessagesAsCsvInMemory(id.Value, messageTypeEnum);
            var memoryStream = new MemoryStream(csvInMemory);

            return new FileStreamResult(memoryStream, "text/csv")
            {
                FileDownloadName = string.Format("{0}Messages.csv", messageTypeEnum)
            };
        }
    }
}