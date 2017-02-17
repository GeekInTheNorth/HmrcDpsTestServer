using System.Collections.Generic;

namespace HmrcTpvsProxy.Domain.Datasets.CsvFiles
{
    public interface ICsvCreator
    {
        byte[] CreateCsvInMemory(IEnumerable<MessageDTO> messages, RequestType messageType);
    }
}