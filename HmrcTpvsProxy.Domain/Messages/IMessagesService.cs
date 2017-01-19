namespace HmrcTpvsProxy.Domain.Messages
{
    public interface IMessagesService
    {
        string GetResponse(int datasourceId, string requestXml);
    }
}