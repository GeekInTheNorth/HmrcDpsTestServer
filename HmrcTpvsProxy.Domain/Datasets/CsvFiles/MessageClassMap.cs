using CsvHelper.Configuration;

namespace HmrcTpvsProxy.Domain.Datasets.CsvFiles
{
    public class MessageClassMap : CsvClassMap<MessageDTO>
    {
        public MessageClassMap(RequestType messageType)
        {
            var messageColumns = new MessageColumns();
            Map(x => x.FormType).Name(messageColumns.FormType);
            Map(x => x.Forename).Name(messageColumns.Forename);
            Map(x => x.Surname).Name(messageColumns.Surname);
            Map(x => x.NINO).Name(messageColumns.NationalInsuranceNo);
            Map(x => x.PayId).Name(messageColumns.PayId);
            Map(x => x.TaxYearEnd).Name(messageColumns.TaxYearEnd);
            Map(x => x.IssueDate).Name(messageColumns.IssueDate);
            Map(x => x.EffectiveDate).Name(messageColumns.EffectiveDate);
            Map(x => x.SequenceNumber).Name(messageColumns.SequenceNumber);

            if (messageType == RequestType.P6 || messageType == RequestType.P9)
            {
                Map(x => x.TaxCode).Name(messageColumns.TaxCode);
                Map(x => x.TaxRegime).Name(messageColumns.TaxRegime);
                Map(x => x.TaxBasisNonCumulative).Name(messageColumns.TaxBasisNonCumulative);
            }

            if (messageType == RequestType.P9)
            {
                Map(x => x.GrossTaxable).Name(messageColumns.GrossTaxable);
                Map(x => x.TaxPaid).Name(messageColumns.TaxPaid);
            }

            if (messageType == RequestType.SL1)
                Map(x => x.PlanType).Name(messageColumns.PlanType);
        }
    }
}