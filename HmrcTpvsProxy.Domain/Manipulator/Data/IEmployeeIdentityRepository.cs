using System.Collections.Generic;

namespace HmrcTpvsProxy.Domain.Manipulator.Data
{
    public interface IEmployeeIdentityRepository
    {
        IEnumerable<EmployeeIdentity> Get();
    }
}