using System.Collections.Generic;
using System.IO;

namespace HmrcTpvsProxy.Domain.Datasets.CsvFiles
{
    public interface ICsvParser
    {
        IEnumerable<MessageDTO> Parse(Stream csvStream, RequestType messageType);
    }
}