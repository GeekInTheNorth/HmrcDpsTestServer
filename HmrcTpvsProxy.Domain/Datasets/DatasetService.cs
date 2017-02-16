using System;
using System.Collections.Generic;
using System.IO;
using HmrcTpvsProxy.Domain.Datasets.CsvParsing;
using HmrcTpvsProxy.Domain.Validators;

namespace HmrcTpvsProxy.Domain.Datasets
{
    public class DatasetService : IDatasetService
    {
        private readonly IDatasetRepository repository;
        private readonly IValidator payReferenceValidator;
        private readonly ICsvParser parser; 

        public DatasetService(IDatasetRepository repository, IValidator payReferenceValidator, ICsvParser parser)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (payReferenceValidator == null) throw new ArgumentNullException(nameof(payReferenceValidator));
            if (parser == null) throw new ArgumentNullException(nameof(parser));

            this.repository = repository;
            this.payReferenceValidator = payReferenceValidator;
            this.parser = parser;
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

        public bool SaveCsv(int datasetId, RequestType messageType, Stream fileStream)
        {
            var messages = parser.Parse(fileStream, messageType);

            return repository.Save(datasetId, messageType, messages);
        }
    }
}