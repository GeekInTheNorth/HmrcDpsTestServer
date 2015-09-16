using HmrcTpvsProxy.Domain.Models;

namespace HmrcTpvsProxy.Domain.Manipulator
{
    public interface IHmrcDataManipulator
    {
        string ApplyEmployeeIdentities(string response, IdentityCache identityCache);
    }
}