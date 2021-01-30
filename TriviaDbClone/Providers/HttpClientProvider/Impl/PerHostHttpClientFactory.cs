using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaDbClone.Providers.HttpClientProvider.Impl
{
    /// <summary>
    /// same Host use same HttpClient
    /// </summary>
    public class PerHostHttpClientFactory : HttpClientFactoryBase
    {
        public PerHostHttpClientFactory()
        {
        }

        public PerHostHttpClientFactory(TimeSpan defaultClientTimeout) : base(defaultClientTimeout)
        {
        }

        protected override string GetCacheKey(string url)
        {
            return new Uri(url).Host;
        }
    }
}
