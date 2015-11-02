using System.Collections.Generic;
using System.Xml;
using HmrcTpvsProxy.Domain.Models;

namespace HmrcTpvsProxy.Domain.TestDataTransformer
{
    public interface IStudentLoanNoticeTransformer
    {
        IEnumerable<StudentLoanNotice> Transform(XmlDocument studentLoanMessage);
    }
}
