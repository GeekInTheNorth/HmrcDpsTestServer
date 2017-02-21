using System.Collections.Generic;
using HmrcTpvsProxy.Domain.Datasets;

namespace TestProxy.Models.Dataset
{
    public class DatasetViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string MessageType { get; set; }

        public List<MessageDTO> Messages { get; set; }

        public bool CanEdit { get; set; }
    }
}