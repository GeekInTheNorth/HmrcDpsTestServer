using System;
using System.IO;
using System.Net;

namespace HmrcTpvsProxy.Domain
{
    public class MessageSender : IMessageSender
    {
        public PostResult PostXml(string xml, string destinationUrl)
        {
            var result = new PostResult
            {
                WasSuccessful = true,
                Response = string.Empty
            };

            if (string.IsNullOrEmpty(destinationUrl))
                throw new ArgumentException("destination must be specified.");

            // The request object is the HTTP packet we are building up to send.
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);

            // If we need to use a proxy then use these settings before we send

            string strSOAPRequestBody = xml;

            // Build up the request object, inserting our XML. Any exceptions are returned as the response
            try
            {
                request.Method = "POST";
                request.ContentType = "application/soap+xml; charset=utf-8";
                request.ContentLength = strSOAPRequestBody.Length;
                request.Timeout = 5000;
                request.KeepAlive = false;
                request.ConnectionGroupName = Guid.NewGuid().ToString();

                // Send the daat
                StreamWriter streamWriter = new StreamWriter(request.GetRequestStream());
                streamWriter.Write(strSOAPRequestBody);
                streamWriter.Close();

                // read the response
                StreamReader streamReader = new StreamReader(request.GetResponse().GetResponseStream());
                while (!streamReader.EndOfStream)
                    result.Response += streamReader.ReadLine();
                streamReader.Close();
            }
            catch (WebException ex)
            {
                result.WasSuccessful = false;
                try
                {
                    StreamReader streamReader2 = new StreamReader(ex.Response.GetResponseStream());
                    while (!streamReader2.EndOfStream)
                        result.Response += streamReader2.ReadLine();
                    streamReader2.Close();
                }
                catch
                {
                    result.Response = ex.Message;
                }
            }
            catch (Exception e)
            {
                result.WasSuccessful = false;
                result.Response = e.ToString();
            }

            return result;
        }
    }
}