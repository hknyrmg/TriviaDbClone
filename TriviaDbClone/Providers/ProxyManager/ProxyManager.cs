using Microsoft.Extensions.Configuration;
using TriviaDbClone.Providers.HttpClientProvider.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TriviaDbClone.Models;

namespace TriviaDbClone.Providers.ProxyManager
{
    public class ProxyManager : IProxyManager
    {
        public IConfiguration Configuration { get; }

  
        public ProxyManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<string> ReadAsStringAsync()
        {
            RapidApiSettings rapidApiValues = new RapidApiSettings();
            PerUrlHttpClientFactory perUrlHttpClientFactory = new PerUrlHttpClientFactory(TimeSpan.FromSeconds(10));
            HttpClient client = perUrlHttpClientFactory.GetHttpClient(rapidApiValues.RapidApiBaseUri);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(rapidApiValues.RapidApiBaseUri),
                Headers = {
                
            },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                

                return body == null ? string.Empty : body;
            }
        }
    }
}
