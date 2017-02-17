using System;
using System.Collections.Generic;
using System.IO;
using HmrcTpvsProxy.Domain.Datasets.CsvFiles;
using HmrcTpvsProxy.Domain.Validators;

namespace HmrcTpvsProxy.Domain.Datasets
{
    public class DatasetService : IDatasetService
    {
        private readonly IDatasetRepository repository;
        private readonly IValidator payReferenceValidator;
        private readonly ICsvParser parser;
        private readonly ICsvCreator creator;

        public DatasetService(IDatasetRepository repository, IValidator payReferenceValidator, ICsvParser parser, ICsvCreator creator)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (payReferenceValidator == null) throw new ArgumentNullException(nameof(payReferenceValidator));
            if (parser == null) throw new ArgumentNullException(nameof(parser));
            if (creator == null) throw new ArgumentNullException(nameof(creator));

            this.repository = repository;
            this.payReferenceValidator = payReferenceValidator;
            this.parser = parser;
            this.creator = creator;
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

        public IEnumerable<MessageDTO> GetMessages(int datasetId, RequestType messageType)
        {
            var allowedTypes = new List<RequestType>
            {
                RequestType.P6,
                RequestType.P9,
                RequestType.SL1,
                RequestType.SL2
            };

            if (!allowedTypes.Contains(messageType))
                return new List<MessageDTO>();

            return repository.GetMessages(datasetId, messageType);
        }

        public byte[] GetMessagesAsCsvInMemory(int datasetId, RequestType messageType)
        {
            var messages = GetMessages(datasetId, messageType);

            return creator.CreateCsvInMemory(messages, messageType);
        }
    }
}