using System;
using System.Collections.Generic;

namespace HmrcTpvsProxy.Domain.Datasets
{
    public class DatasetService : IDatasetService
    {
        private readonly IDatasetRepository repository;

        public DatasetService(IDatasetRepository repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            this.repository = repository;
        }

        public IEnumerable<DatasetSummary> GetDatasetSummaries()
        {
            return repository.GetDatasetSummaries();
        }
    }
}