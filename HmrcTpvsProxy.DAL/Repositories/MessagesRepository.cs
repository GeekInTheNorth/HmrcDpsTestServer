using System.Collections.Generic;
using System.Linq;
using HmrcTpvsProxy.Domain.Messages;
using HmrcTpvsProxy.Domain.Messages.Nodes;

namespace HmrcTpvsProxy.DAL.Repositories
{
    public class MessagesRepository : BaseRepository, IMessagesRepository
    {
        public IEnumerable<CodingNoticesP6P6B> GetP6CodingNotices(int datasourceId)
        {
            return (from notice in context.CodingNotices
                    join dataset in context.Datasets on notice.DatasetID equals dataset.ID
                    where notice.DatasetID == datasourceId && notice.MessageType.StartsWith("P6")
                    select new CodingNoticesP6P6B
                    {
                        SequenceNumber = notice.SequenceNo,
                        WorksNumber = notice.WorksNumber,
                        FormType = notice.MessageType,
                        EffectiveDate = notice.EffectiveDate,
                        EmployerRef = dataset.PayeReference,
                        IssueDate = notice.IssueDate,
                        NINO = notice.NationalInsuranceNo,
                        TaxYearEnd = notice.TaxYear,
                        Name = new Name
                        {
                            Forename = notice.Forename,
                            Surname = notice.Surname
                        },
                        CodingUpdate = new CodingUpdate
                        {
                            TaxCode = new TaxCode
                            {
                                Value = notice.TaxCode,
                                TaxRegime = notice.TaxRegime,
                                Week1Month1Indicator = notice.TaxBasisNonCumulative
                            },
                            TotalPreviousPay = notice.GrossTaxableInPreviousEmployment,
                            TotalPreviousTax = notice.TaxPaidInPreviousEmployment
                        }
                    }).AsEnumerable();
        }

        public IEnumerable<CodingNoticesP9> GetP9CodingNotices(int datasourceId)
        {
            return (from notice in context.CodingNotices
                    join dataset in context.Datasets on notice.DatasetID equals dataset.ID
                    where notice.DatasetID == datasourceId && notice.MessageType.StartsWith("P9")
                    select new CodingNoticesP9
                    {
                        SequenceNumber = notice.SequenceNo,
                        WorksNumber = notice.WorksNumber,
                        FormType = notice.MessageType,
                        EffectiveDate = notice.EffectiveDate,
                        EmployerRef = dataset.PayeReference,
                        IssueDate = notice.IssueDate,
                        NINO = notice.NationalInsuranceNo,
                        TaxYearEnd = notice.TaxYear,
                        Name = new Name
                        {
                            Forename = notice.Forename,
                            Surname = notice.Surname
                        },
                        CodingUpdate = new CodingUpdate
                        {
                            TaxCode = new TaxCode
                            {
                                Value = notice.TaxCode,
                                TaxRegime = notice.TaxRegime,
                                Week1Month1Indicator = notice.TaxBasisNonCumulative
                            },
                            TotalPreviousPay = notice.GrossTaxableInPreviousEmployment,
                            TotalPreviousTax = notice.TaxPaidInPreviousEmployment
                        }
                    }).AsEnumerable();
        }

        public IEnumerable<StudentLoanEnd> GetStudentLoanEndNotices(int datasourceId)
        {
            return (from notice in context.StudentLoanNotice
                    join dataset in context.Datasets on notice.DatasetID equals dataset.ID
                    where notice.DatasetID == datasourceId && notice.MessageType == "SL2"
                    select new StudentLoanEnd
                    {
                        SequenceNumber = notice.SequenceNo,
                        WorksNumber = notice.WorksNumber,
                        EffectiveDate = notice.EffectiveDate,
                        EmployerRef = dataset.PayeReference,
                        IssueDate = notice.IssueDate,
                        NINO = notice.NationalInsuranceNo,
                        TaxYearEnd = notice.TaxYear,
                        Name = new Name
                        {
                            Forename = notice.Forename,
                            Surname = notice.Surname
                        }
                    }).AsEnumerable();
        }

        public IEnumerable<StudentLoanStart> GetStudentLoanStartNotices(int datasourceId)
        {
            return (from notice in context.StudentLoanNotice
                    join dataset in context.Datasets on notice.DatasetID equals dataset.ID
                    where notice.DatasetID == datasourceId && notice.MessageType == "SL1"
                    select new StudentLoanStart
                    {
                        SequenceNumber = notice.SequenceNo,
                        WorksNumber = notice.WorksNumber,
                        EffectiveDate = notice.EffectiveDate,
                        EmployerRef = dataset.PayeReference,
                        IssueDate = notice.IssueDate,
                        NINO = notice.NationalInsuranceNo,
                        TaxYearEnd = notice.TaxYear,
                        Name = new Name
                        {
                            Forename = notice.Forename,
                            Surname = notice.Surname
                        },
                        PlanType = notice.PlanType
                    }).AsEnumerable();
        }
    }
}