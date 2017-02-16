using System.Collections.Generic;
using System.IO;

namespace HmrcTpvsProxy.Domain.Datasets.CsvParsing
{
    public interface ICsvParser
    {
        IEnumerable<MessageDTO> Parse(Stream csvStream, RequestType messageType);
    }
}