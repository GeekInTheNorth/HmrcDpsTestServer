using System;

namespace HmrcTpvsProxy.Domain.Messages.Nodes
{
    [Serializable]
    public class Name
    {
        public string Forename { get; set; }

        public string Surname { get; set; }
    }
}