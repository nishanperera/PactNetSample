using System;
using System.IO;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace Consumer.Tests
{
    public class CardApiPact : IDisposable
    {
        public PactBuilder PactBuilder { get; set; }
        public IMockProviderService MockProviderService { get; set; }
        public int MockServicePort => 3001;
        public string MockProviderServiceBaseUri => $"http://localhost:{MockServicePort}";


        public CardApiPact()
        {
            PactBuilder = new PactBuilder(
                new PactConfig { PactDir = @"D:/pacts/", LogDir = @"D:/logs/"}
                );
            PactBuilder
                .ServiceConsumer("Consumer")
                .HasPactWith("Cards Api");

            MockProviderService = PactBuilder.MockService(MockServicePort);

        }

        public void Build()
        {
            PactBuilder.Build();
        }

        public void Dispose()
        {
            PactBuilder.Build();
        }
    }
}