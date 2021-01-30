using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TriviaDbClone.Providers.HttpClientProvider
{
  
        public interface IHttpClientFactory : IDisposable
        {
            HttpClient GetHttpClient(string url);

            HttpClient GetProxiedHttpClient(string proxyUrl);

        }
    
}
