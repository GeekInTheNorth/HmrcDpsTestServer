using System;
using System.Collections.Generic;
using HmrcTpvsProxy.Domain.Messages.Nodes;

namespace HmrcTpvsProxy.Domain.Test.Messages
{
    public class TestDataBuilder
    {
        public IEnumerable<CodingNoticeP9> GetCodingNoticeP9(int numberOfNotices)
        {
            var notices = new List<CodingNoticeP9>();

            for (var loop = 1; loop <= numberOfNotices; loop++)
            {
                notices.Add(new CodingNoticeP9
                {
                    FormType = RequestType.P9.ToString(),
                    IssueDate = new DateTime(2016, 4, 1).AddDays(loop),
                    SequenceNumber = 229,
                    TaxYearEnd = 2015,
                    EmployerRef = "123/ABC",
                    Name = new Name
                    {
                        Forename = "Joe",
                        Surname = "Bloggs"
                    },
                    NINO = string.Format("AA{0}A", (100000 + loop)),
                    WorksNumber = loop.ToString(),
                    EffectiveDate = new DateTime(2016, 5, 1).AddDays(loop),
                    CodingUpdate = new CodingUpdate
                    {
                        TaxCode = new TaxCode
                        {
                            Value = "1100L"
                        }
                    }
                });
            }

            return notices;
        }

        public IEnumerable<CodingNoticesP6P6B> GetCodingNoticesP6(int numberOfNotices)
        {
            var notices = new List<CodingNoticesP6P6B>();

            for (var loop = 1; loop <= numberOfNotices; loop++)
            {
                notices.Add(new CodingNoticesP6P6B
                {
                    FormType = RequestType.P9.ToString(),
                    IssueDate = new DateTime(2016, 4, 1).AddDays(loop),
                    SequenceNumber = 229,
                    TaxYearEnd = 2015,
                    EmployerRef = "123/ABC",
                    Name = new Name
                    {
                        Forename = "Joe",
                        Surname = "Bloggs"
                    },
                    NINO = string.Format("AA{0}A", (100000 + loop)),
                    WorksNumber = loop.ToString(),
                    EffectiveDate = new DateTime(2016, 5, 1).AddDays(loop),
                    CodingUpdate = new CodingUpdate
                    {
                        TaxCode = new TaxCode
                        {
                            Value = "1100L"
                        }
                    }
                });
            }

            return notices;
        }

        public IEnumerable<StudentLoanStart> GetStudentLoanStartNotices(int numberOfNotices)
        {
            var notices = new List<StudentLoanStart>();

            for (var loop = 1; loop <= numberOfNotices; loop++)
            {
                notices.Add(new StudentLoanStart
                {
                    IssueDate = new DateTime(2016, 4, 1).AddDays(loop),
                    SequenceNumber = 229,
                    TaxYearEnd = 2015,
                    EmployerRef = "123/ABC",
                    Name = new Name
                    {
                        Forename = "Joe",
                        Surname = "Bloggs"
                    },
                    NINO = string.Format("AA{0}A", (100000 + loop)),
                    WorksNumber = loop.ToString(),
                    EffectiveDate = new DateTime(2016, 5, 1).AddDays(loop)
                });
            }

            return notices;
        }

        public IEnumerable<StudentLoanEnd> GetStudentLoanEndNotices(int numberOfNotices)
        {
            var notices = new List<StudentLoanEnd>();

            for (var loop = 1; loop <= numberOfNotices; loop++)
            {
                notices.Add(new StudentLoanEnd
                {
                    IssueDate = new DateTime(2016, 4, 1).AddDays(loop),
                    SequenceNumber = 229,
                    TaxYearEnd = 2015,
                    EmployerRef = "123/ABC",
                    Name = new Name
                    {
                        Forename = "Joe",
                        Surname = "Bloggs"
                    },
                    NINO = string.Format("AA{0}A", (100000 + loop)),
                    WorksNumber = loop.ToString(),
                    EffectiveDate = new DateTime(2016, 5, 1).AddDays(loop)
                });
            }

            return notices;
        }
    }
}