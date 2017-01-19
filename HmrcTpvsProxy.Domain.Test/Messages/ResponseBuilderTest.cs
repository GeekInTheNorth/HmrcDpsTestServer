using System;
using System.Collections.Generic;
using System.Linq;
using HmrcTpvsProxy.Domain.Messages;
using HmrcTpvsProxy.Domain.Messages.Nodes;
using NUnit.Framework;

namespace HmrcTpvsProxy.Domain.Test.Messages
{
    [TestFixture]
    public class ResponseBuilderTest
    {
        private ResponseBuilder responseBuilder;

        [SetUp]
        public void Setup()
        {
            responseBuilder = new ResponseBuilder();
        }

        [Test]
        public void GivenIHaveMoreThanTwentyPendingP9Messages_WhenIBuildAResponse_ThenOnlyTwentyMessagesShouldBeReturned()
        {
            var messages = GetCodingNoticesP9(21);
            var requestData = new RequestData
            {
                RequestType = RequestType.P9,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = 0
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.CodingNoticesP9.Count(), Is.EqualTo(20));
            Assert.That(response.Body.DPSretrieveResponse.DPSdata.DPSheader.NItemsReturned, Is.EqualTo(20));
        }

        [Test]
        public void GivenIHaveMoreThanTwentyPendingP9Messages_WhenIBuildAResponse_ThenTheHeaderShouldSayThereIsMoreData()
        {
            var messages = GetCodingNoticesP9(21);
            var requestData = new RequestData
            {
                RequestType = RequestType.P9,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = 0
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.DPSheader.MoreData, Is.True);
        }

        [Test]
        public void GivenIHaveLessThanTwentyPendingP9Messages_WhenIBuildAResponse_ThenAllMessagesShouldBeReturned()
        {
            var messages = GetCodingNoticesP9(19);
            var requestData = new RequestData
            {
                RequestType = RequestType.P9,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = 0
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.CodingNoticesP9.Count(), Is.EqualTo(19));
            Assert.That(response.Body.DPSretrieveResponse.DPSdata.DPSheader.NItemsReturned, Is.EqualTo(19));
        }

        [Test]
        public void GivenIHaveLessThanTwentyPendingP9Messages_WhenIBuildAResponse_ThenTheHeaderShouldSayThereIsNoMoreData()
        {
            var messages = GetCodingNoticesP9(19);
            var requestData = new RequestData
            {
                RequestType = RequestType.P9,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = 0
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.DPSheader.MoreData, Is.False);
        }

        [Test]
        [TestCase(0)]
        [TestCase(10)]
        public void GivenIHaveAlreadyRecievedP9Messages_WhenIBuildAResponse_ThenOnlyNewMessagesShouldBeReturned(int lastSequenceNumber)
        {
            var messages = GetCodingNoticesP9(30);
            var requestData = new RequestData
            {
                RequestType = RequestType.P9,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = lastSequenceNumber
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.CodingNoticesP9.Any(x => x.SequenceNumber <= lastSequenceNumber), Is.False);
        }

        [Test]
        public void GivenIHaveMoreThanTwentyPendingP6Messages_WhenIBuildAResponse_ThenOnlyTwentyMessagesShouldBeReturned()
        {
            var messages = GetCodingNoticesP6(21);
            var requestData = new RequestData
            {
                RequestType = RequestType.P6,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = 0
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.CodingNoticesP6P6B.Count(), Is.EqualTo(20));
            Assert.That(response.Body.DPSretrieveResponse.DPSdata.DPSheader.NItemsReturned, Is.EqualTo(20));
        }

        [Test]
        public void GivenIHaveMoreThanTwentyPendingP6Messages_WhenIBuildAResponse_ThenTheHeaderShouldSayThereIsMoreData()
        {
            var messages = GetCodingNoticesP6(21);
            var requestData = new RequestData
            {
                RequestType = RequestType.P6,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = 0
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.DPSheader.MoreData, Is.True);
        }

        [Test]
        public void GivenIHaveLessThanTwentyPendingP6Messages_WhenIBuildAResponse_ThenAllMessagesShouldBeReturned()
        {
            var messages = GetCodingNoticesP6(19);
            var requestData = new RequestData
            {
                RequestType = RequestType.P6,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = 0
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.CodingNoticesP6P6B.Count(), Is.EqualTo(19));
            Assert.That(response.Body.DPSretrieveResponse.DPSdata.DPSheader.NItemsReturned, Is.EqualTo(19));
        }

        [Test]
        public void GivenIHaveLessThanTwentyPendingP6Messages_WhenIBuildAResponse_ThenTheHeaderShouldSayThereIsNoMoreData()
        {
            var messages = GetCodingNoticesP6(19);
            var requestData = new RequestData
            {
                RequestType = RequestType.P6,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = 0
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.DPSheader.MoreData, Is.False);
        }

        [Test]
        [TestCase(0)]
        [TestCase(10)]
        public void GivenIHaveAlreadyRecievedP6Messages_WhenIBuildAResponse_ThenOnlyNewMessagesShouldBeReturned(int lastSequenceNumber)
        {
            var messages = GetCodingNoticesP6(30);
            var requestData = new RequestData
            {
                RequestType = RequestType.P6,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = lastSequenceNumber
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.CodingNoticesP6P6B.Any(x => x.SequenceNumber <= lastSequenceNumber), Is.False);
        }

        [Test]
        public void GivenIHaveMoreThanTwentyPendingSL1Messages_WhenIBuildAResponse_ThenOnlyTwentyMessagesShouldBeReturned()
        {
            var messages = GetStudentLoanStartNotices(21);
            var requestData = new RequestData
            {
                RequestType = RequestType.SL1,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = 0
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.StudentLoanStart.Count(), Is.EqualTo(20));
            Assert.That(response.Body.DPSretrieveResponse.DPSdata.DPSheader.NItemsReturned, Is.EqualTo(20));
        }

        [Test]
        public void GivenIHaveMoreThanTwentyPendingSL1Messages_WhenIBuildAResponse_ThenTheHeaderShouldSayThereIsMoreData()
        {
            var messages = GetStudentLoanStartNotices(21);
            var requestData = new RequestData
            {
                RequestType = RequestType.SL1,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = 0
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.DPSheader.MoreData, Is.True);
        }

        [Test]
        public void GivenIHaveLessThanTwentyPendingSL1Messages_WhenIBuildAResponse_ThenAllMessagesShouldBeReturned()
        {
            var messages = GetStudentLoanStartNotices(19);
            var requestData = new RequestData
            {
                RequestType = RequestType.SL1,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = 0
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.StudentLoanStart.Count(), Is.EqualTo(19));
            Assert.That(response.Body.DPSretrieveResponse.DPSdata.DPSheader.NItemsReturned, Is.EqualTo(19));
        }

        [Test]
        public void GivenIHaveLessThanTwentyPendingSL1Messages_WhenIBuildAResponse_ThenTheHeaderShouldSayThereIsNoMoreData()
        {
            var messages = GetStudentLoanStartNotices(19);
            var requestData = new RequestData
            {
                RequestType = RequestType.SL1,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = 0
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.DPSheader.MoreData, Is.False);
        }

        [Test]
        [TestCase(0)]
        [TestCase(10)]
        public void GivenIHaveAlreadyRecievedSL1Messages_WhenIBuildAResponse_ThenOnlyNewMessagesShouldBeReturned(int lastSequenceNumber)
        {
            var messages = GetStudentLoanStartNotices(30);
            var requestData = new RequestData
            {
                RequestType = RequestType.SL1,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = lastSequenceNumber
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.StudentLoanStart.Any(x => x.SequenceNumber <= lastSequenceNumber), Is.False);
        }

        [Test]
        public void GivenIHaveMoreThanTwentyPendingSL2Messages_WhenIBuildAResponse_ThenOnlyTwentyMessagesShouldBeReturned()
        {
            var messages = GetStudentLoanEndNotices(21);
            var requestData = new RequestData
            {
                RequestType = RequestType.SL2,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = 0
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.StudentLoanEnd.Count(), Is.EqualTo(20));
            Assert.That(response.Body.DPSretrieveResponse.DPSdata.DPSheader.NItemsReturned, Is.EqualTo(20));
        }

        [Test]
        public void GivenIHaveMoreThanTwentyPendingSL2Messages_WhenIBuildAResponse_ThenTheHeaderShouldSayThereIsMoreData()
        {
            var messages = GetStudentLoanEndNotices(21);
            var requestData = new RequestData
            {
                RequestType = RequestType.SL2,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = 0
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.DPSheader.MoreData, Is.True);
        }

        [Test]
        public void GivenIHaveLessThanTwentyPendingSL2Messages_WhenIBuildAResponse_ThenAllMessagesShouldBeReturned()
        {
            var messages = GetStudentLoanEndNotices(19);
            var requestData = new RequestData
            {
                RequestType = RequestType.SL2,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = 0
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.StudentLoanEnd.Count(), Is.EqualTo(19));
            Assert.That(response.Body.DPSretrieveResponse.DPSdata.DPSheader.NItemsReturned, Is.EqualTo(19));
        }

        [Test]
        public void GivenIHaveLessThanTwentyPendingSL2Messages_WhenIBuildAResponse_ThenTheHeaderShouldSayThereIsNoMoreData()
        {
            var messages = GetStudentLoanEndNotices(19);
            var requestData = new RequestData
            {
                RequestType = RequestType.SL2,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = 0
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.DPSheader.MoreData, Is.False);
        }

        [Test]
        [TestCase(0)]
        [TestCase(10)]
        public void GivenIHaveAlreadyRecievedSL2Messages_WhenIBuildAResponse_ThenOnlyNewMessagesShouldBeReturned(int lastSequenceNumber)
        {
            var messages = GetStudentLoanEndNotices(30);
            var requestData = new RequestData
            {
                RequestType = RequestType.SL2,
                PayeReference = "123/ABC",
                VendorId = "Vendor 1",
                LastSequenceNumberRecieved = lastSequenceNumber
            };

            var response = responseBuilder.Build(requestData, messages);

            Assert.That(response.Body.DPSretrieveResponse.DPSdata.StudentLoanEnd.Any(x => x.SequenceNumber <= lastSequenceNumber), Is.False);
        }

        private IEnumerable<CodingNoticesP9> GetCodingNoticesP9(int numberOfNotices)
        {
            var notices = new List<CodingNoticesP9>();

            for(var loop = 1; loop <= numberOfNotices; loop++)
            {
                notices.Add(new CodingNoticesP9
                {
                    FormType = RequestType.P9.ToString(),
                    IssueDate = new DateTime(2016, 4, 1).AddDays(loop),
                    SequenceNumber = 229,
                    TaxYearEnd = 2015,
                    EmployerRef = "123/ABC",
                    Name = new Name
                    {
                        Forename = "Joe",
                        Surname = "Bloggs"
                    },
                    NINO = string.Format("AA{0}A", (100000 + loop)),
                    WorksNumber = loop.ToString(),
                    EffectiveDate = new DateTime(2016, 5, 1).AddDays(loop),
                    CodingUpdate = new CodingUpdate
                    {
                        TaxCode = new TaxCode
                        {
                            Value = "1100L"
                        }
                    }
                });
            }

            return notices;
        }

        private IEnumerable<CodingNoticesP6P6B> GetCodingNoticesP6(int numberOfNotices)
        {
            var notices = new List<CodingNoticesP6P6B>();

            for (var loop = 1; loop <= numberOfNotices; loop++)
            {
                notices.Add(new CodingNoticesP6P6B
                {
                    FormType = RequestType.P9.ToString(),
                    IssueDate = new DateTime(2016, 4, 1).AddDays(loop),
                    SequenceNumber = 229,
                    TaxYearEnd = 2015,
                    EmployerRef = "123/ABC",
                    Name = new Name
                    {
                        Forename = "Joe",
                        Surname = "Bloggs"
                    },
                    NINO = string.Format("AA{0}A", (100000 + loop)),
                    WorksNumber = loop.ToString(),
                    EffectiveDate = new DateTime(2016, 5, 1).AddDays(loop),
                    CodingUpdate = new CodingUpdate
                    {
                        TaxCode = new TaxCode
                        {
                            Value = "1100L"
                        }
                    }
                });
            }

            return notices;
        }

        private IEnumerable<StudentLoanStart> GetStudentLoanStartNotices(int numberOfNotices)
        {
            var notices = new List<StudentLoanStart>();

            for (var loop = 1; loop <= numberOfNotices; loop++)
            {
                notices.Add(new StudentLoanStart
                {
                    IssueDate = new DateTime(2016, 4, 1).AddDays(loop),
                    SequenceNumber = 229,
                    TaxYearEnd = 2015,
                    EmployerRef = "123/ABC",
                    Name = new Name
                    {
                        Forename = "Joe",
                        Surname = "Bloggs"
                    },
                    NINO = string.Format("AA{0}A", (100000 + loop)),
                    WorksNumber = loop.ToString(),
                    EffectiveDate = new DateTime(2016, 5, 1).AddDays(loop)
                });
            }

            return notices;
        }

        private IEnumerable<StudentLoanEnd> GetStudentLoanEndNotices(int numberOfNotices)
        {
            var notices = new List<StudentLoanEnd>();

            for (var loop = 1; loop <= numberOfNotices; loop++)
            {
                notices.Add(new StudentLoanEnd
                {
                    IssueDate = new DateTime(2016, 4, 1).AddDays(loop),
                    SequenceNumber = 229,
                    TaxYearEnd = 2015,
                    EmployerRef = "123/ABC",
                    Name = new Name
                    {
                        Forename = "Joe",
                        Surname = "Bloggs"
                    },
                    NINO = string.Format("AA{0}A", (100000 + loop)),
                    WorksNumber = loop.ToString(),
                    EffectiveDate = new DateTime(2016, 5, 1).AddDays(loop)
                });
            }

            return notices;
        }
    }
}