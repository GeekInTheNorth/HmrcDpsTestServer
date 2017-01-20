using System.Collections.Generic;

namespace HmrcTpvsProxy.Domain.Datasets
{
    public class DatasetSummary
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public Dictionary<string, int> MessageCounts { get; private set; }

        public DatasetSummary(int id, string name, Dictionary<string, int> messageCounts)
        {
            Id = id;
            Name = name;
            MessageCounts = messageCounts ?? new Dictionary<string, int>();
        }
    }
}