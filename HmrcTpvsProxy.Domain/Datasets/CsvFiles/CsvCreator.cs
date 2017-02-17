using System.Collections.Generic;
using System.IO;
using System.Text;
using CsvHelper;

namespace HmrcTpvsProxy.Domain.Datasets.CsvFiles
{
    public class CsvCreator : ICsvCreator
    {
        public byte[] CreateCsvInMemory(IEnumerable<MessageDTO> messages, RequestType messageType)
        {
            var mapper = new MessageClassMap(messageType);

            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter))
            {
                csvWriter.Configuration.RegisterClassMap(mapper);
                csvWriter.Configuration.Encoding = Encoding.UTF8;
                csvWriter.WriteRecords(messages);

                streamWriter.Flush();

                return memoryStream.ToArray();
            }
        }
    }
}