using System.Linq;
using System.Collections.Generic;
using HmrcTpvsProxy.Domain.Datasets;
using System;
using HmrcTpvsProxy.Domain.Datasets.CsvParsing;
using HmrcTpvsProxy.Domain;

namespace HmrcTpvsProxy.DAL.Repositories
{
    public class DatasetRepository : BaseRepository, IDatasetRepository
    {
        private class MessageCount
        {
            public int Id { get; set; }

            public string Type { get; set; }

            public int Count { get; set; }
        }

        public bool Create(string description, string payeReference)
        {
            if (context.Datasets.Any(x => x.Name.Equals(description, StringComparison.CurrentCultureIgnoreCase)))
                return false;

            context.Datasets.Add(new Entities.Dataset
            {
                Name = description,
                PayeReference = payeReference
            });

            context.SaveChanges();

            return true;
        }

        public IEnumerable<DatasetSummary> GetDatasetSummaries()
        {
            var datasets = context.Datasets.ToList();

            var codingNoticeCounts = (from codingNotice in context.CodingNotices
                                      group codingNotice by new { codingNotice.DatasetID, codingNotice.MessageType } into groupedMessages
                                      select new MessageCount
                                      {
                                          Id = groupedMessages.Key.DatasetID,
                                          Type = groupedMessages.Key.MessageType,
                                          Count = groupedMessages.Count()
                                      }).ToList();

            var studentLoanCounts = (from studentLoan in context.StudentLoanNotices
                                     group studentLoan by new { studentLoan.DatasetID, studentLoan.MessageType } into groupedMessages
                                     select new MessageCount
                                     {
                                         Id = groupedMessages.Key.DatasetID,
                                         Type = groupedMessages.Key.MessageType,
                                         Count = groupedMessages.Count()
                                     }).ToList();

            var unifiedMessageCounts = codingNoticeCounts.Union(studentLoanCounts);

            var summaries = new List<DatasetSummary>();

            foreach(var dataset in datasets)
            {
                var messageCounts = unifiedMessageCounts.Where(x => x.Id == dataset.ID).ToDictionary(x => x.Type, x=> x.Count);

                summaries.Add(new DatasetSummary(dataset.ID, dataset.Name, messageCounts));
            }

            return summaries;
        }

        public bool Save(int datasetId, RequestType messageType, IEnumerable<MessageDTO> messages)
        {
            switch (messageType)
            {
                case RequestType.P9:
                case RequestType.P6:
                    return SaveCodingNotice(datasetId, messages);
                case RequestType.SL1:
                case RequestType.SL2:
                    return SaveStudentLoanNotice(datasetId, messages);
                default:
                    return false;
            }
        }

        private bool SaveCodingNotice(int datasetId, IEnumerable<MessageDTO> messages)
        {
            var datasetExists = context.Datasets.Any(x => x.ID == datasetId);

            if (!datasetExists) return false;

            foreach(var message in messages)
            {
                var notice = context.CodingNotices.FirstOrDefault(x => x.DatasetID == datasetId && x.SequenceNo == message.SequenceNumber);

                if (notice == null)
                {
                    notice = new Entities.CodingNotice();
                    context.CodingNotices.Add(notice);
                }

                notice.DatasetID = datasetId;
                notice.EffectiveDate = message.EffectiveDate;
                notice.Forename = message.Forename;
                notice.GrossTaxableInPreviousEmployment = message.GrossTaxable;
                notice.IssueDate = message.IssueDate;
                notice.MessageType = message.FormType;
                notice.NationalInsuranceNo = message.NINO;
                notice.SequenceNo = message.SequenceNumber;
                notice.Surname = message.Surname;
                notice.TaxBasisNonCumulative = message.TaxBasisNonCumulative;
                notice.TaxCode = message.TaxCode;
                notice.TaxPaidInPreviousEmployment = message.TaxPaid;
                notice.TaxRegime = message.TaxRegime;
                notice.TaxYear = message.TaxYearEnd;
                notice.WorksNumber = message.PayId;
            }

            try
            {
                context.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private bool SaveStudentLoanNotice(int datasetId, IEnumerable<MessageDTO> messages)
        {
            var datasetExists = context.Datasets.Any(x => x.ID == datasetId);

            if (!datasetExists) return false;

            foreach (var message in messages)
            {
                var notice = context.StudentLoanNotices.FirstOrDefault(x => x.DatasetID == datasetId && x.SequenceNo == message.SequenceNumber);

                if (notice == null)
                {
                    notice = new Entities.StudentLoanNotice();
                    context.StudentLoanNotices.Add(notice);
                }

                notice.DatasetID = datasetId;
                notice.EffectiveDate = message.EffectiveDate;
                notice.Forename = message.Forename;
                notice.IssueDate = message.IssueDate;
                notice.MessageType = message.FormType;
                notice.NationalInsuranceNo = message.NINO;
                notice.PlanType = message.PlanType;
                notice.SequenceNo = message.SequenceNumber;
                notice.Surname = message.Surname;
                notice.TaxYear = message.TaxYearEnd;
                notice.WorksNumber = message.PayId;
            }

            try
            {
                context.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
    }
}