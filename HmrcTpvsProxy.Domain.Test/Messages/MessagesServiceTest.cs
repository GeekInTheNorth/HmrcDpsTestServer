using System;
using System.Collections.Generic;
using System.Xml;
using HmrcTpvsProxy.Domain.Messages;
using HmrcTpvsProxy.Domain.Messages.Nodes;
using HmrcTpvsProxy.Domain.Messages.Serialization;
using Moq;
using NUnit.Framework;

namespace HmrcTpvsProxy.Domain.Test.Messages
{
    [TestFixture]
    public class MessagesServiceTest
    {
        private IResponseFileRetriever fileRetriever;
        private IMessagesService service;
        private TestDataBuilder testData;
        private Mock<IRequestDataResolver> mockDataResolver;
        private Mock<IMessagesRepository> mockRepository;
        private Mock<IResponseBuilder> mockResponseBuilder;
        private Mock<ISerializer> mockSerializer;

        [SetUp]
        public void SetUp()
        {
            mockRepository = new Mock<IMessagesRepository>();
            mockDataResolver = new Mock<IRequestDataResolver>();
            mockResponseBuilder = new Mock<IResponseBuilder>();
            mockSerializer = new Mock<ISerializer>();

            service = new MessagesService(mockRepository.Object, mockDataResolver.Object, mockResponseBuilder.Object, mockSerializer.Object);
            fileRetriever = new ResponseFileRetriever();
            testData = new TestDataBuilder();
        }

        [Test]
        public void GivenIAnInvalidDatasourceId_WhenIAskForAResponse_ThenIShouldGetAnException()
        {
            Assert.Throws<ArgumentException>(() => service.GetResponse(-1, TestRequests.Authorisation));
        }

        [Test]
        public void GivenIDoNotHaveARequestXml_WhenIAskForAResponse_ThenIShouldGetAnException()
        {
            Assert.Throws<ArgumentException>(() => service.GetResponse(1, string.Empty));
        }

        [Test]
        public void GivenIHaveAnAuthorisationRequest_WhenIAskForAResponse_ThenIShouldGetTheDefaultAuthorisationResponse()
        {
            mockDataResolver.Setup(x => x.Get(It.IsAny<XmlDocument>())).Returns(new RequestData { RequestType = RequestType.Authorisation });

            var response = service.GetResponse(1, TestRequests.Authorisation);

            Assert.That(response, Is.EqualTo(fileRetriever.GetResponseAsString(RequestType.Authorisation)));
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        public void GivenIHaveARequestForP9CodingNotices_WhenIAskForAResponse_ThenP9CodingNoticesShouldBeLoadedFromTheRepositoryForTheRightDataset(int datasetId)
        {
            mockDataResolver.Setup(x => x.Get(It.IsAny<XmlDocument>())).Returns(new RequestData { RequestType = RequestType.P9 });

            var response = service.GetResponse(datasetId, TestRequests.P9Request);

            mockRepository.Verify(x => x.GetP9CodingNotices(datasetId), Times.Once());
            mockRepository.Verify(x => x.GetP9CodingNotices(It.IsAny<int>()), Times.Once());
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        public void GivenIHaveARequestForP6CodingNotices_WhenIAskForAResponse_ThenP6CodingNoticesShouldBeLoadedFromTheRepositoryForTheRightDataset(int datasetId)
        {
            mockDataResolver.Setup(x => x.Get(It.IsAny<XmlDocument>())).Returns(new RequestData { RequestType = RequestType.P6 });

            var response = service.GetResponse(datasetId, TestRequests.P6Request);

            mockRepository.Verify(x => x.GetP6CodingNotices(datasetId), Times.Once());
            mockRepository.Verify(x => x.GetP6CodingNotices(It.IsAny<int>()), Times.Once());
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        public void GivenIHaveARequestForStudentLoanStartNotices_WhenIAskForAResponse_ThenStudentLoanStartNoticesShouldBeLoadedFromTheRepositoryForTheRightDataset(int datasetId)
        {
            mockDataResolver.Setup(x => x.Get(It.IsAny<XmlDocument>())).Returns(new RequestData { RequestType = RequestType.SL1 });

            var response = service.GetResponse(datasetId, TestRequests.SL1Request);

            mockRepository.Verify(x => x.GetStudentLoanStartNotices(datasetId), Times.Once());
            mockRepository.Verify(x => x.GetStudentLoanStartNotices(It.IsAny<int>()), Times.Once());
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        public void GivenIHaveARequestForStudentLoanEndNotices_WhenIAskForAResponse_ThenStudentLoanEndNoticesShouldBeLoadedFromTheRepositoryForTheRightDataset(int datasetId)
        {
            mockDataResolver.Setup(x => x.Get(It.IsAny<XmlDocument>())).Returns(new RequestData { RequestType = RequestType.SL2 });

            var response = service.GetResponse(datasetId, TestRequests.SL2Request);

            mockRepository.Verify(x => x.GetStudentLoanEndNotices(datasetId), Times.Once());
            mockRepository.Verify(x => x.GetStudentLoanEndNotices(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void GivenIHaveARequestForP9CodingNotices_WhenIAskForAResponse_ThenTheResponseBuilderShouldBuildAResponse()
        {
            mockDataResolver.Setup(x => x.Get(It.IsAny<XmlDocument>())).Returns(new RequestData { RequestType = RequestType.P9 });
            mockRepository.Setup(x => x.GetP9CodingNotices(It.IsAny<int>())).Returns(testData.GetCodingNoticeP9(3));

            var response = service.GetResponse(1, TestRequests.P9Request);

            mockResponseBuilder.Verify(x => x.Build(It.IsAny<RequestData>(), It.IsAny<IEnumerable<CodingNoticeP9>>()), Times.Once());
        }

        [Test]
        public void GivenIHaveARequestForP6CodingNotices_WhenIAskForAResponse_ThenTheResponseBuilderShouldBuildAResponse()
        {
            mockDataResolver.Setup(x => x.Get(It.IsAny<XmlDocument>())).Returns(new RequestData { RequestType = RequestType.P6 });
            mockRepository.Setup(x => x.GetP6CodingNotices(It.IsAny<int>())).Returns(testData.GetCodingNoticesP6(3));

            var response = service.GetResponse(1, TestRequests.P6Request);

            mockResponseBuilder.Verify(x => x.Build(It.IsAny<RequestData>(), It.IsAny<IEnumerable<CodingNoticesP6P6B>>()), Times.Once());
        }

        [Test]
        public void GivenIHaveARequestForStudentLoanStartNotices_WhenIAskForAResponse_ThenTheResponseBuilderShouldBuildAResponse()
        {
            mockDataResolver.Setup(x => x.Get(It.IsAny<XmlDocument>())).Returns(new RequestData { RequestType = RequestType.SL1 });
            mockRepository.Setup(x => x.GetStudentLoanStartNotices(It.IsAny<int>())).Returns(testData.GetStudentLoanStartNotices(3));

            var response = service.GetResponse(1, TestRequests.SL1Request);

            mockResponseBuilder.Verify(x => x.Build(It.IsAny<RequestData>(), It.IsAny<IEnumerable<StudentLoanStart>>()), Times.Once());
        }

        [Test]
        public void GivenIHaveARequestForStudentLoanEndNotices_WhenIAskForAResponse_ThenTheResponseBuilderShouldBuildAResponse()
        {
            mockDataResolver.Setup(x => x.Get(It.IsAny<XmlDocument>())).Returns(new RequestData { RequestType = RequestType.SL2 });
            mockRepository.Setup(x => x.GetStudentLoanEndNotices(It.IsAny<int>())).Returns(testData.GetStudentLoanEndNotices(3));

            var response = service.GetResponse(1, TestRequests.SL2Request);

            mockResponseBuilder.Verify(x => x.Build(It.IsAny<RequestData>(), It.IsAny<IEnumerable<StudentLoanEnd>>()), Times.Once());
        }

        [Test]
        public void GivenIHaveARequestForP6CodingNotices_WhenIAskForAResponse_ThenSerializerShouldSerializeTheXmlDocument()
        {
            mockDataResolver.Setup(x => x.Get(It.IsAny<XmlDocument>())).Returns(new RequestData { RequestType = RequestType.P6 });
            mockRepository.Setup(x => x.GetP6CodingNotices(It.IsAny<int>())).Returns(testData.GetCodingNoticesP6(3));
            mockResponseBuilder.Setup(x => x.Build(It.IsAny<RequestData>(), It.IsAny<IEnumerable<CodingNoticesP6P6B>>())).Returns(new Envelope());

            var response = service.GetResponse(1, TestRequests.P6Request);

            mockSerializer.Verify(x => x.Serialize(It.IsAny<Envelope>()), Times.Once());
        }

        [Test]
        public void GivenIHaveARequestForP9CodingNotices_WhenIAskForAResponse_ThenSerializerShouldSerializeTheXmlDocument()
        {
            mockDataResolver.Setup(x => x.Get(It.IsAny<XmlDocument>())).Returns(new RequestData { RequestType = RequestType.P9 });
            mockRepository.Setup(x => x.GetP9CodingNotices(It.IsAny<int>())).Returns(testData.GetCodingNoticeP9(3));
            mockResponseBuilder.Setup(x => x.Build(It.IsAny<RequestData>(), It.IsAny<IEnumerable<CodingNoticeP9>>())).Returns(new Envelope());

            var response = service.GetResponse(1, TestRequests.P9Request);

            mockSerializer.Verify(x => x.Serialize(It.IsAny<Envelope>()), Times.Once());
        }

        [Test]
        public void GivenIHaveARequestForStudentLoanStartNotices_WhenIAskForAResponse_ThenSerializerShouldSerializeTheXmlDocument()
        {
            mockDataResolver.Setup(x => x.Get(It.IsAny<XmlDocument>())).Returns(new RequestData { RequestType = RequestType.SL1 });
            mockRepository.Setup(x => x.GetStudentLoanStartNotices(It.IsAny<int>())).Returns(testData.GetStudentLoanStartNotices(3));
            mockResponseBuilder.Setup(x => x.Build(It.IsAny<RequestData>(), It.IsAny<IEnumerable<StudentLoanStart>>())).Returns(new Envelope());

            var response = service.GetResponse(1, TestRequests.SL1Request);

            mockSerializer.Verify(x => x.Serialize(It.IsAny<Envelope>()), Times.Once());
        }

        [Test]
        public void GivenIHaveARequestForStudentLoanEndNotices_WhenIAskForAResponse_ThenSerializerShouldSerializeTheXmlDocument()
        {
            mockDataResolver.Setup(x => x.Get(It.IsAny<XmlDocument>())).Returns(new RequestData { RequestType = RequestType.SL2 });
            mockRepository.Setup(x => x.GetStudentLoanEndNotices(It.IsAny<int>())).Returns(testData.GetStudentLoanEndNotices(3));
            mockResponseBuilder.Setup(x => x.Build(It.IsAny<RequestData>(), It.IsAny<IEnumerable<StudentLoanEnd>>())).Returns(new Envelope());

            var response = service.GetResponse(1, TestRequests.SL2Request);

            mockSerializer.Verify(x => x.Serialize(It.IsAny<Envelope>()), Times.Once());
        }
    }
}