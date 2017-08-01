using System.Collections.Generic;
using NUnit.Framework;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;

namespace Consumer.Tests
{
    [TestFixture]
    public class CardApiClientsTests
    {
        private IMockProviderService _mockProviderService;
        private string _mockProviderServiceaBaseUrl;
        private CardApiPact _cardServicePact;

        [SetUp]
        public void Setup()
        {
            _cardServicePact = new CardApiPact();
            _mockProviderService = _cardServicePact.MockProviderService;
            _mockProviderServiceaBaseUrl = _cardServicePact.MockProviderServiceBaseUri;
            _cardServicePact.MockProviderService.ClearInteractions();
        }

        [Test]
        public void GetCards_WhenHasCards_ShouldReturnCards()
        {
            _mockProviderService
                .Given("There is a card with card id '1'")
                .UponReceiving("A Get Request to retrieve the card")
                .With( new ProviderServiceRequest()
                {
                    Method = HttpVerb.Get,
                    Path = "/card/1",
                    Headers = new Dictionary<string, string>
                    {
                        {"Accept", "application/json" }
                    }
                })
                .WillRespondWith( new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, string>
                    {
                        {"Content-Type", "application/json; content=utf-8" }
                    },
                    Body = new
                    {
                        id = 1,
                        displayName = "VISA"
                    }
                });

            var consumer = new CardApiClient(_mockProviderServiceaBaseUrl);

            var result = consumer.GetCards(1);

            Assert.AreEqual(1, result.Id);

            //NOTE: Verifies that interactions registered on the mock provider are called once and only once
            _mockProviderService.VerifyInteractions(); 
            _mockProviderService.ClearInteractions();
            _cardServicePact.Build();
            
        }

    }
}
