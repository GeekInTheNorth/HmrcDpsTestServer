using System;
using System.Collections.Generic;
using System.Text;
using HmrcTpvsProxy.Domain.Messages.Nodes;
using HmrcTpvsProxy.Domain.Messages.Serialization;
using NUnit.Framework;

namespace HmrcTpvsProxy.Domain.Test.Messages
{
    [TestFixture]
    public class StudentLoanEndSerializationTest
    {
        [Test]
        public void CorrectlySerializesAMessageWithASingleP6Notice()
        {
            var expectedXml = GetExpectedXml(1);
            var messages = GetMessages(1);

            var serializer = new Serializer();
            var actualXml = serializer.Serialize(messages);

            actualXml = GetBody(actualXml);
            expectedXml = GetBody(expectedXml);

            Assert.That(actualXml, Is.EqualTo(expectedXml));
        }

        [Test]
        [TestCase(2)]
        [TestCase(3)]
        public void CorrectlySerializesAMessageWithAMultipleP6Notices(int numberOfMessages)
        {
            var expectedXml = GetExpectedXml(numberOfMessages);
            var messages = GetMessages(numberOfMessages);

            var serializer = new Serializer();
            var actualXml = serializer.Serialize(messages);

            actualXml = GetBody(actualXml);
            expectedXml = GetBody(expectedXml);

            Assert.That(actualXml, Is.EqualTo(expectedXml));
        }

        private string GetExpectedXml(int numberOfMessages)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<env:Envelope xmlns:env=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
            stringBuilder.Append("<env:Body>");
            stringBuilder.Append("<DPSretrieveResponse xmlns=\"https://tpvs.hmrc.gov.uk/dps\">");
            stringBuilder.Append("<DPSdata xmlns=\"http://www.govtalk.gov.uk/taxation/DPSwrapper/1\">");
            stringBuilder.Append("<DPSheader>");
            stringBuilder.Append("<Service>PAYE</Service>");
            stringBuilder.Append("<EntityType>EmpRef</EntityType>");
            stringBuilder.Append("<Entity>123/A6</Entity>");
            stringBuilder.Append("<DataType>SL2</DataType>");
            stringBuilder.Append(string.Format("<Got>{0}</Got>", numberOfMessages));
            stringBuilder.Append(string.Format("<NItems>{0}</NItems>", numberOfMessages));
            stringBuilder.Append("<VendorID>0178</VendorID>");
            stringBuilder.Append("<MoreData>false</MoreData>");
            stringBuilder.Append(string.Format("<HighWaterMark>{0}</HighWaterMark>", numberOfMessages));
            stringBuilder.Append(string.Format("<NItemsReturned>{0}</NItemsReturned>", numberOfMessages));
            stringBuilder.Append("<Timestamp>2017-01-01T00:00:00</Timestamp>");
            stringBuilder.Append("</DPSheader>");

            if (numberOfMessages >= 1)
            {
                stringBuilder.Append("<StudentLoanEnd IssueDate=\"2015-04-01\" SequenceNumber=\"466\" TaxYearEnd=\"2016\" xmlns=\"http://www.govtalk.gov.uk/taxation/StudentLoanEnd/2\">");
                stringBuilder.Append("<EmployerRef>123/A6</EmployerRef>");
                stringBuilder.Append("<NINO>NW440030B</NINO>");
                stringBuilder.Append("<Name>");
                stringBuilder.Append("<Forename>Simon</Forename>");
                stringBuilder.Append("<Surname>O'Donnell</Surname>");
                stringBuilder.Append("</Name>");
                stringBuilder.Append("<WorksNumber>1670</WorksNumber>");
                stringBuilder.Append("<StopDate>2016-03-31</StopDate>");
                stringBuilder.Append("</StudentLoanEnd>");
            }

            if (numberOfMessages >= 2)
            {
                stringBuilder.Append("<StudentLoanEnd IssueDate=\"2015-04-01\" SequenceNumber=\"467\" TaxYearEnd=\"2016\" xmlns=\"http://www.govtalk.gov.uk/taxation/StudentLoanEnd/2\">");
                stringBuilder.Append("<EmployerRef>123/A6</EmployerRef>");
                stringBuilder.Append("<NINO>WE785789D</NINO>");
                stringBuilder.Append("<Name>");
                stringBuilder.Append("<Forename>Cara</Forename>");
                stringBuilder.Append("<Surname>Houghton</Surname>");
                stringBuilder.Append("</Name>");
                stringBuilder.Append("<WorksNumber>33082</WorksNumber>");
                stringBuilder.Append("<StopDate>2015-07-01</StopDate>");
                stringBuilder.Append("</StudentLoanEnd>");
            }

            if (numberOfMessages >= 3)
            {
                stringBuilder.Append("<StudentLoanEnd IssueDate=\"2015-04-01\" SequenceNumber=\"468\" TaxYearEnd=\"2016\" xmlns=\"http://www.govtalk.gov.uk/taxation/StudentLoanEnd/2\">");
                stringBuilder.Append("<EmployerRef>123/A6</EmployerRef>");
                stringBuilder.Append("<NINO>WP904239A</NINO>");
                stringBuilder.Append("<Name>");
                stringBuilder.Append("<Forename>Sophie</Forename>");
                stringBuilder.Append("<Surname>Lewis</Surname>");
                stringBuilder.Append("</Name>");
                stringBuilder.Append("<WorksNumber>33058</WorksNumber>");
                stringBuilder.Append("<StopDate>2015-09-30</StopDate>");
                stringBuilder.Append("</StudentLoanEnd>");
            }

            stringBuilder.Append("</DPSdata>");
            stringBuilder.Append("</DPSretrieveResponse>");
            stringBuilder.Append("</env:Body>");
            stringBuilder.Append("</env:Envelope>");

            return stringBuilder.ToString();
        }

        private Envelope GetMessages(int numberOfMessages)
        {
            var messages = new List<StudentLoanEnd>();
            var envelope = new Envelope();
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.DataType = RequestType.SL2.ToString();
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.VendorID = "0178";
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity = "123/A6";
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Got = numberOfMessages;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.HighWaterMark = numberOfMessages;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.NItems = numberOfMessages;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.NItemsReturned = numberOfMessages;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Timestamp = new DateTime(2017, 1, 1);


            if (numberOfMessages >= 1)
            {
                messages.Add(new StudentLoanEnd
                {
                    IssueDate = new DateTime(2015, 4, 1),
                    SequenceNumber = 466,
                    TaxYearEnd = 2016,
                    EmployerRef = envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity,
                    Name = new Name
                    {
                        Forename = "Simon",
                        Surname = "O'Donnell"
                    },
                    NINO = "NW440030B",
                    WorksNumber = "1670",
                    EffectiveDate = new DateTime(2016, 03, 31)
                });
            }

            if (numberOfMessages >= 2)
            {
                messages.Add(new StudentLoanEnd
                {
                    IssueDate = new DateTime(2015, 4, 1),
                    SequenceNumber = 467,
                    TaxYearEnd = 2016,
                    EmployerRef = envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity,
                    Name = new Name
                    {
                        Forename = "Cara",
                        Surname = "Houghton"
                    },
                    NINO = "WE785789D",
                    WorksNumber = "33082",
                    EffectiveDate = new DateTime(2015, 07, 01)
                });
            }

            if (numberOfMessages >= 3)
            {
                messages.Add(new StudentLoanEnd
                {
                    IssueDate = new DateTime(2015, 4, 1),
                    SequenceNumber = 468,
                    TaxYearEnd = 2016,
                    EmployerRef = envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity,
                    Name = new Name
                    {
                        Forename = "Sophie",
                        Surname = "Lewis"
                    },
                    NINO = "WP904239A",
                    WorksNumber = "33058",
                    EffectiveDate = new DateTime(2015, 9, 30)
                });
            }

            envelope.Body.DPSretrieveResponse.DPSdata.StudentLoanEnd = messages;

            return envelope;
        }

        private string GetBody(string xmlAsString)
        {
            var strippedXml = xmlAsString.Substring(xmlAsString.IndexOf("<env:Body>"));

            return strippedXml.Substring(0, strippedXml.IndexOf("</env:Envelope>"));
        }
    }
}