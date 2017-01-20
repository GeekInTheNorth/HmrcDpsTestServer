using System.Linq;
using System.Collections.Generic;
using HmrcTpvsProxy.Domain.Datasets;

namespace HmrcTpvsProxy.DAL.Repositories
{
    public class DatasetRepository : BaseRepository, IDatasetRepository
    {
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

            var studentLoanCounts = (from studentLoan in context.StudentLoanNotice
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

        private class MessageCount
        {
            public int Id { get; set; }

            public string Type { get; set; }

            public int Count { get; set; }
        }
    }
}