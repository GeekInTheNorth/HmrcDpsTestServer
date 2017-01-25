using System.Collections.Generic;

namespace HmrcTpvsProxy.Domain.Datasets
{
    public interface IDatasetService
    {
        IEnumerable<DatasetSummary> GetDatasetSummaries();

        bool Create(string description, string payeReference);
    }
}