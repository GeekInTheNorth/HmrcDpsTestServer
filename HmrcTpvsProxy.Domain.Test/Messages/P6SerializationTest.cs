using System;
using System.Collections.Generic;
using System.Text;
using HmrcTpvsProxy.Domain.Messages.Nodes;
using HmrcTpvsProxy.Domain.Messages.Serialization;
using NUnit.Framework;

namespace HmrcTpvsProxy.Domain.Test.Messages
{
    [TestFixture]
    public class P6SerializationTest
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
            stringBuilder.Append("<DataType>P6</DataType>");
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
                stringBuilder.Append("<CodingNoticesP6P6B FormType=\"P6\" IssueDate=\"2014-01-01\" SequenceNumber=\"1\" TaxYearEnd=\"2015\" xmlns=\"http://www.govtalk.gov.uk/taxation/CodingNoticesP6P6B/2\">");
                stringBuilder.Append("<EmployerRef>123/A6</EmployerRef>");
                stringBuilder.Append("<Name>");
                stringBuilder.Append("<Forename>Cara</Forename>");
                stringBuilder.Append("<Surname>Houghton</Surname>");
                stringBuilder.Append("</Name>");
                stringBuilder.Append("<NINO>WE785789D</NINO>");
                stringBuilder.Append("<WorksNumber>180</WorksNumber>");
                stringBuilder.Append("<EffectiveDate>2014-11-11</EffectiveDate>");
                stringBuilder.Append("<CodingUpdate>");
                stringBuilder.Append("<TaxCode>920L</TaxCode>");
                stringBuilder.Append("<TotalPreviousPay>22624</TotalPreviousPay>");
                stringBuilder.Append("<TotalPreviousTax>3028</TotalPreviousTax>");
                stringBuilder.Append("</CodingUpdate>");
                stringBuilder.Append("</CodingNoticesP6P6B>");
            }

            if (numberOfMessages >= 2)
            {
                stringBuilder.Append("<CodingNoticesP6P6B FormType=\"P6\" IssueDate=\"2014-03-03\" SequenceNumber=\"2\" TaxYearEnd=\"2015\" xmlns=\"http://www.govtalk.gov.uk/taxation/CodingNoticesP6P6B/2\">");
                stringBuilder.Append("<EmployerRef>123/A6</EmployerRef>");
                stringBuilder.Append("<Name>");
                stringBuilder.Append("<Forename>Fiona</Forename>");
                stringBuilder.Append("<Surname>Cameron</Surname>");
                stringBuilder.Append("</Name>");
                stringBuilder.Append("<NINO>JK258147D</NINO>");
                stringBuilder.Append("<WorksNumber>135</WorksNumber>");
                stringBuilder.Append("<EffectiveDate>2014-04-01</EffectiveDate>");
                stringBuilder.Append("<CodingUpdate>");
                stringBuilder.Append("<TaxCode>BR</TaxCode>");
                stringBuilder.Append("</CodingUpdate>");
                stringBuilder.Append("</CodingNoticesP6P6B>");
            }

            if (numberOfMessages >= 3)
            {
                stringBuilder.Append("<CodingNoticesP6P6B FormType=\"P6\" IssueDate=\"2014-03-03\" SequenceNumber=\"3\" TaxYearEnd=\"2015\" xmlns=\"http://www.govtalk.gov.uk/taxation/CodingNoticesP6P6B/2\">");
                stringBuilder.Append("<EmployerRef>123/A6</EmployerRef>");
                stringBuilder.Append("<Name>");
                stringBuilder.Append("<Forename>Raymond</Forename>");
                stringBuilder.Append("<Surname>Jackson</Surname>");
                stringBuilder.Append("</Name>");
                stringBuilder.Append("<NINO>ZB394740B</NINO>");
                stringBuilder.Append("<WorksNumber>136</WorksNumber>");
                stringBuilder.Append("<EffectiveDate>2014-04-01</EffectiveDate>");
                stringBuilder.Append("<CodingUpdate>");
                stringBuilder.Append("<TaxCode>406L</TaxCode>");
                stringBuilder.Append("<TotalPreviousPay>980</TotalPreviousPay>");
                stringBuilder.Append("<TotalPreviousTax>12.41</TotalPreviousTax>");
                stringBuilder.Append("</CodingUpdate>");
                stringBuilder.Append("</CodingNoticesP6P6B>");
            }

            stringBuilder.Append("</DPSdata>");
            stringBuilder.Append("</DPSretrieveResponse>");
            stringBuilder.Append("</env:Body>");
            stringBuilder.Append("</env:Envelope>");

            return stringBuilder.ToString();
        }

        private Envelope GetMessages(int numberOfMessages)
        {
            var messages = new List<CodingNoticesP6P6B>();
            var envelope = new Envelope();
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.DataType = RequestType.P6.ToString();
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.VendorID = "0178";
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity = "123/A6";
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Got = numberOfMessages;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.HighWaterMark = numberOfMessages;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.NItems = numberOfMessages;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.NItemsReturned = numberOfMessages;
            envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Timestamp = new DateTime(2017, 1, 1);


            if (numberOfMessages >= 1)
            {
                messages.Add(new CodingNoticesP6P6B
                {
                    FormType = RequestType.P6.ToString(),
                    IssueDate = new DateTime(2014, 1, 1),
                    SequenceNumber = 1,
                    TaxYearEnd = 2015,
                    EmployerRef = envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity,
                    Name = new Name
                    {
                        Forename = "Cara",
                        Surname = "Houghton"
                    },
                    NINO = "WE785789D",
                    WorksNumber = "180",
                    EffectiveDate = new DateTime(2014, 11, 11),
                    CodingUpdate = new CodingUpdate
                    {
                        TaxCode = new TaxCode
                        {
                            Value = "920L"
                        },
                        TotalPreviousPay = 22624,
                        TotalPreviousTax = 3028
                    }
                });
            }

            if (numberOfMessages >= 2)
            {
                messages.Add(new CodingNoticesP6P6B
                {
                    FormType = RequestType.P6.ToString(),
                    IssueDate = new DateTime(2014, 3, 3),
                    SequenceNumber = 2,
                    TaxYearEnd = 2015,
                    EmployerRef = envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity,
                    Name = new Name
                    {
                        Forename = "Fiona",
                        Surname = "Cameron"
                    },
                    NINO = "JK258147D",
                    WorksNumber = "135",
                    EffectiveDate = new DateTime(2014, 4, 1),
                    CodingUpdate = new CodingUpdate
                    {
                        TaxCode = new TaxCode
                        {
                            Value = "BR"
                        }
                    }
                });
            }

            if (numberOfMessages >= 3)
            {
                messages.Add(new CodingNoticesP6P6B
                {
                    FormType = RequestType.P6.ToString(),
                    IssueDate = new DateTime(2014, 3, 3),
                    SequenceNumber = 3,
                    TaxYearEnd = 2015,
                    EmployerRef = envelope.Body.DPSretrieveResponse.DPSdata.DPSheader.Entity,
                    Name = new Name
                    {
                        Forename = "Raymond",
                        Surname = "Jackson"
                    },
                    NINO = "ZB394740B",
                    WorksNumber = "136",
                    EffectiveDate = new DateTime(2014, 4, 1),
                    CodingUpdate = new CodingUpdate
                    {
                        TaxCode = new TaxCode
                        {
                            Value = "406L"
                        },
                        TotalPreviousPay = 980,
                        TotalPreviousTax = 12.41M
                    }
                });
            }

            envelope.Body.DPSretrieveResponse.DPSdata.CodingNoticesP6P6B = messages;

            return envelope;
        }

        private string GetBody(string xmlAsString)
        {
            var strippedXml = xmlAsString.Substring(xmlAsString.IndexOf("<env:Body>"));

            return strippedXml.Substring(0, strippedXml.IndexOf("</env:Envelope>"));
        }
    }
}