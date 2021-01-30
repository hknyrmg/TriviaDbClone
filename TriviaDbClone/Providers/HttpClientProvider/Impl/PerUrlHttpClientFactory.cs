using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaDbClone.Providers.HttpClientProvider.Impl
{
    /// <summary>
    /// same url use same HttpClient
    /// </summary>
    public class PerUrlHttpClientFactory : HttpClientFactoryBase
    {
        public PerUrlHttpClientFactory()
        {
        }

        public PerUrlHttpClientFactory(TimeSpan defaultClientTimeout) : base(defaultClientTimeout)
        {
        }

        protected override string GetCacheKey(string url)
        {
            return url;
        }

        protected override IHttpClient Create(string url)
        {
            return new SafeHttpClient(this, url);
        }
    }
}
