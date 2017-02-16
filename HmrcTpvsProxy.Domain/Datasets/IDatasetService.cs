using System.Collections.Generic;
using System.IO;

namespace HmrcTpvsProxy.Domain.Datasets
{
    public interface IDatasetService
    {
        IEnumerable<DatasetSummary> GetDatasetSummaries();

        bool Create(string description, string payeReference);

        bool SaveCsv(int datasetId, RequestType messageType, Stream fileStream);
    }
}