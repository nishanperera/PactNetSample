using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace Consumer
{
    public class CardApiClient
    {
        private readonly string _mockProviderServiceaBaseUrl;

        public CardApiClient(string mockProviderServiceaBaseUrl)
        {
            _mockProviderServiceaBaseUrl = mockProviderServiceaBaseUrl;
        }

        public Card GetCards(int id)
        {
            string reasonPhrase;

            using (var client = new HttpClient { BaseAddress = new Uri(_mockProviderServiceaBaseUrl) })
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "/card/" + id);
                request.Headers.Add("Accept", "application/json");

                var response = client.SendAsync(request);

                var content = response.Result.Content.ReadAsStringAsync().Result;
                var status = response.Result.StatusCode;

                //NOTE: any Pact mock provider errors will be returned here and in the response body
                reasonPhrase = response.Result.ReasonPhrase; 

                request.Dispose();
                response.Dispose();

                if (status == HttpStatusCode.OK)
                {
                    return !String.IsNullOrEmpty(content) ?
                      JsonConvert.DeserializeObject<Card>(content)
                      : null;
                }
            }

            throw new Exception(reasonPhrase);

        }
    }


    public class Card
    {
        public int Id { get; set; }
        public string  DisplayName { get; set; }
    }
}
