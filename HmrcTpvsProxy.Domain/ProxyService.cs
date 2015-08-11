using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using HmrcTpvsProxy.Domain.Manipulator;
using HmrcTpvsProxy.Domain.ConfigurationData;

namespace HmrcTpvsProxy.Domain
{
    public class ProxyService : IProxyService
    {
        private readonly IHmrcDataManipulator dataManipulator;
        private readonly IConfigurationRepository configRepository;
        private readonly IMessageSender messageSender;

        public ProxyService(IHmrcDataManipulator dataManipulator, IConfigurationRepository configRepository, IMessageSender messageSender)
        {
            if (dataManipulator == null) throw new ArgumentNullException(nameof(dataManipulator));
            if (configRepository == null) throw new ArgumentNullException(nameof(configRepository));
            if (messageSender == null) throw new ArgumentNullException(nameof(messageSender));

            this.dataManipulator = dataManipulator;
            this.configRepository = configRepository;
            this.messageSender = messageSender;
        }

        public HttpResponseMessage GetAuthorisationResponseFor(HttpRequestMessage request)
        {
            var requestContent = request.Content.ReadAsStringAsync().Result;
            var configuration = configRepository.GetConfiguration();

            var result = messageSender.PostXml(requestContent, configuration.HmrcAuthenticationServer);

            return new HttpResponseMessage
            {
                Content = new StringContent(result.Response, Encoding.UTF8)
            };
        }

        public HttpResponseMessage GetMessageResponseFor(HttpRequestMessage request)
        {
            var requestContent = request.Content.ReadAsStringAsync().Result;
            var configuration = configRepository.GetConfiguration();

            var result = messageSender.PostXml(requestContent, configuration.HmrcGetMessagesServer);

            var response = result.Response;
            if (configuration.OverrideIdentities)
                response = dataManipulator.ApplyEmployeeIdentities(response);

            return new HttpResponseMessage
            {
                Content = new StringContent(response, Encoding.UTF8)
            };
        }        
    }
}
