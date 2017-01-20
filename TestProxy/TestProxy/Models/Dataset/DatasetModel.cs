using System.Collections.Generic;
using System.Linq;
using HmrcTpvsProxy.Domain.Datasets;

namespace TestProxy.Models.Dataset
{
    public class DatasetModel
    {
        public IEnumerable<DatasetSummary> Summaries { get; private set; }

        public IEnumerable<string> MessageTypes { get; private set; }

        public string ApiUrl { get; private set; }

        public DatasetModel(IEnumerable<DatasetSummary> summaries, string apiUrl)
        {
            ApiUrl = apiUrl;
            Summaries = summaries;
            MessageTypes = summaries.SelectMany(x => x.MessageCounts.Select(y => y.Key)).Distinct();
        }
    }
}