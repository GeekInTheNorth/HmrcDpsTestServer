using System.Collections.Generic;

namespace HmrcTpvsProxy.Domain.Datasets
{
    public interface IDatasetService
    {
        IEnumerable<DatasetSummary> GetDatasetSummaries();
    }
}