using System.Collections.Generic;
using HmrcTpvsProxy.Domain.Datasets.CsvParsing;

namespace HmrcTpvsProxy.Domain.Datasets
{
    public interface IDatasetRepository
    {
        IEnumerable<DatasetSummary> GetDatasetSummaries();

        bool Create(string description, string payeReference);

        bool Save(int datasetId, RequestType messageType, IEnumerable<MessageDTO> messages);

        IEnumerable<MessageDTO> GetMessages(int datasetId, RequestType messageType);
    }
}