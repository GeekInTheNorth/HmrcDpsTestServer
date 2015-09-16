namespace HmrcTpvsProxy.Domain.Manipulator.Data
{
    public class EmployeeIdentity
    {
        public int EmployeePayId { get; set; }

        public string NationalInsuranceNo { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string Title { get; set; }

        public EmployeeIdentity(int payId, string nino, string forename, string surname, string title)
        {
            EmployeePayId = payId;
            NationalInsuranceNo = nino;
            Forename = forename;
            Surname = surname;
        }
    }
}