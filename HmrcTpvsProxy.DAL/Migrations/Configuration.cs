namespace HmrcTpvsProxy.DAL.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using Domain;
    using Domain.Messages.Nodes;
    using Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<HmrcTpvsProxy.DAL.DpsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DpsContext context)
        {
            var dataset = new Dataset
            {
                Name = "Default Edge Cases",
                PayeReference = "123/A6",
                CodingNotices = new List<CodingNotice>(),
                StudentLoanNotices = new List<StudentLoanNotice>()
            };

            AddP6Notices(dataset);
            AddP9Notices(dataset);
            AddSL1Notices(dataset);
            AddSL2Notices(dataset);

            context.Datasets.Add(dataset);
            context.SaveChanges();
        }

        private void AddP6Notices(Dataset dataset)
        {
            var responseFileRetriever = new ResponseFileRetriever();
            var xml = responseFileRetriever.GetResponseAsString(RequestType.P6);

            using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
            {
                reader.MoveToContent();
                var deserializedObject = new XmlSerializer(typeof(Envelope)).Deserialize(reader);
                var envelope = (Envelope)deserializedObject;

                foreach(var notice in envelope.Body.DPSretrieveResponse.DPSdata.CodingNoticesP6P6B)
                {
                    dataset.CodingNotices.Add(new CodingNotice
                    {
                        EffectiveDate = notice.EffectiveDate,
                        Forename = notice.Name.Forename,
                        GrossTaxableInPreviousEmployment = notice.CodingUpdate.TotalPreviousPay,
                        IssueDate = notice.IssueDate,
                        MessageType = notice.FormType,
                        NationalInsuranceNo = notice.NINO,
                        SequenceNo = notice.SequenceNumber,
                        Surname = notice.Name.Surname,
                        TaxBasisNonCumulative = notice.CodingUpdate.TaxCode.Week1Month1Indicator,
                        TaxCode = notice.CodingUpdate.TaxCode.Value,
                        TaxPaidInPreviousEmployment = notice.CodingUpdate.TotalPreviousTax,
                        TaxRegime = notice.CodingUpdate.TaxCode.TaxRegime,
                        TaxYear = notice.TaxYearEnd,
                        WorksNumber = notice.WorksNumber
                    });
                }
            }
        }

        private void AddP9Notices(Dataset dataset)
        {
            var responseFileRetriever = new ResponseFileRetriever();
            var xml = responseFileRetriever.GetResponseAsString(RequestType.P9);

            using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
            {
                reader.MoveToContent();
                var deserializedObject = new XmlSerializer(typeof(Envelope)).Deserialize(reader);
                var envelope = (Envelope)deserializedObject;

                foreach (var notice in envelope.Body.DPSretrieveResponse.DPSdata.CodingNoticeP9)
                {
                    dataset.CodingNotices.Add(new CodingNotice
                    {
                        EffectiveDate = notice.EffectiveDate,
                        Forename = notice.Name.Forename,
                        GrossTaxableInPreviousEmployment = notice.CodingUpdate.TotalPreviousPay,
                        IssueDate = notice.IssueDate,
                        MessageType = notice.FormType,
                        NationalInsuranceNo = notice.NINO,
                        SequenceNo = notice.SequenceNumber,
                        Surname = notice.Name.Surname,
                        TaxBasisNonCumulative = notice.CodingUpdate.TaxCode.Week1Month1Indicator,
                        TaxCode = notice.CodingUpdate.TaxCode.Value,
                        TaxPaidInPreviousEmployment = notice.CodingUpdate.TotalPreviousTax,
                        TaxRegime = notice.CodingUpdate.TaxCode.TaxRegime,
                        TaxYear = notice.TaxYearEnd,
                        WorksNumber = notice.WorksNumber
                    });
                }
            }
        }

        private void AddSL1Notices(Dataset dataset)
        {
            var responseFileRetriever = new ResponseFileRetriever();
            var xml = responseFileRetriever.GetResponseAsString(RequestType.SL1);

            using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
            {
                reader.MoveToContent();
                var deserializedObject = new XmlSerializer(typeof(Envelope)).Deserialize(reader);
                var envelope = (Envelope)deserializedObject;

                foreach (var notice in envelope.Body.DPSretrieveResponse.DPSdata.StudentLoanStart)
                {
                    dataset.StudentLoanNotices.Add(new StudentLoanNotice
                    {
                        EffectiveDate = notice.EffectiveDate,
                        Forename = notice.Name.Forename,
                        IssueDate = notice.IssueDate,
                        MessageType = RequestType.SL1.ToString(),
                        NationalInsuranceNo = notice.NINO,
                        PlanType = notice.PlanType,
                        SequenceNo = notice.SequenceNumber,
                        Surname = notice.Name.Surname,
                        TaxYear = notice.TaxYearEnd,
                        WorksNumber = notice.WorksNumber
                    });
                }
            }
        }

        private void AddSL2Notices(Dataset dataset)
        {
            var responseFileRetriever = new ResponseFileRetriever();
            var xml = responseFileRetriever.GetResponseAsString(RequestType.SL2);

            using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
            {
                reader.MoveToContent();
                var deserializedObject = new XmlSerializer(typeof(Envelope)).Deserialize(reader);
                var envelope = (Envelope)deserializedObject;

                foreach (var notice in envelope.Body.DPSretrieveResponse.DPSdata.StudentLoanEnd)
                {
                    dataset.StudentLoanNotices.Add(new StudentLoanNotice
                    {
                        EffectiveDate = notice.EffectiveDate,
                        Forename = notice.Name.Forename,
                        IssueDate = notice.IssueDate,
                        MessageType = RequestType.SL2.ToString(),
                        NationalInsuranceNo = notice.NINO,
                        SequenceNo = notice.SequenceNumber,
                        Surname = notice.Name.Surname,
                        TaxYear = notice.TaxYearEnd,
                        WorksNumber = notice.WorksNumber
                    });
                }
            }
        }
    }
}