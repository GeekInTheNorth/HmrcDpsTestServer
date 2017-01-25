using System;
using System.Collections.Generic;
using HmrcTpvsProxy.Domain.Validators;

namespace HmrcTpvsProxy.Domain.Datasets
{
    public class DatasetService : IDatasetService
    {
        private readonly IDatasetRepository repository;
        private readonly IValidator payReferenceValidator;

        public DatasetService(IDatasetRepository repository, IValidator payReferenceValidator)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (payReferenceValidator == null) throw new ArgumentNullException(nameof(payReferenceValidator));

            this.repository = repository;
            this.payReferenceValidator = payReferenceValidator;
        }

        public bool Create(string description, string payeReference)
        {
            var sanitisedDescription = description.Trim();
            var sanitisedPayeReference = payeReference.Trim().ToUpper();

            if (!payReferenceValidator.Validate(sanitisedPayeReference))
                return false;

            try
            {
                repository.Create(sanitisedDescription, sanitisedPayeReference);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<DatasetSummary> GetDatasetSummaries()
        {
            return repository.GetDatasetSummaries();
        }
    }
}