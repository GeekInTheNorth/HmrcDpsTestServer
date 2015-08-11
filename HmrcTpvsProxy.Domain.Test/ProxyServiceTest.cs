using System.Net.Http;
using HmrcTpvsProxy.Domain.ConfigurationData;
using HmrcTpvsProxy.Domain.Manipulator;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;

namespace HmrcTpvsProxy.Domain.Test
{
    [TestFixture]
    public class ProxyServiceTest
    {
        private Mock<IHmrcDataManipulator> dataManipulator;
        private Mock<IConfigurationRepository> configRepository;
        private Mock<IMessageSender> messageSender;

        private ProxyService proxyService;

        [SetUp]
        public void SetUp()
        {
            var mocker = new AutoMocker();
            dataManipulator = mocker.GetMock<IHmrcDataManipulator>();
            configRepository = mocker.GetMock<IConfigurationRepository>();
            messageSender = mocker.GetMock<IMessageSender>();

            messageSender.Setup(x => x.PostXml(It.IsAny<string>(), It.IsAny<string>())).Returns(GetPostResult());

            proxyService = mocker.CreateInstance<ProxyService>();
        }

        [Test]
        public void WhenConfiguredToOverrideIdentities_ThenTheDataManipulatorWillOverrideIdentities()
        {
            configRepository.Setup(x => x.GetConfiguration()).Returns(new Configuration { OverrideIdentities = true });
            dataManipulator.Setup(x => x.ApplyEmployeeIdentities(It.IsAny<string>())).Returns(GetPostResult().Response);

            proxyService.GetMessageResponseFor(GetTestMessage());

            dataManipulator.Verify(x => x.ApplyEmployeeIdentities(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void WhenNotConfiguredToOverrideIdentities_ThenTheDataManipulatorWillNotOverrideIdentities()
        {
            configRepository.Setup(x => x.GetConfiguration()).Returns(new Configuration { OverrideIdentities = false });

            proxyService.GetMessageResponseFor(GetTestMessage());

            dataManipulator.Verify(x => x.ApplyEmployeeIdentities(It.IsAny<string>()), Times.Never());
        }

        private HttpRequestMessage GetTestMessage()
        {
            var content = TestRequests.P6Request;

            return new HttpRequestMessage
            {
                Content = new StringContent(content)
            };
        }

        private PostResult GetPostResult()
        {
            var content = TestResponses.P6Response;

            return new PostResult
            {
                Response = content,
                WasSuccessful = true
            };
        }
    }
}