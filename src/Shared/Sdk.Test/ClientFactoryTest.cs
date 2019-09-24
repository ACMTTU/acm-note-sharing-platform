using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using ACMTTU.NoteSharing.Shared.DataContracts;
using ACMTTU.NoteSharing.Shared.SDK.Clients;
using ACMTTU.NoteSharing.Shared.TestingUtils;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;

namespace ACMTTU.NoteSharing.Shared.Test.Sdk
{
    public class ClientFacoryTest
    {
        [Fact]
        public async void ShouldProvideUsersWithTheCorrectEnvironmentVariableWhenRunningInANamespace()
        {
            List<String> namespaceVariations = new List<String>();
            namespaceVariations.Add("NAMESPACE");
            namespaceVariations.Add("Namespace");
            namespaceVariations.Add("namespace");

            List<String> environments = new List<String>();
            environments.Add("developement");
            environments.Add("staging");
            environments.Add("production");

            IHttpClientFactory httpClientFactoryMock = Substitute.For<IHttpClientFactory>();

            EnvironmentVariablePayload payload = new EnvironmentVariablePayload()
            {
                Development = "Development",
                Staging = "Staging",
                Production = "Production",
            };

            HttpResponseMessage fakeResponseMessage = new HttpResponseMessage();
            fakeResponseMessage.StatusCode = HttpStatusCode.OK;
            fakeResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            FakeHttpMessageHandler fakeHttpMessageHandler = new FakeHttpMessageHandler(fakeResponseMessage);

            HttpClient fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            httpClientFactoryMock.CreateClient().Returns(fakeHttpClient);

            ClientFactory clientFactory = new ClientFactory(httpClientFactoryMock);

            // Check if we get back the right variable with development env

            string dbConnectionString;

            foreach (String namespaces in namespaceVariations)
            {
                foreach (String environment in environments)
                {
                    Environment.SetEnvironmentVariable(namespaces, environment);
                    dbConnectionString = await clientFactory.GetConnectionStringForClient(ClientOptions.Database);

                    switch (environment)
                    {
                        case ("development"):
                            Assert.Equal(dbConnectionString, payload.Development);
                            break;
                        case ("staging"):
                            Assert.Equal(dbConnectionString, payload.Staging);
                            break;
                        case ("production"):
                            Assert.Equal(dbConnectionString, payload.Production);
                            break;
                    }

                    Environment.SetEnvironmentVariable(namespaces, null);
                }
            }

        }
    }
}