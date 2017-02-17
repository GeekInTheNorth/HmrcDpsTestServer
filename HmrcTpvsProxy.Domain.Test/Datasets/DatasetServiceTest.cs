using System;
using System.Collections.Generic;
using System.Linq;
using HmrcTpvsProxy.Domain.Datasets;
using HmrcTpvsProxy.Domain.Datasets.CsvFiles;
using HmrcTpvsProxy.Domain.Validators;
using Moq;
using NUnit.Framework;

namespace HmrcTpvsProxy.Domain.Test.Datasets
{
    [TestFixture]
    public class DatasetServiceTest
    {
        private Mock<IDatasetRepository> mockRepository;
        private Mock<IValidator> mockValidator;
        private Mock<ICsvParser> mockParser;
        private Mock<ICsvCreator> mockCreator;
        private DatasetService service;

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IDatasetRepository>();
            mockValidator = new Mock<IValidator>();
            mockParser = new Mock<ICsvParser>();
            mockCreator = new Mock<ICsvCreator>();

            service = new DatasetService(mockRepository.Object, mockValidator.Object, mockParser.Object, mockCreator.Object);
        }

        [Test]
        [TestCase(RequestType.AR)]
        [TestCase(RequestType.Authorisation)]
        [TestCase(RequestType.NOT)]
        [TestCase(RequestType.RTI)]
        [TestCase(RequestType.Unknown)]
        public void GivenIAmAskingForAnUnsupportedMessageType_WhenIGetMessages_ThenIShouldGetAnEmptyList(RequestType invalidType)
        {
            mockRepository.Setup(x => x.GetMessages(It.IsAny<int>(), It.IsAny<RequestType>())).Returns(GetPopulatedMessages(invalidType));

            var messages = service.GetMessages(1, invalidType);

            Assert.That(messages, Is.Empty);
        }

        [Test]
        [TestCase(RequestType.P6)]
        [TestCase(RequestType.P9)]
        [TestCase(RequestType.SL1)]
        [TestCase(RequestType.SL2)]
        public void GivenIAmAskingForASupportedMessageType_WhenIGetMessages_ThenIShouldGetAListOfMessages(RequestType validType)
        {
            mockRepository.Setup(x => x.GetMessages(It.IsAny<int>(), It.IsAny<RequestType>())).Returns(GetPopulatedMessages(validType));

            var messages = service.GetMessages(1, validType);

            Assert.That(messages, Is.Not.Empty);
            Assert.That(messages.All(x => x.FormType == validType.ToString()), Is.True);
        }

        private List<MessageDTO> GetPopulatedMessages(RequestType requestType)
        {
            return new List<MessageDTO>
            {
                new MessageDTO
                {
                    FormType = requestType.ToString(),
                    Forename = "John",
                    Surname = "Smith",
                    TaxCode = "1160L",
                    SequenceNumber = 1,
                    IssueDate = DateTime.Today,
                    EffectiveDate = DateTime.Today
                },
                new MessageDTO
                {
                    FormType = requestType.ToString(),
                    Forename = "John",
                    Surname = "Smith",
                    TaxCode = "1160L",
                    SequenceNumber = 1,
                    IssueDate = DateTime.Today,
                    EffectiveDate = DateTime.Today
                }
            };
        }
    }
}