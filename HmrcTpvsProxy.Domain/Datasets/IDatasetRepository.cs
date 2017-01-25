using System.Collections.Generic;

namespace HmrcTpvsProxy.Domain.Datasets
{
    public interface IDatasetRepository
    {
        IEnumerable<DatasetSummary> GetDatasetSummaries();

        bool Create(string description, string payeReference);
    }
}