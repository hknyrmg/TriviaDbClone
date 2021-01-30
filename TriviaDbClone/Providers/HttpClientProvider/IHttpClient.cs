using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TriviaDbClone.Providers.HttpClientProvider
{
    public interface IHttpClient : IDisposable
    {
        HttpClient HttpClient { get; }

        HttpMessageHandler HttpMessageHandler { get; }

        string BaseUrl { get; set; }
        bool IsDisposed { get; }
    }
}
