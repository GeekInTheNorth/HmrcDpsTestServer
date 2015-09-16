using HmrcTpvsProxy.Domain.Manipulator.Data;
using System.Collections.Generic;

namespace HmrcTpvsProxy.Domain.Models
{
    public class IdentityCache
    {
        public Dictionary<string, EmployeeIdentity> Identities { get; set; }

        public IdentityCache()
        {
            Identities = new Dictionary<string, EmployeeIdentity>();
        }
    }
}
