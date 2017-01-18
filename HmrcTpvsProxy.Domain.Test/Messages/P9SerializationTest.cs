using System;
using System.Collections.Generic;
using System.Text;
using HmrcTpvsProxy.Domain.Messages.Nodes;
using HmrcTpvsProxy.Domain.Messages.Serialization;
using NUnit.Framework;

namespace HmrcTpvsProxy.Domain.Test.Messages
{
    [TestFixture]
    public class P9SerializationTest
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
            var expectedXml = GetExpectedXml(1);
            var messages = GetMessages(1);

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
            stringBuilder.Append("<DataType>P9</DataType>");
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
                stringBuilder.Append("<CodingNoticesP9 FormType=\"P9\" IssueDate=\"2014-01-01\" SequenceNumber=\"227\" TaxYearEnd=\"2015\" xmlns=\"http://www.govtalk.gov.uk/taxation/CodingNoticeP9/2\">");
                stringBuilder.Append("<EmployerRef>123/A6</EmployerRef>");
                stringBuilder.Append("<Name>");
                stringBuilder.Append("<Forename>NiChg</Forename>");
                stringBuilder.Append("<Surname>Leaver</Surname>");
                stringBuilder.Append("</Name>");
                stringBuilder.Append("<NINO>JH987643A</NINO>");
                stringBuilder.Append("<WorksNumber>1264</WorksNumber>");
                stringBuilder.Append("<EffectiveDate>2014-04-06</EffectiveDate>");
                stringBuilder.Append("<CodingUpdate>");
                stringBuilder.Append("<TaxCode TaxRegime=\"S\">647L</TaxCode>");
                stringBuilder.Append("</CodingUpdate>");
                stringBuilder.Append("</CodingNoticesP9>");
            }

            if (numberOfMessages >= 2)
            {
                stringBuilder.Append("<CodingNoticesP9 FormType=\"P9\" IssueDate=\"2014-01-01\" SequenceNumber=\"228\" TaxYearEnd=\"2015\" xmlns=\"http://www.govtalk.gov.uk/taxation/CodingNoticeP9/2\">");
                stringBuilder.Append("<EmployerRef>123/A6</EmployerRef>");
                stringBuilder.Append("<Name>");
                stringBuilder.Append("<Forename>NiChg</Forename>");
                stringBuilder.Append("<Surname>Leaver</Surname>");
                stringBuilder.Append("</Name>");
                stringBuilder.Append("<NINO>JH987643A</NINO>");
                stringBuilder.Append("<WorksNumber>1264</WorksNumber>");
                stringBuilder.Append("<EffectiveDate>2014-04-06</EffectiveDate>");
                stringBuilder.Append("<CodingUpdate>");
                stringBuilder.Append("<TaxCode>647L</TaxCode>");
                stringBuilder.Append("</CodingUpdate>");
                stringBuilder.Append("</CodingNoticesP9>");
            }

            if (numberOfMessages >= 3)
            {
                stringBuilder.Append("<CodingNoticesP9 FormType=\"P9\" IssueDate=\"2014-03-03\" SequenceNumber=\"229\" TaxYearEnd=\"2015\" xmlns=\"http://www.govtalk.gov.uk/taxation/CodingNoticeP9/2\">");
                stringBuilder.Append("<EmployerRef>123/A6</EmployerRef>");
                stringBuilder.Append("<Name>");
                stringBuilder.Append("<Forename>Pamela</Forename>");
                stringBuilder.Append("<Surname>Hamilton</Surname>");
                stringBuilder.Append("</Name>");
                stringBuilder.Append("<NINO>JC678437D</NINO>");
                stringBuilder.Append("<WorksNumber>33063</WorksNumber>");
                stringBuilder.Append("<EffectiveDate>2014-04-06</EffectiveDate>");
                stringBuilder.Append("<CodingUpdate>");
                stringBuilder.Append("<TaxCode Week1Month1Indicator=\"X\">609L</TaxCode>");
                stringBuilder.Append("</CodingUpdate>");
                stringBuilder.Append("</CodingNoticesP9>");
            }

            stringBuilder.Append("</DPSdata>");
            stringBuilder.Append("</DPSretrieveResponse>");
            stringBuilder.Append("</env:Body>");
            stringBuilder.Append("</env:Envelope>");

            return stringBuilder.ToString();
        }

        private Envelope GetMessages(int numberOfMessages)
        {
            var messages = new List<CodingNoticesP9>();
            var envelope = new Envelope();
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.DataType = RequestType.P9.ToString();
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.VendorID = "0178";
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity = "123/A6";
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Got = numberOfMessages;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.HighWaterMark = numberOfMessages;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.NItems = numberOfMessages;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.NItemsReturned = numberOfMessages;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Timestamp = new DateTime(2017, 1, 1);


            if (numberOfMessages >= 1)
            {
                messages.Add(new CodingNoticesP9
                {
                    FormType = RequestType.P9.ToString(),
                    IssueDate = new DateTime(2014, 1, 1),
                    SequenceNumber = 227,
                    TaxYearEnd = 2015,
                    EmployerRef = envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity,
                    Name = new Name
                    {
                        Forename = "NiChg",
                        Surname = "Leaver"
                    },
                    NINO = "JH987643A",
                    WorksNumber = "1264",
                    EffectiveDate = new DateTime(2014, 04, 06),
                    CodingUpdate = new CodingUpdate
                    {
                        TaxCode = new TaxCode
                        {
                            Value = "647L",
                            IsScottishEmployee = true
                        }
                    }
                });
            }

            if (numberOfMessages >= 2)
            {
                messages.Add(new CodingNoticesP9
                {
                    FormType = RequestType.P9.ToString(),
                    IssueDate = new DateTime(2014, 1, 1),
                    SequenceNumber = 228,
                    TaxYearEnd = 2015,
                    EmployerRef = envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity,
                    Name = new Name
                    {
                        Forename = "NiChg",
                        Surname = "Leaver"
                    },
                    NINO = "JH987643A",
                    WorksNumber = "1264",
                    EffectiveDate = new DateTime(2014, 4, 6),
                    CodingUpdate = new CodingUpdate
                    {
                        TaxCode = new TaxCode
                        {
                            Value = "647L"
                        }
                    }
                });
            }

            if (numberOfMessages >= 3)
            {
                messages.Add(new CodingNoticesP9
                {
                    FormType = RequestType.P9.ToString(),
                    IssueDate = new DateTime(2014, 3, 3),
                    SequenceNumber = 229,
                    TaxYearEnd = 2015,
                    EmployerRef = envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity,
                    Name = new Name
                    {
                        Forename = "Pamela",
                        Surname = "Hamilton"
                    },
                    NINO = "JC678437D",
                    WorksNumber = "33063",
                    EffectiveDate = new DateTime(2014, 4, 6),
                    CodingUpdate = new CodingUpdate
                    {
                        TaxCode = new TaxCode
                        {
                            Value = "609L",
                            Week1Month1 = true
                        }
                    }
                });
            }

            envelope.Body.DPSretrieveResponse.DPSdata.CodingNoticesP9 = messages;

            return envelope;
        }

        private string GetBody(string xmlAsString)
        {
            var strippedXml = xmlAsString.Substring(xmlAsString.IndexOf("<env:Body>"));

            return strippedXml.Substring(0, strippedXml.IndexOf("</env:Envelope>"));
        }
    }
}