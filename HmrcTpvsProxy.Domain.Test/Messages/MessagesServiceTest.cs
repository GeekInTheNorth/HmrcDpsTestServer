using System;
using HmrcTpvsProxy.Domain.Messages;
using Moq;
using NUnit.Framework;

namespace HmrcTpvsProxy.Domain.Test.Messages
{
    [TestFixture]
    public class MessagesServiceTest
    {
        private MessagesService service;
        private Mock<IRequestDataResolver> mockDataResolver;
        private Mock<IMessagesRepository> mockRepository;

        [SetUp]
        public void SetUp()
        {
            mockRepository = new Mock<IMessagesRepository>();
            mockDataResolver = new Mock<IRequestDataResolver>();

            service = new MessagesService(mockRepository.Object, mockDataResolver.Object);
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
    }
}