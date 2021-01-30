using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TriviaDbClone.Providers.ProxyManager
{
    public interface IProxyManager
    {
         Task<string> ReadAsStringAsync();
    }
}
