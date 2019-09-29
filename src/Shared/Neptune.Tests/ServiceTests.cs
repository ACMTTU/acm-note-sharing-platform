using ACMTTU.NoteSharing.Shared.Neptune.Messages;
using ACMTTU.NoteSharing.Shared.Neptune.Services;
using ACMTTU.NoteSharing.Shared.TestingUtils;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//http://anthonygiretti.com/2018/09/06/how-to-unit-test-a-class-that-consumes-an-httpclient-with-ihttpclientfactory-in-asp-net-core/

namespace Neptune.Tests
{
    //Almost verbatim from link above
    public interface IFakeService : IService { }
    public class FakeApp : MessageService<IFakeService>
    {
        public FakeApp(System.Net.Http.IHttpClientFactory clientFactory) : base(clientFactory) { }
    }
    public class FakeGetRequest : GetRequest<IFakeService>
    {
        public override Uri Uri => new Uri("http://do.notclick.me");
    }
    public class FakeGetResponse : Response<IFakeService, FakeGetRequest>
    {
        public string body = "default body";
        public string errorCode = "No error... because I am fake";

        public FakeGetResponse()
        {
        }

        public FakeGetResponse(string body)
        {
            this.body = body;
        }
    }

    [TestFixture]
    class ServiceTests
    {
        [Test]
        public void WhenAReqIsProvided_ServiceShouldReturnCorrectResp()
        {
            var fakeResponse = new FakeGetResponse("I am fake");
            var fakeMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(fakeResponse),
                Encoding.UTF8,
                "application/json")
            });
            var fakeClient = new HttpClient(fakeMessageHandler);

            var mockClientFactory = Substitute.For<IHttpClientFactory>();
            mockClientFactory.CreateClient().Returns(fakeClient);

            var app = new FakeApp(mockClientFactory);

            //Act
            FakeGetResponse resp = app
                .GetRequest<FakeGetRequest, FakeGetResponse>(new FakeGetRequest()).Result;
            Assert.True(resp.body == "I am fake");
        }
    }
}
