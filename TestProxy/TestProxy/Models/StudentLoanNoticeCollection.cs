using System.Collections.Generic;
using HmrcTpvsProxy.Domain.Models;

namespace TestProxy.Models
{
    public class StudentLoanNoticeCollection
    {
        public List<StudentLoanNotice> Notices { get; set; }

        public StudentLoanNoticeCollection()
        {
            Notices = new List<StudentLoanNotice>();
        }
    }
}
