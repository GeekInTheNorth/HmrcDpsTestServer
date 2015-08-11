namespace HmrcTpvsProxy.Domain
{
    public interface IMessageSender
    {
        PostResult PostXml(string xml, string destinationUrl);
    }
}