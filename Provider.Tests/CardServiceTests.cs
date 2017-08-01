using System;
using System.Net.Http;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using PactNet;

namespace Provider.Tests
{
    [TestFixture]
    public class CardServiceTests
    {
        [Test]
        public void EnsureCardApiHonorsPactWithConsumer()
        {
            var config = new PactVerifierConfig();
            IPactVerifier pactVerifier = new PactVerifier(() => { }, () => { }, config);

            pactVerifier
                .ProviderState("When card exists", setUp: DataSetup, tearDown: DataTearDown);

            using (var testServer = TestServer.Create<Startup>())
            {

                pactVerifier
                   .ServiceProvider("Card Api", testServer.HttpClient)
                   .HonoursPactWith("Consumer")
                   .PactUri(@"D:\pacts\consumer-cards_api.json")
                   .Verify();
            }
        }

        private void DataTearDown()
        {

        }


        private void DataSetup()
        {
        }
    }
}
