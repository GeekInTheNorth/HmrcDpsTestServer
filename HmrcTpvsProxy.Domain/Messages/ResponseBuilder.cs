using System.Collections.Generic;
using System.Linq;
using HmrcTpvsProxy.Domain.Messages.Nodes;

namespace HmrcTpvsProxy.Domain.Messages
{
    public class ResponseBuilder : IResponseBuilder
    {
        private const int MaximumMessageCount = 20;

        public Envelope Build(RequestData requestData, IEnumerable<StudentLoanStart> notices)
        {
            var envelope = BuildEnvelope(requestData, notices);
            envelope.Body.DPSretrieveResponse.DPSdata.StudentLoanStart = notices.Where(x => x.SequenceNumber > requestData.LastSequenceNumberRecieved)
                                                                                .OrderBy(x => x.SequenceNumber)
                                                                                .Take(MaximumMessageCount)
                                                                                .ToList();

            return envelope;
        }

        public Envelope Build(RequestData requestData, IEnumerable<StudentLoanEnd> notices)
        {
            var envelope = BuildEnvelope(requestData, notices);
            envelope.Body.DPSretrieveResponse.DPSdata.StudentLoanEnd = notices.Where(x => x.SequenceNumber > requestData.LastSequenceNumberRecieved)
                                                                                .OrderBy(x => x.SequenceNumber)
                                                                                .Take(MaximumMessageCount)
                                                                                .ToList();

            return envelope;
        }

        public Envelope Build(RequestData requestData, IEnumerable<CodingNoticesP9> notices)
        {
            var envelope = BuildEnvelope(requestData, notices);
            envelope.Body.DPSretrieveResponse.DPSdata.CodingNoticesP9 = notices.Where(x => x.SequenceNumber > requestData.LastSequenceNumberRecieved)
                                                                                .OrderBy(x => x.SequenceNumber)
                                                                                .Take(MaximumMessageCount)
                                                                                .ToList();

            return envelope;
        }

        public Envelope Build(RequestData requestData, IEnumerable<CodingNoticesP6P6B> notices)
        {
            var envelope = BuildEnvelope(requestData, notices);
            envelope.Body.DPSretrieveResponse.DPSdata.CodingNoticesP6P6B = notices.Where(x => x.SequenceNumber > requestData.LastSequenceNumberRecieved)
                                                                                .OrderBy(x => x.SequenceNumber)
                                                                                .Take(MaximumMessageCount)
                                                                                .ToList();

            return envelope;
        }

        private Envelope BuildEnvelope(RequestData requestData, IEnumerable<INotice> notices)
        {
            var outstandingMessages = notices.Where(x => x.SequenceNumber > requestData.LastSequenceNumberRecieved).OrderBy(x => x.SequenceNumber);
            var messagesToSend = outstandingMessages.OrderBy(x => x.SequenceNumber).Take(MaximumMessageCount);

            var envelope = new Envelope();
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.DataType = requestData.RequestType.ToString();
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity = requestData.PayeReference;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Got = requestData.LastSequenceNumberRecieved;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.HighWaterMark = messagesToSend.Max(x => x.SequenceNumber);
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.MoreData = outstandingMessages.Count() > MaximumMessageCount;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.NItemsReturned = messagesToSend.Count();
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.VendorID = requestData.VendorId;

            return envelope;
        }
    }
}