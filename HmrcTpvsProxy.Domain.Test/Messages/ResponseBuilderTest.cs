using System.Linq;
using HmrcTpvsProxy.Domain.Messages;
using NUnit.Framework;

namespace HmrcTpvsProxy.Domain.Test.Messages
{
    [TestFixture]
    public class ResponseBuilderTest
    {
        private ResponseBuilder responseBuilder;
        private TestDataBuilder testData;

        [SetUp]
        public void Setup()
        {
            responseBuilder = new ResponseBuilder();
            testData = new TestDataBuilder();
        }

        [Test]
        public void GivenIHaveMoreThanTwentyPendingP9Messages_WhenIBuildAResponse_ThenOnlyTwentyMessagesShouldBeReturned()
        {
            var messages = testData.GetCodingNoticesP9(21);
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
            var messages = testData.GetCodingNoticesP9(21);
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
            var messages = testData.GetCodingNoticesP9(19);
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
            var messages = testData.GetCodingNoticesP9(19);
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
            var messages = testData.GetCodingNoticesP9(30);
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
            var messages = testData.GetCodingNoticesP6(21);
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
            var messages = testData.GetCodingNoticesP6(21);
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
            var messages = testData.GetCodingNoticesP6(19);
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
            var messages = testData.GetCodingNoticesP6(19);
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
            var messages = testData.GetCodingNoticesP6(30);
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
            var messages = testData.GetStudentLoanStartNotices(21);
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
            var messages = testData.GetStudentLoanStartNotices(21);
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
            var messages = testData.GetStudentLoanStartNotices(19);
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
            var messages = testData.GetStudentLoanStartNotices(19);
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
            var messages = testData.GetStudentLoanStartNotices(30);
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
            var messages = testData.GetStudentLoanEndNotices(21);
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
            var messages = testData.GetStudentLoanEndNotices(21);
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
            var messages = testData.GetStudentLoanEndNotices(19);
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
            var messages = testData.GetStudentLoanEndNotices(19);
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
            var messages = testData.GetStudentLoanEndNotices(30);
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
    }
}