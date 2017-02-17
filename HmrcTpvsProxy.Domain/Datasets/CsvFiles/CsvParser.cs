using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;

namespace HmrcTpvsProxy.Domain.Datasets.CsvFiles
{
    public class CsvParser : ICsvParser
    {
        private readonly MessageColumns columns;

        public CsvParser()
        {
            columns = new MessageColumns();
        }

        public IEnumerable<MessageDTO> Parse(Stream csvStream, RequestType messageType)
        {
            if (csvStream == null) throw new ArgumentNullException(nameof(csvStream));

            var messages = new List<MessageDTO>();

            using (var streamReader = new StreamReader(csvStream))
            using (var csvReader = new CsvReader(streamReader))
            {
                while (csvReader.Read())
                {
                    var formType = GetStringValue(csvReader, columns.PlanType);
                    formType = string.IsNullOrWhiteSpace(formType) ? messageType.ToString() : formType;

                    var message = new MessageDTO
                    {
                        FormType = formType,
                        Forename = GetStringValue(csvReader, columns.Forename),
                        Surname = GetStringValue(csvReader, columns.Surname),
                        NINO = GetStringValue(csvReader, columns.NationalInsuranceNo),
                        PayId = GetStringValue(csvReader, columns.PayId),
                        IssueDate = GetDateValue(csvReader, columns.IssueDate),
                        SequenceNumber = GetIntegerValue(csvReader, columns.SequenceNumber),
                        TaxYearEnd = GetIntegerValue(csvReader, columns.SequenceNumber),
                        EffectiveDate = GetDateValue(csvReader, columns.EffectiveDate),
                        TaxCode = GetStringValue(csvReader, columns.TaxCode),
                        TaxRegime = GetStringValue(csvReader, columns.TaxRegime),
                        TaxBasisNonCumulative = GetStringValue(csvReader, columns.TaxBasisNonCumulative),
                        GrossTaxable = GetDecimalValue(csvReader, columns.GrossTaxable),
                        TaxPaid = GetDecimalValue(csvReader, columns.TaxPaid),
                        PlanType = GetStringValue(csvReader, columns.PlanType)
                    };

                    messages.Add(message);
                }
            }

            return messages;
        }

        private string GetStringValue(CsvReader reader, string fieldName)
        {
            if (!reader.FieldHeaders.Contains(fieldName))
                return string.Empty;

            return reader.GetField(fieldName);
        }

        private int GetIntegerValue(CsvReader reader, string fieldName)
        {
            if (!reader.FieldHeaders.Contains(fieldName))
                return 0;

            int returnValue;
            if (!reader.TryGetField(fieldName, out returnValue))
                returnValue = 0;

            return returnValue;
        }

        private decimal GetDecimalValue(CsvReader reader, string fieldName)
        {
            if (!reader.FieldHeaders.Contains(fieldName))
                return 0M;

            decimal returnValue;
            if (!reader.TryGetField(fieldName, out returnValue))
                returnValue = 0M;

            return returnValue;
        }

        private DateTime GetDateValue(CsvReader reader, string fieldName)
        {
            if (!reader.FieldHeaders.Contains(fieldName))
                return DateTime.Today;

            DateTime returnValue;
            if (!reader.TryGetField(fieldName, out returnValue))
                returnValue = DateTime.Today;

            return returnValue;
        }
    }
}