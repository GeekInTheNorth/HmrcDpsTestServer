using System;
using System.Collections.Generic;
using System.Text;
using HmrcTpvsProxy.Domain.Messages.Nodes;
using HmrcTpvsProxy.Domain.Messages.Serialization;
using NUnit.Framework;

namespace HmrcTpvsProxy.Domain.Test.Messages.Serilization
{
    [TestFixture]
    public class StudentLoanStartSerializationTest
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
            stringBuilder.Append("<DataType>SL1</DataType>");
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
                stringBuilder.Append("<StudentLoanStart IssueDate=\"2015-04-01\" SequenceNumber=\"441\" TaxYearEnd=\"2016\" xmlns=\"http://www.govtalk.gov.uk/taxation/StudentLoanStart/2\">");
                stringBuilder.Append("<EmployerRef>123/A6</EmployerRef>");
                stringBuilder.Append("<NINO>BM802103D</NINO>");
                stringBuilder.Append("<Name>");
                stringBuilder.Append("<Forename>Lloyd</Forename>");
                stringBuilder.Append("<Surname>Jones</Surname>");
                stringBuilder.Append("</Name>");
                stringBuilder.Append("<WorksNumber>33015</WorksNumber>");
                stringBuilder.Append("<LoanStartDate>2016-02-28</LoanStartDate>");
                stringBuilder.Append("<PlanType>01</PlanType>");
                stringBuilder.Append("</StudentLoanStart>");
            }

            if (numberOfMessages >= 2)
            {
                stringBuilder.Append("<StudentLoanStart IssueDate=\"2015-04-01\" SequenceNumber=\"442\" TaxYearEnd=\"2016\" xmlns=\"http://www.govtalk.gov.uk/taxation/StudentLoanStart/2\">");
                stringBuilder.Append("<EmployerRef>123/A6</EmployerRef>");
                stringBuilder.Append("<NINO>JC678437D</NINO>");
                stringBuilder.Append("<Name>");
                stringBuilder.Append("<Forename>Pamela</Forename>");
                stringBuilder.Append("<Surname>Hamilton</Surname>");
                stringBuilder.Append("</Name>");
                stringBuilder.Append("<WorksNumber>33063</WorksNumber>");
                stringBuilder.Append("<LoanStartDate>2015-10-01</LoanStartDate>");
                stringBuilder.Append("<PlanType>02</PlanType>");
                stringBuilder.Append("</StudentLoanStart>");
            }

            if (numberOfMessages >= 3)
            {
                stringBuilder.Append("<StudentLoanStart IssueDate=\"2015-04-01\" SequenceNumber=\"443\" TaxYearEnd=\"2016\" xmlns=\"http://www.govtalk.gov.uk/taxation/StudentLoanStart/2\">");
                stringBuilder.Append("<EmployerRef>123/A6</EmployerRef>");
                stringBuilder.Append("<NINO>JN123456D</NINO>");
                stringBuilder.Append("<Name>");
                stringBuilder.Append("<Forename>James</Forename>");
                stringBuilder.Append("<Surname>Boothman</Surname>");
                stringBuilder.Append("</Name>");
                stringBuilder.Append("<WorksNumber>33136</WorksNumber>");
                stringBuilder.Append("<LoanStartDate>2015-03-31</LoanStartDate>");
                stringBuilder.Append("</StudentLoanStart>");
            }

            stringBuilder.Append("</DPSdata>");
            stringBuilder.Append("</DPSretrieveResponse>");
            stringBuilder.Append("</env:Body>");
            stringBuilder.Append("</env:Envelope>");

            return stringBuilder.ToString();
        }

        private Envelope GetMessages(int numberOfMessages)
        {
            var messages = new List<StudentLoanStart>();
            var envelope = new Envelope();
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.DataType = RequestType.SL1.ToString();
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.VendorID = "0178";
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity = "123/A6";
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Got = numberOfMessages;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.HighWaterMark = numberOfMessages;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.NItems = numberOfMessages;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.NItemsReturned = numberOfMessages;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Timestamp = new DateTime(2017, 1, 1);


            if (numberOfMessages >= 1)
            {
                messages.Add(new StudentLoanStart
                {
                    IssueDate = new DateTime(2015, 4, 1),
                    SequenceNumber = 441,
                    TaxYearEnd = 2016,
                    EmployerRef = envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity,
                    Name = new Name
                    {
                        Forename = "Lloyd",
                        Surname = "Jones"
                    },
                    NINO = "BM802103D",
                    WorksNumber = "33015",
                    LoanStartDate = new DateTime(2016, 02, 28),
                    PlanType = "01"
                });
            }

            if (numberOfMessages >= 2)
            {
                messages.Add(new StudentLoanStart
                {
                    IssueDate = new DateTime(2015, 4, 1),
                    SequenceNumber = 442,
                    TaxYearEnd = 2016,
                    EmployerRef = envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity,
                    Name = new Name
                    {
                        Forename = "Pamela",
                        Surname = "Hamilton"
                    },
                    NINO = "JC678437D",
                    WorksNumber = "33063",
                    LoanStartDate = new DateTime(2015, 10, 01),
                    PlanType = "02"
                });
            }

            if (numberOfMessages >= 3)
            {
                messages.Add(new StudentLoanStart
                {
                    IssueDate = new DateTime(2015, 4, 1),
                    SequenceNumber = 443,
                    TaxYearEnd = 2016,
                    EmployerRef = envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity,
                    Name = new Name
                    {
                        Forename = "James",
                        Surname = "Boothman"
                    },
                    NINO = "JN123456D",
                    WorksNumber = "33136",
                    LoanStartDate = new DateTime(2015, 3, 31)
                });
            }

            envelope.Body.DPSretrieveResponse.DPSdata.StudentLoanStart = messages;

            return envelope;
        }

        private string GetBody(string xmlAsString)
        {
            var strippedXml = xmlAsString.Substring(xmlAsString.IndexOf("<env:Body>"));

            return strippedXml.Substring(0, strippedXml.IndexOf("</env:Envelope>"));
        }
    }
}