using System.Collections.Generic;
using HmrcTpvsProxy.Domain.Messages.Nodes;

namespace HmrcTpvsProxy.Domain.Messages
{
    public interface IMessagesRepository
    {
        IEnumerable<CodingNoticesP6P6B> GetP6CodingNotices(int datasourceId);

        IEnumerable<CodingNoticeP9> GetP9CodingNotices(int datasourceId);

        IEnumerable<StudentLoanStart> GetStudentLoanStartNotices(int datasourceId);

        IEnumerable<StudentLoanEnd> GetStudentLoanEndNotices(int datasourceId);
    }
}