using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmrcTpvsProxy.Domain.Messages
{
    public class RequestData
    {
        public RequestType RequestType { get; set; }

        public string PayeReference { get; set; }

        public string VendorId { get; set; }

        public int LastSequenceNumberRecieved { get; set; }

        public RequestData()
        {
            RequestType = RequestType.Unknown;
        }
    }
}