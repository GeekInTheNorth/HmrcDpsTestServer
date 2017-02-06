using System.Collections.Generic;
using System.Web.Mvc;
using HmrcTpvsProxy.Domain;

namespace TestProxy.Models.Dataset
{
    public class DatasetUploadModel
    {
        public string DatasetBeingModified { get; set; }

        public string MessageType { get; set; }

        public bool OverwriteExistingRecords { get; set; }

        public List<SelectListItem> MessageTypes { get; set; }

        public string ValidationError { get; set; }

        public bool ShowValidationError { get; set; }

        public DatasetUploadModel()
        {
            MessageTypes = new List<SelectListItem>();
            MessageTypes.Add(GetSelectListItem(RequestType.P6));
            MessageTypes.Add(GetSelectListItem(RequestType.P9));
            MessageTypes.Add(GetSelectListItem(RequestType.SL1));
            MessageTypes.Add(GetSelectListItem(RequestType.SL2));

            MessageType = RequestType.P6.ToString();

            ValidationError = string.Empty;
            ShowValidationError = false;
        }

        private SelectListItem GetSelectListItem(RequestType requestType)
        {
            return new SelectListItem
            {
                Text = requestType.ToString(),
                Value = requestType.ToString()
            };
        }
    }
}