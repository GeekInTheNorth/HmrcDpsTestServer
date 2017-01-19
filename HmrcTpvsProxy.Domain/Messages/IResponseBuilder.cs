using System.Collections.Generic;
using HmrcTpvsProxy.Domain.Messages.Nodes;

namespace HmrcTpvsProxy.Domain.Messages
{
    public interface IResponseBuilder
    {
        Envelope Build(RequestData requestData, IEnumerable<CodingNoticesP6P6B> p6Notices);

        Envelope Build(RequestData requestData, IEnumerable<CodingNoticesP9> p9Notices);

        Envelope Build(RequestData requestData, IEnumerable<StudentLoanStart> sl1Notices);

        Envelope Build(RequestData requestData, IEnumerable<StudentLoanEnd> sl2Notices);
    }
}