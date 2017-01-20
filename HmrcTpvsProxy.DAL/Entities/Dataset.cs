using System.Collections.Generic;

namespace HmrcTpvsProxy.DAL.Entities
{
    public class Dataset
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string PayeReference { get; set; }

        public virtual ICollection<CodingNotice> CodingNotices { get; set; }

        public virtual ICollection<StudentLoanNotice> StudentLoanNotices { get; set; }
    }
}